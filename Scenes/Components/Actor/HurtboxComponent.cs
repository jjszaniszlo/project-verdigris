using Godot;
using GodotUtilities;

namespace Scenes.Components.Actor;

public partial class HurtboxComponent : Area2D
{
	[Export]
	public HealthComponent HealthComponent { get; set; }

	[Export]
	public CanvasItem TakeDamageSprite {get; private set; }


	public void Damage(int amount)
	{
		HealthComponent.Damage(amount);
		CreateTween()
			.TweenProperty(TakeDamageSprite, "instance_shader_parameters/take_damage_fade", 0.0f, 0.3f)
			.SetTrans(Tween.TransitionType.Bounce)
			.From(1.0f);
	}
}
