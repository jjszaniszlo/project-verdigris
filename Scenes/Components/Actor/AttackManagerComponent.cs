using Godot;
using GodotUtilities;
using Scenes.Components.Actor;

[Scene]
public partial class AttackManagerComponent : Node2D
{
	[Export]
	public PlayerControllerComponent PlayerControllerComponent { get; private set; }

	[Signal]
	public delegate void AttackComponentReadyEventHandler(BaseAttackComponent attackComponent);

	public override void _Ready()
	{
		InjectPlayerControllerComponentsToChildren();
	}

	public void AddAttackComponent(BaseAttackComponent attackComponent)
	{
		InjectPlayerControllerComponent(attackComponent);
		AddChild(attackComponent);
	}

	private void InjectPlayerControllerComponent(BaseAttackComponent attackComponent)
	{
		attackComponent.PlayerControllerComponent = PlayerControllerComponent;
		EmitSignal(SignalName.AttackComponentReady, attackComponent);
	}

	private void InjectPlayerControllerComponentsToChildren()
	{
		foreach (var node in GetChildren())
		{
			if (node is BaseAttackComponent baseAttackComponent)
			{
				InjectPlayerControllerComponent(baseAttackComponent);
			}
		}
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

