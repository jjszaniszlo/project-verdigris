using Godot;
using Scenes.Components.Actor;

public partial class Projectile : Area2D 
{
	[Export]
	public float Speed { get; set; } = 400f;

	[Export]
	public Vector2 Direction { get; set; } = Vector2.Right;

	[Export]
	public float Damage { get; set; } = 10f;

	[Export]
	public float Lifetime { get; set; } = 5f;

	[Signal]
	public delegate void LifetimeEndedEventHandler();

	private float _TimeAlive = 0f;

	public override void _PhysicsProcess(double delta)
	{
		Position += Direction * Speed * (float)delta;

		if (GetOverlappingAreas().Count > 0)
		{
			foreach (var area in GetOverlappingAreas())
			{
				if (area is HurtboxComponent hurtbox)
				{
					hurtbox.Damage(Damage);
				}
			}
			EmitSignal(SignalName.LifetimeEnded);
			QueueFree();
		}

		_TimeAlive += (float)delta;
		if (_TimeAlive >= Lifetime)
		{
			EmitSignal(SignalName.LifetimeEnded);
			QueueFree();
		}
	}
}

