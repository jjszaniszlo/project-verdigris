using Godot;
using System;

public partial class PlayerEventSubscriberComponent : Node
{
	[Export]
	public TextureProgressBar HealthBar;

	[Export]
	public TextureProgressBar ExperienceBar;

	public override void _Ready()
	{
		PlayerEventBus.Instance.PlayerHealthChanged += OnPlayerHealthChanged;
		PlayerEventBus.Instance.PlayerExperienceChanged += OnPlayerExperienceChanged;
	}

	private void OnPlayerExperienceChanged(int newExperience, int newExperienceToNextLevel)
	{
		ExperienceBar.Value = newExperience;
		ExperienceBar.MaxValue = newExperienceToNextLevel;
    }

	private void OnPlayerHealthChanged(int newHealth, int newMaxHealth)
	{
		HealthBar.Value = newHealth;
		HealthBar.MaxValue = newMaxHealth;
    }
}
