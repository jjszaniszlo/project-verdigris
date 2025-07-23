using Godot;

namespace Scenes.Components.Actor;

public partial class HurtboxComponent : Area2D
{
	[Export]
	public HealthComponent HealthComponent { get; set; }
}
