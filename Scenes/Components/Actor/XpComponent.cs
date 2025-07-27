using Godot;
using GodotUtilities;
using Scenes.Actors;

public partial class XpComponent : Node2D
{
	[Export]
	public DieComponent DieComponent { get; private set; }

	[Export]
	public int ExperiencePoints { get; set; } = 2;

	public override void _Ready()
	{
		DieComponent.Died += OnDied;
	}

	private void OnDied()
	{
		Globals.Instance.Player.LevelComponent.GainExperience(ExperiencePoints);
	}
}

