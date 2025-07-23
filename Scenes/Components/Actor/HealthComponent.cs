using System;
using Godot;

namespace Scenes.Components.Actor;

public partial class HealthComponent : Node
{
	[Export]
	public int MaxHealth { get; private set; } = 100;

	public int CurrentHealth { get; private set; }

	public bool IsHealthy => CurrentHealth > 0;
	public bool IsDamaged => CurrentHealth < MaxHealth;
	public float CurrentHealthRatio => CurrentHealth / MaxHealth;

	[Signal]
	public delegate void HealthChangedEventHandler(HealthChangedContext healthChangedContext);
	[Signal]
	public delegate void DiedEventHandler();

	public override void _Ready()
	{
		CurrentHealth = MaxHealth;
	}

	public void Damage(int amount)
	{
		CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
		EmitSignal(SignalName.HealthChanged, new HealthChangedContext
		{
			CurrentHealth = CurrentHealth,
			MaxHealth = MaxHealth,
			CurrentHealthRatio = CurrentHealthRatio,
			IsHeal = false
		});

		if (CurrentHealth == 0)
		{
			EmitSignal(SignalName.Died);
		}
	}

	public void Heal(int amount)
	{
		CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
		EmitSignal(SignalName.HealthChanged, new HealthChangedContext
		{
			CurrentHealth = CurrentHealth,
			MaxHealth = MaxHealth,
			CurrentHealthRatio = CurrentHealthRatio,
			IsHeal = true
		});
	}

	public partial class HealthChangedContext : Resource {
		public int CurrentHealth { get; set; }
		public int MaxHealth { get; set; }
		public float CurrentHealthRatio { get; set; }
		public bool IsHeal { get; set; }
	};

	public void SetMaxHealth(int max, MaxHealthUpdateType mode = MaxHealthUpdateType.KeepCurrentHealth)
	{
		switch (mode)
		{
			case MaxHealthUpdateType.KeepCurrentHealth:
				CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
				break;
			case MaxHealthUpdateType.ResetToMaxHealth:
				CurrentHealth = max;
				break;
			case MaxHealthUpdateType.Scale:
				CurrentHealth *= max / MaxHealth;
				break;
		}

		MaxHealth = max;
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);
	}

	public enum MaxHealthUpdateType
	{
		KeepCurrentHealth,
		ResetToMaxHealth,
		Scale
	}
}
