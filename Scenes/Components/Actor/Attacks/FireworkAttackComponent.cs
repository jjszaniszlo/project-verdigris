using System.Threading.Tasks;
using Godot;
using GodotUtilities;
using Scenes.Components.Actor;

[Scene]
public partial class FireworkAttackComponent : BaseAttackComponent
{
	[Export]
	public PackedScene FireworkEffect { get; private set; }
	[Export]
	public PackedScene FireworkProjectileEffect { get; private set; }

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
		GlobalPosition = HitScanComponent.GetHitPosition();
	}

    public override void _Notification(int what)
    {
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }

	public override async void Attack()
	{
		var fireworkEffect = FireworkEffect.Instantiate<Node2D>();
		fireworkEffect.GlobalPosition = GlobalPosition;

		var fireworkProjectileEffect = FireworkProjectileEffect.Instantiate<ShootEffect>();
		Globals.Instance.EffectsManager.AddEffect(fireworkProjectileEffect);
		var hurtboxProjectile = HitScanComponent.GetCollidiingHurtBox();
		await fireworkProjectileEffect.Shoot(HitScanComponent.GlobalPosition, GlobalPosition, 300f);

		hurtboxProjectile?.Damage(CurrentFireworkProjectileDamage);

		Globals.Instance.ScreenShake.PlayShake();
		Globals.Instance.EffectsManager.AddEffect(fireworkEffect);
		FireworkSound.Play();

		ApplyDamage();
	}

	private void SetExplosionRadius(float radius)
	{
		if (CollisionShape2D != null && CollisionShape2D.Shape is CircleShape2D circleShape2D)
		{
			circleShape2D.Radius = radius;
		}
	}
}
