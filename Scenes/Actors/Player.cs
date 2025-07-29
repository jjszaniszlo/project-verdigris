using System;
using Godot;
using GodotUtilities;
using Scenes.Components.Actor;
using Verdigris.Scenes.Components.Actor.Modifiers;

namespace Scenes.Actors;

[Scene]
public partial class Player : CharacterBody2D
{
	[Node]
	public LevelComponent LevelComponent { get; private set; }
	[Node]
	public HealthComponent HealthComponent { get; private set; }
	[Node]
	public DieComponent DieComponent { get; private set; }

	[Node]
	public ModifierManagerComponent ModifierManagerComponent { get; private set; }

	public override void _Ready()
	{
		Globals.Instance.Player = this;

		HealthComponent.HealthChanged += OnHealthChanged;
		LevelComponent.LevelChanged += OnLevelChanged;

		GameEventBus.Instance.GameUIReady += () =>
		{
			PlayerEventBus.Instance.EmitPlayerHealthChanged(HealthComponent.CurrentHealth, HealthComponent.CurrentMaxHealth);
			PlayerEventBus.Instance.EmitPlayerExperienceChanged(LevelComponent.Experience, LevelComponent.ExperienceToNextLevel);
		};
	}

    private void OnHealthChanged(HealthComponent.HealthChangedContext healthChangedContext)
    {
		PlayerEventBus.Instance.EmitPlayerHealthChanged(healthChangedContext.CurrentHealth, healthChangedContext.MaxHealth);
    }

    private void OnLevelChanged(int levelsGained, int experience, int experienceToNextLevel)
    {
        PlayerEventBus.Instance.EmitPlayerExperienceChanged(experience, experienceToNextLevel);
		for (int i = 0; i < levelsGained; i++) PlayerEventBus.Instance.EmitPlayerLevelUp();
    }

    public override void _Notification(int what)
    {
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }
}
