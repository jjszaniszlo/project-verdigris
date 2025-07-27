using System;
using Godot;
using GodotUtilities;
using Scenes.Components.Actor;

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

	public override void _Ready()
	{
		Globals.Instance.Player = this;

		HealthComponent.HealthChanged += OnHealthChanged;
		LevelComponent.LevelChanged += OnLevelChanged;

		PlayerEventBus.Instance.EmitPlayerHealthChanged(HealthComponent.CurrentHealth, HealthComponent.MaxHealth);
		PlayerEventBus.Instance.EmitPlayerExperienceChanged(LevelComponent.Experience, LevelComponent.ExperienceToNextLevel);
	}

    private void OnHealthChanged(HealthComponent.HealthChangedContext healthChangedContext)
    {
		PlayerEventBus.Instance.EmitPlayerHealthChanged(healthChangedContext.CurrentHealth, healthChangedContext.MaxHealth);
    }

    private void OnLevelChanged(int level, int experience, int experienceToNextLevel)
    {
        PlayerEventBus.Instance.EmitPlayerExperienceChanged(experience, experienceToNextLevel);
    }

    public override void _Notification(int what)
    {
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }
}
