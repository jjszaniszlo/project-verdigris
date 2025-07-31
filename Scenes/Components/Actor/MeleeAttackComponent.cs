using Godot;
using Scenes.Components.Actor;
using System;
using System.Runtime.CompilerServices;

public partial class MeleeAttackComponent : Area2D
{
	[Export]
	public int Damage { get; set; } = 10;

	private float _AttackCooldown = 1.0f;
	[Export]
	public float AttackCooldown
	{
		get => _AttackCooldown;
		set
		{
			_AttackCooldown = value;
			if (_AttackTimer != null)
			{
				_AttackTimer.WaitTime = _AttackCooldown;
			}
		}
	}

	public CollisionShape2D AttackShape { get; private set; }

	private Timer _AttackTimer = new();

	public override void _Ready()
	{
		AttackShape = GetNodeOrNull<CollisionShape2D>("CollisionShape2D");
		if (AttackShape == null)
		{
			GD.PrintErr("MeleeAttackComponent requires a CollisionShape2D child node.");
		}

		_AttackTimer.WaitTime = AttackCooldown;
		_AttackTimer.ProcessCallback = Timer.TimerProcessCallback.Physics;
		_AttackTimer.OneShot = false;
		_AttackTimer.Autostart = true;
		_AttackTimer.Timeout += Attack;
		AddChild(_AttackTimer);
	}

	private void Attack()
	{
		var hurtboxes = GetOverlappingAreas();
		foreach (var hurtbox in hurtboxes)
		{
			if (hurtbox is HurtboxComponent hurtboxComponent)
			{
				hurtboxComponent.Damage(Damage);
			}
		}
	}
}
