using Godot;
using GodotUtilities;
using Scenes.Components.Actor;

public partial class DieComponent : Node2D
{
	[Export]
	public HealthComponent HealthComponent { get; private set; }

	[Signal]
	public delegate void DiedEventHandler();

	public override void _Ready()
	{
		HealthComponent.Died += OnDied;
	}

	private void OnDied()
	{
		EmitSignal(SignalName.Died);
		GetParent().QueueFree();
	}
}