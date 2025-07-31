using Godot;
using Scenes.Components.Actor;

public partial class DifficultyModifierComponent : Node
{
	[Export]
	public HealthComponent HealthComponent { get; private set; }

	[Export]
	public float MaxHealthDifficultyScaling { get; private set; } = 0.1f;

	[Export]
	public VelocityComponent VelocityComponent { get; private set; }
	[Export]
	public float VelocityDifficultyScaling { get; private set; } = 0.01f;

	[Export]
	public BaseAttackComponent BaseAttackComponent { get; private set; }
	[Export]
	public float BaseAttackDifficultyScaling { get; private set; } = 0.02f;
	[Export]
	public float BaseAttackSpeedDifficultyScaling { get; private set; } = 0.05f;

	[Export]
	public XpComponent XpComponent { get; private set; }

	[Export]
	public float XpDifficultyScaling { get; private set; } = 1.5f;


	public void ModifyDifficulty(int difficulty)
	{
		HealthComponent.HealthMultiplier += MaxHealthDifficultyScaling * difficulty;
		VelocityComponent.SpeedMultiplier += VelocityDifficultyScaling * difficulty;
		VelocityComponent.AccelerationMultiplier += VelocityDifficultyScaling * difficulty;
		BaseAttackComponent.DamageMultiplier += BaseAttackDifficultyScaling * difficulty;
		BaseAttackComponent.CooldownReductionMultiplier += BaseAttackSpeedDifficultyScaling * difficulty;
		XpComponent.ExperienceMultiplier += XpDifficultyScaling * difficulty;
	}
}
