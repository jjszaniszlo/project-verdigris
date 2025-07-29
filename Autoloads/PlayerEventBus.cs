using Godot;
using System;

public partial class PlayerEventBus : Node
{
	public static PlayerEventBus Instance { get; private set; }

	[Signal]
	public delegate void PlayerDiedEventHandler();

	[Signal]
	public delegate void PlayerHealthChangedEventHandler(float newHealth, float newMaxHealth);

	[Signal]
	public delegate void PlayerExperienceChangedEventHandler(int newExperience, int newExperienceToNextLevel);

	[Signal]
	public delegate void PlayerLevelUpEventHandler();

	public override void _Ready()
	{
		Instance = this;
	}

	public void EmitPlayerDied()
	{
		EmitSignal(SignalName.PlayerDied);
	}

	public void EmitPlayerHealthChanged(float newHealth, float newMaxHealth)
	{
		EmitSignal(SignalName.PlayerHealthChanged, newHealth, newMaxHealth);
	}

	public void EmitPlayerExperienceChanged(int newExperience, int newExperienceToNextLevel)
	{
		EmitSignal(SignalName.PlayerExperienceChanged, newExperience, newExperienceToNextLevel);
	}

	public void EmitPlayerLevelUp()
	{
		EmitSignal(SignalName.PlayerLevelUp);
	}
}
