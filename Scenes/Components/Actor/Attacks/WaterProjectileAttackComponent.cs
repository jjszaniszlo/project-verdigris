using Godot;
using GodotUtilities;

[Scene]
public partial class WaterProjectileAttackComponent : BaseAttackComponent 
{
	[Export]
	public EnemyHitScanComponent EnemyHitScanComponent { get; private set; }
	[Export]
	public PackedScene WaterProjectileScene { get; private set; }
	
	[Export]
	public float WaterProjectileSpeed { get; private set; } = 200f;

	[Export]
	public float WaterProjectileDamage { get; private set; } = 10f;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void Attack()
	{
		var waterProjectile = WaterProjectileScene.Instantiate<Projectile>();
        waterProjectile.Speed = WaterProjectileSpeed;
        waterProjectile.Damage = WaterProjectileDamage;
        waterProjectile.Direction = EnemyHitScanComponent.DirectionToPlayer;
        waterProjectile.GlobalTransform = GlobalTransform;

        Globals.Instance.EffectsManager.AddEffect(waterProjectile);
	}
}

