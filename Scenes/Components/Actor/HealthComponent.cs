using Godot;

namespace Scenes.Components.Actor;

public partial class HealthComponent : Node
{
	[Export]
	public float MaxHealth { get; private set; } = 100;
	[Export]
	public float CurrentHealth { get; private set; }


	public bool IsHealthy => CurrentHealth > 0;
	public bool IsDamaged => CurrentHealth < CurrentMaxHealth;
	public float CurrentHealthRatio => CurrentHealth / CurrentMaxHealth;

	private float _HealthMultiplier = 1f;
	public float HealthMultiplier
	{
		get => _HealthMultiplier;
		set
		{
			_HealthMultiplier = value;
			EmitSignal(SignalName.HealthChanged, new HealthChangedContext
			{
				CurrentHealth = CurrentHealth,
				MaxHealth = CurrentMaxHealth,
				CurrentHealthRatio = CurrentHealthRatio,
				IsHeal = false
			});
		}
	}
	public float CurrentMaxHealth => MaxHealth * HealthMultiplier;


	[Signal]
	public delegate void HealthChangedEventHandler(HealthChangedContext healthChangedContext);
	[Signal]
	public delegate void DiedEventHandler();

	public override void _Ready()
	{
		CurrentHealth = CurrentMaxHealth;
		GD.Print($"HealthComponent ready with MaxHealth: {CurrentMaxHealth}, CurrentHealth: {CurrentHealth}, HealthRatio: {CurrentHealthRatio}");
	}

	public void Damage(float amount)
	{
		CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
		EmitSignal(SignalName.HealthChanged, new HealthChangedContext
		{
			CurrentHealth = CurrentHealth,
			MaxHealth = CurrentMaxHealth,
			CurrentHealthRatio = CurrentHealthRatio,
			IsHeal = false
		});

		if (CurrentHealth == 0)
		{
			EmitSignal(SignalName.Died);
		}
	}

	public void Heal(float amount)
	{
		if (CurrentHealth == CurrentMaxHealth || CurrentHealth == CurrentHealth + amount) return;

		CurrentHealth = Mathf.Min(CurrentHealth + amount, CurrentMaxHealth);
		EmitSignal(SignalName.HealthChanged, new HealthChangedContext
		{
			CurrentHealth = CurrentHealth,
			MaxHealth = CurrentMaxHealth,
			CurrentHealthRatio = CurrentHealthRatio,
			IsHeal = true
		});
	}

	public void SetHealthMultiplier(float multiplier)
	{
		HealthMultiplier = multiplier;
		CurrentHealth = Mathf.Min(CurrentHealth, CurrentMaxHealth);
		EmitSignal(SignalName.HealthChanged, new HealthChangedContext
		{
			CurrentHealth = CurrentHealth,
			MaxHealth = CurrentMaxHealth,
			CurrentHealthRatio = CurrentHealthRatio,
			IsHeal = false
		});
	}

	public partial class HealthChangedContext : Resource
	{
		public float CurrentHealth { get; set; }
		public float MaxHealth { get; set; }
		public float CurrentHealthRatio { get; set; }
		public bool IsHeal { get; set; }
	};
}
