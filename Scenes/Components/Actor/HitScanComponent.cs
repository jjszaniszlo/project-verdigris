using Godot;
using Scenes.Components.Actor;

public partial class HitScanComponent : RayCast2D
{
	[Export]
	public PlayerControllerComponent PlayerControllerComponent { get; private set; }

	public override void _PhysicsProcess(double delta)
	{
		TargetPosition = PlayerControllerComponent.AimingAt - GlobalPosition;
	}

	public Vector2 GetHitPosition()
	{
		if (IsColliding())
		{
			return GetCollisionPoint();
		}
		return PlayerControllerComponent.AimingAt;
	}

	public HurtboxComponent GetCollidiingHurtBox()
	{
		return GetCollider() as HurtboxComponent;
	}
}

