using Godot;
using Scenes.Components.Actor;

namespace Verdigris.Scenes.Components.Actor.Modifiers;

public partial class ActorStatModifier : Node, IModifier
{
    [Export]
    public HealthComponent HealthComponent { get; private set; }
    [Export]
    public VelocityComponent VelocityComponent { get; private set; }

    public void AddToHealthMultiplier(float amount)
    {
        HealthComponent.HealthMultiplier += amount;
        GD.Print($"Adding to health multiplier: {amount}, new multiplier: {HealthComponent.HealthMultiplier}");
    }

    public void AddToHealthMultiplerAndHeal(float amount, float healAmount)
    {
        HealthComponent.HealthMultiplier += amount;
        HealthComponent.Heal(healAmount);
        GD.Print($"Adding to health multiplier: {amount}, healing for {healAmount}, new multiplier: {HealthComponent.HealthMultiplier}");
    }

    public void AddToSpeedMultiplier(float amount)
    {
        VelocityComponent.SpeedMultiplier += amount;
        VelocityComponent.AccelerationMultiplier += amount;
        GD.Print($"Adding to speed multiplier: {amount}, new speed multiplier: {VelocityComponent.SpeedMultiplier}, new acceleration multiplier: {VelocityComponent.AccelerationMultiplier}");
    }

    public Resource GetRandomUpgradeResource()
    {
        return ActorStatUpgrade.CreateRandom();
    }

    public void ApplyUpgrade(Resource upgradeResource)
    {
        if (upgradeResource is ActorStatUpgrade actorStatUpgrade)
        {
            AddToHealthMultiplerAndHeal(actorStatUpgrade.MaxHealthIncrease, actorStatUpgrade.Heal);
            AddToSpeedMultiplier(actorStatUpgrade.SpeedIncrease);
        }
    }
}