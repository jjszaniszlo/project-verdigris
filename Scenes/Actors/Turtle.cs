using Godot;
using GodotUtilities;
using GodotUtilities.Logic;
using Scenes.Components.Actor;

[Scene]
public partial class Turtle : CharacterBody2D 
{
	[Node]
	public PathfindComponent PathfindComponent { get; private set; }
	[Node]
	public VelocityComponent VelocityComponent { get; private set; }
	[Node]
	public AnimatedActorComponent AnimatedActorComponent { get; private set; }

	[Export]
	public float PlayerFollowDistance = 60f;

	private DelegateStateMachine StateMachine = new();

	private Vector2 Facing => ((Globals.Instance.Player?.GlobalPosition - GlobalPosition) ?? Vector2.Up).Normalized();
	private bool FacingUp => Facing.Dot(Vector2.Up) > Facing.Dot(Vector2.Right) && Facing.Dot(Vector2.Up) > Facing.Dot(Vector2.Left);
	private bool FacingDown => Facing.Dot(Vector2.Down) > Facing.Dot(Vector2.Right) && Facing.Dot(Vector2.Down) > Facing.Dot(Vector2.Left);
	private bool FacingLeft => Facing.Dot(Vector2.Left) > Facing.Dot(Vector2.Up) && Facing.Dot(Vector2.Left) > Facing.Dot(Vector2.Down);
	private bool FacingRight => Facing.Dot(Vector2.Right) > Facing.Dot(Vector2.Up) && Facing.Dot(Vector2.Right) > Facing.Dot(Vector2.Down);

	public override void _Ready()
	{
		StateMachine.AddStates(StateChasePlayer);
		StateMachine.SetInitialState(StateChasePlayer);
	}

	public override void _PhysicsProcess(double delta)
	{
		StateMachine.Update();
		if (FacingUp)
		{
			AnimatedActorComponent.PlayMoveAway();
		}
		else if (FacingDown)
		{
			AnimatedActorComponent.PlayMoveTowards();
		}
		else if (FacingLeft)
		{
			AnimatedActorComponent.PlayMoveLeft();
		}
		else if (FacingRight)
		{
			AnimatedActorComponent.PlayMoveRight();
		}
	}

	public void StateChasePlayer()
	{
		Vector2 playerPosition = Globals.Instance.Player?.GlobalPosition ?? GlobalPosition;
		Vector2 turtlePosition = GlobalPosition;
		Vector2 directionToPlayer = (playerPosition - turtlePosition).Normalized();
		Vector2 targetPosition = playerPosition - directionToPlayer * PlayerFollowDistance;

		if (targetPosition.DistanceTo(playerPosition) < PlayerFollowDistance)
		{
			targetPosition = turtlePosition;
		}

		PathfindComponent.SetTargetPosition(targetPosition);
		PathfindComponent.FollowPath();
		VelocityComponent.Move(this);
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

