using System.Threading.Tasks;
using Godot;
using GodotUtilities;
using Scenes.Actors;
using Scenes.Components.Actor;

[Scene]
public partial class FireworkAttackComponent : BaseAttackComponent
{
	[Export]
	public PackedScene FireworkEffect { get; private set; }
	[Export]
	public PackedScene FireworkProjectileEffect { get; private set; }

	[Export]
	public float FireworkProjectileSpeed { get; private set; } = 200f;

	[Export]
	public float FireworkProjectileDamage { get; private set; } = 20f;

	[Export]
	public float FireworkProjectileDamageMultiplier { get; set; } = 1f;

	[Export]
	public HitScanComponent HitScanComponent { get; private set; }

	[Node]
	public AudioStreamPlayer2D FireworkSound { get; private set; }

	private float _FireworkExplosionRadius = 20f;
	[Export]
	public float FireworkExplosionRadius
	{
		get => _FireworkExplosionRadius;
		set
		{
			_FireworkExplosionRadius = value;
			SetExplosionRadius(_FireworkExplosionRadius);
		}
	}

	public float CurrentFireworkProjectileDamage => FireworkProjectileDamage * FireworkProjectileDamageMultiplier;

	public override float TotalDamage => base.TotalDamage + CurrentFireworkProjectileDamage;

	public override void _Ready()
	{
		base._Ready();
		SetExplosionRadius(FireworkExplosionRadius);
	}

	public override void _PhysicsProcess(double delta)
	{
		GlobalPosition = _CurrentFireworkProjectile?.GlobalPosition ?? GlobalPosition;
	}

    public override void _Notification(int what)
    {
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }

	private Projectile _CurrentFireworkProjectile;

	public override void Attack()
	{
		var fireworkProjectile = FireworkProjectileEffect.Instantiate<Projectile>();
		fireworkProjectile.Speed = FireworkProjectileSpeed;
		fireworkProjectile.Damage = FireworkProjectileDamage;
		fireworkProjectile.Direction = PlayerControllerComponent.AimingDirection;
		fireworkProjectile.GlobalTransform = HitScanComponent.GlobalTransform;

		var distanceToTarget = HitScanComponent.GlobalPosition.DistanceTo(PlayerControllerComponent.AimingAt);
		var lifetime = distanceToTarget / FireworkProjectileSpeed;

		fireworkProjectile.Lifetime = lifetime;
		_CurrentFireworkProjectile = fireworkProjectile;
		Globals.Instance.EffectsManager.AddEffect(fireworkProjectile);
		fireworkProjectile.LifetimeEnded += () =>
		{
			var fireworkEffect = FireworkEffect.Instantiate<Node2D>();
			fireworkEffect.GlobalPosition = fireworkProjectile.GlobalPosition;

			Globals.Instance.EffectsManager.AddEffect(fireworkEffect);

			FireworkSound.Play();
			Globals.Instance.ScreenShake.PlayShake();

			ApplyDamage();
			_CurrentFireworkProjectile = null;
		};
	}

	private void SetExplosionRadius(float radius)
	{
		if (CollisionShape2D != null && CollisionShape2D.Shape is CircleShape2D circleShape2D)
		{
			circleShape2D.Radius = radius;
		}
	}
}
