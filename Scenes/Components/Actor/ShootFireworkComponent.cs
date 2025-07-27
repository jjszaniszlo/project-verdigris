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
			if (_FireTimer != null)
			{
				_FireTimer.WaitTime = _FireCooldown;
			}
		}
	}

	public CollisionShape2D CollisionShape2D { get; private set; }

	private Timer _FireTimer = new();

	public override void _Ready()
	{
		CollisionShape2D = GetNodeOrNull<CollisionShape2D>("CollisionShape2D");
		if (CollisionShape2D == null)
		{
			GD.PrintErr("ShootFireworkComponent requires a CollisionShape2D child node.");
		}

		_FireTimer.WaitTime = FireCooldown;
		_FireTimer.ProcessCallback = Timer.TimerProcessCallback.Physics;
		_FireTimer.OneShot = false;
		_FireTimer.Autostart = true;
		_FireTimer.Timeout += Fire;
		AddChild(_FireTimer);
	}

	public override void _PhysicsProcess(double delta)
	{
		GlobalPosition = PlayerControllerComponent.AimingAt;

	}

	private void Fire()
	{
		var hurtboxes = GetOverlappingAreas();
		foreach (var area in hurtboxes)
		{
			if (area is HurtboxComponent hurtbox)
			{
				hurtbox.Damage(Damage);
			}
		}

		var fireworkEffect = FireworkEffect.Instantiate<Node2D>();
		fireworkEffect.GlobalPosition = PlayerControllerComponent.AimingAt;

		Globals.Instance.ScreenShake.PlayShake();
		Globals.Instance.EffectsManager.AddEffect(fireworkEffect);
	}
}
