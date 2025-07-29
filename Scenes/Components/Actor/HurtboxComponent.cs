using Godot;

namespace Scenes.Components.Actor;

public partial class HurtboxComponent : Area2D
{
	[Export]
	public HealthComponent HealthComponent { get; set; }

	[Export]
	public CanvasItem TakeDamageSprite {get; private set; }

	[Signal]
	public delegate void HurtboxDamagedEventHandler(float amount);

	public void Damage(float amount)
	{
		HealthComponent.Damage(amount);
		EmitSignal(SignalName.HurtboxDamaged, amount);
		CreateTween()
			.TweenProperty(TakeDamageSprite, "instance_shader_parameters/take_damage_fade", 0.0f, 0.5f)
			.SetTrans(Tween.TransitionType.Bounce)
			.From(1.0f);
	}
}
