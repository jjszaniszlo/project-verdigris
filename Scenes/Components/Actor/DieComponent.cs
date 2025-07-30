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

	[Export]
	public CanvasItem ShadowSprite { get; private set; }

	[Signal]
	public delegate void DiedEventHandler();

	public override void _Ready()
	{
		HealthComponent.Died += OnDied;
		if (DeathSprite == null)
		{
			GD.PushError("DeathSprite is not set in DieComponent. Please assign a valid CanvasItem.");
			return;
		}

		if (ShadowSprite == null)
		{
			GD.PushError("ShadowSprite is not set in DieComponent. Please assign a valid CanvasItem.");
			return;
		}
	}

	private async void OnDied()
	{
		EmitSignal(SignalName.Died);

		CreateTween()
			.TweenProperty(ShadowSprite, "modulate", new Color(1, 1, 1, 0), 1.2f)
			.FromCurrent();
		var tween = CreateTween()
			.TweenProperty(DeathSprite, "instance_shader_parameters/death_disolve_threshold", 0.0f, 1.2f)
			.From(1.0f);
		await ToSignal(tween, Tween.SignalName.Finished);

		GetParent().QueueFree();
	}
}