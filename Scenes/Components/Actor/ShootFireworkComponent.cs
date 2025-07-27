using System;
using System.Collections;
using System.Collections.Generic;
using Godot;
using Scenes.Components.Actor;

public partial class ShootFireworkComponent : Area2D
{
	[Export]
	public PlayerControllerComponent PlayerControllerComponent { get; private set; }
	[Export]
	public PackedScene FireworkEffect { get; private set; }

	[Export]
	public int Damage { get; set; } = 10;

	public float _FireCooldown = 2.0f;
	[Export]
	public float FireCooldown
	{
		get => _FireCooldown;
		set
		{
			_FireCooldown = value;
			if (_fireTimer != null)
			{
				_fireTimer.WaitTime = _FireCooldown;
			}
		}
	}

	public CollisionShape2D CollisionShape2D { get; private set; }

	private Timer _fireTimer = new();

	public override void _Ready()
	{
		CollisionShape2D = GetNodeOrNull<CollisionShape2D>("CollisionShape2D");
		if (CollisionShape2D == null)
		{
			GD.PrintErr("ShootFireworkComponent requires a CollisionShape2D child node.");
		}

		_fireTimer.WaitTime = FireCooldown;
		_fireTimer.ProcessCallback = Timer.TimerProcessCallback.Physics;
		_fireTimer.OneShot = false;
		_fireTimer.Autostart = true;
		_fireTimer.Timeout += Fire;
		AddChild(_fireTimer);
	}

	private bool _isFiring = false;

	public override void _PhysicsProcess(double delta)
	{
		GlobalPosition = PlayerControllerComponent.AimingAt;

		if (_isFiring)
		{
			HurtActorsInArea();
			_isFiring = false;
		}
	}

	public void Fire()
	{
		if (_isFiring) return;
		_isFiring = true;

		var fireworkEffect = FireworkEffect.Instantiate<Node2D>();
		fireworkEffect.GlobalPosition = PlayerControllerComponent.AimingAt;

		Globals.Instance.ScreenShake.PlayShake();
		Globals.Instance.EffectsManager.AddEffect(fireworkEffect);
	}

	private void HurtActorsInArea()
	{
		var hurtboxes = GetOverlappingAreas();
		foreach (var area in hurtboxes)
		{
			if (area is HurtboxComponent hurtbox)
			{
				hurtbox.Damage(Damage);
			}
		}
	}
}
