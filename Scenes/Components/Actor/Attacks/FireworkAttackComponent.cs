using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Scenes.Components.Actor;

public partial class FireworkAttackComponent : BaseAttackComponent
{
	[Export]
	public PackedScene FireworkEffect { get; private set; }

	[Export]
	public float FireworkProjectileDamage { get; private set; } = 20f;

	[Export]
	public float FireworkProjectileDamageMultiplier { get; set; } = 1f;

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
		GlobalPosition = PlayerControllerComponent.AimingAt;
	}

	public override void Attack()
	{
		var fireworkEffect = FireworkEffect.Instantiate<Node2D>();
		fireworkEffect.GlobalPosition = PlayerControllerComponent.AimingAt;

		Globals.Instance.ScreenShake.PlayShake();
		Globals.Instance.EffectsManager.AddEffect(fireworkEffect);

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
