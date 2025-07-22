using Godot;
using System;

public partial class HealthComponent : Node
{
	[Export] public int MaxHealth { get; set; } = 100;
	public int CurrentHealth { get; private set; }

	[Signal]
	public delegate void HealthChangedEventHandler(int current, int max);

	[Signal]
	public delegate void DiedEventHandler();

	public override void _Ready()
	{
		CurrentHealth = MaxHealth;
	}

	public void TakeDamage(int amount)
	{
		CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);

		if (CurrentHealth == 0)
		{
			EmitSignal(SignalName.Died);
		}
	}

	public void Heal(int amount)
	{
		CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);
	}

	public void SetMaxHealth(int max)
	{
		MaxHealth = max;
		CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);
	}
}
