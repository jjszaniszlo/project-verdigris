using System.Threading.Tasks;
using Godot;
using GodotUtilities;
using Scenes.Components.Actor;

public partial class DieComponent : Node2D
{
	[Export]
	public HealthComponent HealthComponent { get; private set; }

	[Export]
	public CanvasItem DeathSprite { get; private set; }

	[Signal]
	public delegate void DiedEventHandler();

	public override void _Ready()
	{
		HealthComponent.Died += OnDied;
	}

	private async void OnDied()
	{
		if (DeathSprite != null)
		{
			var tween = CreateTween()
				.TweenProperty(DeathSprite, "instance_shader_parameters/death_disolve_threshold", 0.0f, 0.5f)
				.From(1.0f);
			await ToSignal(tween, Tween.SignalName.Finished);
		}

		EmitSignal(SignalName.Died);
		GetParent().QueueFree();
	}
}