using System;
using Godot;

[GlobalClass]
public partial class FireworkUpgrade : BaseUpgrade, IUpgradeRandom
{
    [Export]
    public float FireworkExplosionRadiusIncrease { get; set; }
    [Export]
    public float FireworkExplosionDamageIncrease { get; set; }
    [Export]
    public float FireworkProjectileDamageIncrease { get; set; }
    [Export]
    public float FireworkCooldownReduction { get; set; }

    public static FireworkUpgrade CreateRandomExplosionDamageUpgrade(float minDamage = 5, float maxDamage = 10)
    {
        var background = GD.Load<Texture2D>("res://Resources/Images/GameUI/Upgrades/firework_uprade.png");
        return new FireworkUpgrade
        {
            UpgradeName = "Firework Explosion Damage",
            Description = "Increases the explosion damage of firework projectiles.",
            FireworkExplosionDamageIncrease = (GD.Randf() * (maxDamage - minDamage) + minDamage) / 100.0f,
            FireworkExplosionRadiusIncrease = 0,
            FireworkProjectileDamageIncrease = 0,
            FireworkCooldownReduction = 0,
            CardBackground = background
        };
    }
    public static FireworkUpgrade CreateRandomExplosionRadiusUpgrade(float minRadius = 2, float maxRadius = 6)
    {
        var background = GD.Load<Texture2D>("res://Resources/Images/GameUI/Upgrades/firework_uprade.png");
        return new FireworkUpgrade
        {
            UpgradeName = "Firework Explosion Radius",
            Description = "Increases the explosion radius of firework projectiles.",
            FireworkExplosionDamageIncrease = 0,
            FireworkExplosionRadiusIncrease = (GD.Randf() * (maxRadius - minRadius) + minRadius)/100.0f,
            FireworkProjectileDamageIncrease = 0,
            FireworkCooldownReduction = 0,
            CardBackground = background
        };
    }
    public static FireworkUpgrade CreateRandomProjectileDamageUpgrade(float minDamage = 10, float maxDamage = 15)
    {
        var background = GD.Load<Texture2D>("res://Resources/Images/GameUI/Upgrades/firework_uprade.png");
        return new FireworkUpgrade
        {
            UpgradeName = "Firework Projectile Damage",
            Description = "Increases the damage of firework projectiles.",
            FireworkExplosionDamageIncrease = 0,
            FireworkExplosionRadiusIncrease = 0,
            FireworkProjectileDamageIncrease = (GD.Randf() * (maxDamage - minDamage) + minDamage)/100.0f,
            FireworkCooldownReduction = 0,
            CardBackground = background
        };
    }
    public static FireworkUpgrade CreateRandomExplosionUpgrade(float minDamage = 5, float maxDamage = 10, float minRadius = 1, float maxRadius = 5)
    {
        var background = GD.Load<Texture2D>("res://Resources/Images/GameUI/Upgrades/firework_upgrade_plus.png");
        return new FireworkUpgrade
        {
            UpgradeName = "Firework Explosion Upgrade",
            Description = "Randomly increases firework explosion damage or radius.",
            FireworkExplosionDamageIncrease = (GD.Randf() * (maxDamage - minDamage) + minDamage)/100.0f,
            FireworkExplosionRadiusIncrease = (GD.Randf() * (maxRadius - minRadius) + minRadius)/100.0f,
            FireworkProjectileDamageIncrease = 0,
            FireworkCooldownReduction = 0,
            CardBackground = background
        };
    }
    public static FireworkUpgrade CreateRandomDamageUpgrade(float minExplosionDamage = 5, float maxExplosionDamage = 10, float minProjDamage = 5, float maxProjDamage = 10)
    {
        var background = GD.Load<Texture2D>("res://Resources/Images/GameUI/Upgrades/firework_upgrade_plus.png");
        return new FireworkUpgrade
        {
            UpgradeName = "Firework Damage Upgrade",
            Description = "Randomly increases firework damage.",
            FireworkExplosionDamageIncrease = (GD.Randf() * (maxExplosionDamage - minExplosionDamage) + minExplosionDamage)/100.0f,
            FireworkExplosionRadiusIncrease = 0,
            FireworkProjectileDamageIncrease = (GD.Randf() * (maxProjDamage - minProjDamage) + minProjDamage)/100.0f,
            FireworkCooldownReduction = 0,
            CardBackground = background
        };
    }
    public static FireworkUpgrade CreateRandomCooldownReductionPlusExplosionDamageUpgrade(float minDamage = 5, float maxDamage = 10, float minCooldownReduction = 1, float maxCooldownReduction = 3)
    {
        var background = GD.Load<Texture2D>("res://Resources/Images/GameUI/Upgrades/firework_upgrade_plus.png");
        return new FireworkUpgrade
        {
            UpgradeName = "Firework Explosion Damage",
            Description = "Increases the explosion damage of firework projectiles.",
            FireworkExplosionDamageIncrease = (GD.Randf() * (maxDamage - minDamage) + minDamage) / 100.0f,
            FireworkExplosionRadiusIncrease = 0,
            FireworkProjectileDamageIncrease = 0,
            FireworkCooldownReduction = (GD.Randf() * (maxCooldownReduction - minCooldownReduction) + minCooldownReduction) / 100.0f,
            CardBackground = background
        };
    }

    public static FireworkUpgrade CreateRandomCooldownReductionPlusExplosionRadiusUpgrade(float minRadius = 2, float maxRadius = 6, float minCooldownReduction = 1, float maxCooldownReduction = 3)
    {
        var background = GD.Load<Texture2D>("res://Resources/Images/GameUI/Upgrades/firework_upgrade_plus.png");
        return new FireworkUpgrade
        {
            UpgradeName = "Firework Explosion Radius",
            Description = "Increases the explosion radius of firework projectiles.",
            FireworkExplosionDamageIncrease = 0,
            FireworkExplosionRadiusIncrease = (GD.Randf() * (maxRadius - minRadius) + minRadius)/100.0f,
            FireworkProjectileDamageIncrease = 0,
            FireworkCooldownReduction = (GD.Randf() * (maxCooldownReduction - minCooldownReduction) + minCooldownReduction) / 100.0f,
            CardBackground = background
        };
    }
    public static FireworkUpgrade CreateRandomCooldownReductionPlusProjectileDamageUpgrade(float minDamage = 10, float maxDamage = 15, float minCooldownReduction = 1, float maxCooldownReduction = 3)
    {
        var background = GD.Load<Texture2D>("res://Resources/Images/GameUI/Upgrades/firework_upgrade_plus.png");
        return new FireworkUpgrade
        {
            UpgradeName = "Firework Projectile Damage",
            Description = "Increases the damage of firework projectiles.",
            FireworkExplosionDamageIncrease = 0,
            FireworkExplosionRadiusIncrease = 0,
            FireworkProjectileDamageIncrease = (GD.Randf() * (maxDamage - minDamage) + minDamage)/100.0f,
            FireworkCooldownReduction = (GD.Randf() * (maxCooldownReduction - minCooldownReduction) + minCooldownReduction) / 100.0f,
            CardBackground = background
        };
    }
    public static FireworkUpgrade CreateRandomCooldownReductionPlusExplosionUpgrade(float minDamage = 5, float maxDamage = 10, float minRadius = 1, float maxRadius = 5, float minCooldownReduction = 1, float maxCooldownReduction = 3)
    {
        var background = GD.Load<Texture2D>("res://Resources/Images/GameUI/Upgrades/firework_upgrade_plus.png");
        return new FireworkUpgrade
        {
            UpgradeName = "Firework Explosion Upgrade",
            Description = "Randomly increases firework explosion damage or radius.",
            FireworkExplosionDamageIncrease = (GD.Randf() * (maxDamage - minDamage) + minDamage)/100.0f,
            FireworkExplosionRadiusIncrease = (GD.Randf() * (maxRadius - minRadius) + minRadius)/100.0f,
            FireworkProjectileDamageIncrease = 0,
            FireworkCooldownReduction = (GD.Randf() * (maxCooldownReduction - minCooldownReduction) + minCooldownReduction) / 100.0f,
            CardBackground = background
        };
    }
    public static FireworkUpgrade CreateRandomCooldownReductionPlusDamageUpgrade(float minExplosionDamage = 5, float maxExplosionDamage = 10, float minProjDamage = 5, float maxProjDamage = 10, float minCooldownReduction = 1, float maxCooldownReduction = 3)
    {
        var background = GD.Load<Texture2D>("res://Resources/Images/GameUI/Upgrades/firework_upgrade_plus.png");
        return new FireworkUpgrade
        {
            UpgradeName = "Firework Damage Upgrade",
            Description = "Randomly increases firework damage.",
            FireworkExplosionDamageIncrease = (GD.Randf() * (maxExplosionDamage - minExplosionDamage) + minExplosionDamage)/100.0f,
            FireworkExplosionRadiusIncrease = 0,
            FireworkProjectileDamageIncrease = (GD.Randf() * (maxProjDamage - minProjDamage) + minProjDamage)/100.0f,
            FireworkCooldownReduction = (GD.Randf() * (maxCooldownReduction - minCooldownReduction) + minCooldownReduction) / 100.0f,
            CardBackground = background
        };
    }

    public static Resource CreateRandom()
    {
        var randomChoice = Math.Abs(GD.Randi()) % 10;
        return randomChoice switch
        {
            0 => CreateRandomExplosionDamageUpgrade(),
            1 => CreateRandomExplosionRadiusUpgrade(),
            2 => CreateRandomProjectileDamageUpgrade(),
            3 => CreateRandomExplosionUpgrade(),
            4 => CreateRandomDamageUpgrade(),
            5 => CreateRandomCooldownReductionPlusExplosionDamageUpgrade(),
            6 => CreateRandomCooldownReductionPlusExplosionRadiusUpgrade(),
            7 => CreateRandomCooldownReductionPlusProjectileDamageUpgrade(),
            8 => CreateRandomCooldownReductionPlusExplosionUpgrade(),
            9 => CreateRandomCooldownReductionPlusDamageUpgrade(),
            _ => throw new System.NotImplementedException("Unexpected random choice for FireworkUpgrade creation.")
        };
    }

    public override void ApplyToUpgradeCardUIComponent(Card card)
    {
        card.TextComponent.Text = (FireworkExplosionDamageIncrease > 0 ? $"Exp. Dmg: +{FireworkExplosionDamageIncrease * 100:F1}%\n" : "") +
                                  (FireworkExplosionRadiusIncrease > 0 ? $"Exp. Radius: +{FireworkExplosionRadiusIncrease * 100:F1}%\n" : "") +
                                  (FireworkProjectileDamageIncrease > 0 ? $"Proj. Dmg: +{FireworkProjectileDamageIncrease * 100:F1}%" : "") +
                                  (FireworkCooldownReduction > 0 ? $"\nFire Rate: +{FireworkCooldownReduction * 100:F1}%" : "");
        card.TextureButtonComponent.TextureNormal = CardBackground;
    }
}