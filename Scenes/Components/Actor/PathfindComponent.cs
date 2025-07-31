using Godot;
using GodotUtilities;

namespace Scenes.Components.Actor;

[Scene]
public partial class PathfindComponent : Node2D
{
	[Export]
	public VelocityComponent VelocityComponent { get; private set; }

	[Node]
	public NavigationAgent2D NavigationAgent { get; private set; }
	[Node]
	public Timer TargetingCooldown { get; private set; }

	public override void _Ready()
	{
		NavigationAgent.VelocityComputed += OnVelocityComputed;
	}

	public void SetTargetPosition(Vector2 targetPosition)
	{
		if (!TargetingCooldown.IsStopped())
		{
			return;
		}

		NavigationAgent.SetTargetPosition(targetPosition);
		TargetingCooldown.Start();
	}

	public void SetTargedPositionForced(Vector2 targetPosition)
	{
		NavigationAgent.SetTargetPosition(targetPosition);
		TargetingCooldown.Stop();
	}

	public void FollowPath()
	{
		if (NavigationAgent.IsNavigationFinished())
		{
			VelocityComponent.MinimizeVelocity();
			return;
		}

		var direction = (NavigationAgent.GetNextPathPosition() - GlobalPosition).Normalized();

		VelocityComponent.Accelerate(direction);
		NavigationAgent.SetVelocityForced(VelocityComponent.Velocity);
	}

	public void OnVelocityComputed(Vector2 velocity)
	{
		var newDirection = velocity.Normalized();
		var currentDirection = VelocityComponent.Velocity.Normalized();
		var interpolatedDirection = newDirection.Lerp(currentDirection, 1f - Mathf.Exp(VelocityComponent.Acceleration * (float)GetProcessDeltaTime()));

		VelocityComponent.SetVelocityForced(interpolatedDirection * VelocityComponent.Speed);
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

