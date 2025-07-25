using Godot;
using GodotUtilities;
using GodotUtilities.Logic;
using Scenes.Components.Actor;

namespace Scenes.Actors;

[Scene]
public partial class Crab : CharacterBody2D
{
	[Node]
	public PathfindComponent PathfindComponent { get; private set; }
	[Node]
	public VelocityComponent VelocityComponent { get; private set; }

	private DelegateStateMachine StateMachine = new();

	public override void _Ready()
	{
		StateMachine.AddStates(StateChasePlayer);
		StateMachine.SetInitialState(StateChasePlayer);
		
		var health = GetNode<HealthComponent>("HealthComponent"); 
	health.Died += Die;
	}

	public override void _PhysicsProcess(double delta)
	{
		StateMachine.Update();
	}

	public void StateChasePlayer()
	{
		PathfindComponent.SetTargetPosition(Globals.Instance.Player?.GlobalPosition ?? GlobalPosition);
		PathfindComponent.FollowPath();
		VelocityComponent.Move(this);
	}

public void Die()
{
	var player = GetTree().Root.GetNode("Main/Player"); 
	var levelComponent = player.GetNode<LevelComponent>("LevelComponent"); 

	levelComponent?.GainExperience(5); 

	QueueFree();
}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}
