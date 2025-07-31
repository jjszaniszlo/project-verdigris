using Godot;
using GodotUtilities;
using Scenes.Actors;

public partial class XpComponent : Node2D
{
	[Export]
	public DieComponent DieComponent { get; private set; }

	[Export]
	public int ExperiencePoints { get; set; } = 2;

	public float ExperienceMultiplier { get; set; } = 1.0f;

	public float CurrentExperiencePoints => ExperiencePoints * ExperienceMultiplier;

	public override void _Ready()
	{
		DieComponent.Died += OnDied;
	}

	private void OnDied()
	{
		Globals.Instance.Player.LevelComponent.GainExperience((int)CurrentExperiencePoints);
	}
}

