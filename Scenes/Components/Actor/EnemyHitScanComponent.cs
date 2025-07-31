using Godot;
using GodotUtilities;
using Scenes.Components.Actor;

[Scene]
public partial class EnemyHitScanComponent : RayCast2D
{

	[Export]
	public float Range = 200f;

	public Vector2 DirectionToPlayer => (Globals.Instance.Player?.GlobalPosition - new Vector2(0, 16) - GlobalPosition ?? Vector2.Up).Normalized();

	public Vector2 GetHitPosition()
	{
		if (IsColliding())
		{
			return GetCollisionPoint();
		}
		return GlobalPosition + DirectionToPlayer * Range;
	}

	public HitScanResult GetHitScanResult()
	{
		Vector2 hitPosition = GetHitPosition();
		HurtboxComponent hurtbox = GetCollider() as HurtboxComponent;
		return new HitScanResult(hitPosition, hurtbox);
	}

	public class HitScanResult
	{
		public Vector2 HitPosition { get; }
		public HurtboxComponent Hurtbox { get; }
		public bool IsHit => Hurtbox != null;

		public HitScanResult(Vector2 hitPosition, HurtboxComponent hurtbox)
		{
			HitPosition = hitPosition;
			Hurtbox = hurtbox;
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

