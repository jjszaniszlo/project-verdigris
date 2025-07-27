using Godot;
using GodotUtilities;

namespace Scenes.Components.Actor;

public partial class HurtboxComponent : Area2D
{
	[Export]
	public HealthComponent HealthComponent { get; set; }


	public void Damage(int amount)
	{
		HealthComponent.Damage(amount);
	}
}
