using Godot;
using System;

public partial class PlayerEventSubscriberComponent : Node
{
	[Export]
	public TextureProgressBar HealthBar;

	[Export]
	public TextureProgressBar ExperienceBar;

	[Export]
	public Label DebugLabel;

	public override void _Ready()
	{
		PlayerEventBus.Instance.PlayerHealthChanged += OnPlayerHealthChanged;
		PlayerEventBus.Instance.PlayerExperienceChanged += OnPlayerExperienceChanged;

		GameEventBus.Instance.EmitGameUIReady();
	}

	private void OnPlayerExperienceChanged(int newExperience, int newExperienceToNextLevel)
	{
		ExperienceBar.Value = newExperience;
		ExperienceBar.MaxValue = newExperienceToNextLevel;
		UpdateLabel();
	}

	private void OnPlayerHealthChanged(float newHealth, float newMaxHealth)
	{
		HealthBar.Value = newHealth;
		HealthBar.MaxValue = newMaxHealth;
		UpdateLabel();
	}

	public void UpdateLabel()
	{
		if (DebugLabel == null) return;

		DebugLabel.Text = $"Health: {HealthBar.Value}/{HealthBar.MaxValue}\n" +
		                  $"Experience: {ExperienceBar.Value}/{ExperienceBar.MaxValue}";
	}
}
