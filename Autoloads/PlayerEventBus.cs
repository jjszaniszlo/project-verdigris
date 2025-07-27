using Godot;
using System;

public partial class PlayerEventBus : Node
{
	public static PlayerEventBus Instance { get; private set; }

	[Signal]
	public delegate void PlayerDiedEventHandler();

	[Signal]
	public delegate void PlayerHealthChangedEventHandler(int newHealth, int newMaxHealth);

	[Signal]
	public delegate void PlayerExperienceChangedEventHandler(int newExperience, int newExperienceToNextLevel);
	
	public override void _Ready()
	{
		Instance = this;
	}

	public void EmitPlayerDied()
	{
		EmitSignal(SignalName.PlayerDied);
	}

	public void EmitPlayerHealthChanged(int newHealth, int newMaxHealth)
	{
		EmitSignal(SignalName.PlayerHealthChanged, newHealth, newMaxHealth);
	}

	public void EmitPlayerExperienceChanged(int newExperience, int newExperienceToNextLevel)
	{
		EmitSignal(SignalName.PlayerExperienceChanged, newExperience, newExperienceToNextLevel);
	}
}
