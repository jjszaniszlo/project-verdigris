using Godot;
using Scenes.Components.Actor;
using System;

public partial class BaseAttackComponent : Area2D
{
	public PlayerControllerComponent PlayerControllerComponent { get; set; }

	[Export]
	public PackedScene ModifierComponent { get; private set; }
	[Export]
	public float Damage { get; set; } = 30f;

	[Export]
	public float DamageMultiplier { get; set; } = 1f;

	public float CurrentDamage => Damage * DamageMultiplier;

	public virtual float TotalDamage => CurrentDamage;

	private float _Cooldown = 3.0f;
	[Export]
	public float Cooldown
	{
		get => _Cooldown;
		set
		{
			_Cooldown = value;
			if (AttackTimer != null)
			{
				AttackTimer.WaitTime = _Cooldown;
			}
		}
	}

	public Timer AttackTimer { get; private set; } = new();

	public CollisionShape2D CollisionShape2D { get; private set; }

	[Signal]
	public delegate void DoDamageEventHandler();

	public override void _Ready()
	{
		CollisionShape2D = GetNodeOrNull<CollisionShape2D>("CollisionShape2D");

		AttackTimer.WaitTime = Cooldown;
		AttackTimer.ProcessCallback = Timer.TimerProcessCallback.Physics;
		AttackTimer.OneShot = false;
		AttackTimer.Autostart = true;
		AttackTimer.Timeout += Attack;
		AddChild(AttackTimer);
	}

	public void ApplyDamage()
	{
		var hurtboxes = GetOverlappingAreas();
		foreach (var area in hurtboxes)
		{
			if (area is HurtboxComponent hurtbox)
			{
				hurtbox.Damage(TotalDamage);
			}
		}
	}

	public virtual void Attack()
	{
		ApplyDamage();
	}
}
