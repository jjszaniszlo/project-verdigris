using Godot;

[GlobalClass]
public partial class ActorStatUpgrade : BaseUpgrade, IUpgradeRandom
{
    [Export]
    public float MaxHealthIncrease { get; set; }
    [Export]
    public float Heal { get; set; }

    [Export]
    public float SpeedIncrease { get; set; }

    public static ActorStatUpgrade CreateRandomHealthUpgrade(float min = 2, float max = 6)
    {
        var background = GD.Load<Texture2D>("res://Resources/Images/GameUI/Upgrades/health_uprade.png");
        return new ActorStatUpgrade
        {
            UpgradeName = "Health Upgrade",
            Description = "Increases maximum health.",
            MaxHealthIncrease = (GD.Randf() * (max - min) + min) / 100.0f,
            Heal = 0,
            SpeedIncrease = 0,
            CardBackground = background
        };
    }

    public static ActorStatUpgrade CreateRandomSpeedUpgrade(float min = 4, float max = 8)
    {
        var background = GD.Load<Texture2D>("res://Resources/Images/GameUI/Upgrades/speed_uprade.png");
        return new ActorStatUpgrade
        {
            UpgradeName = "Speed Upgrade",
            Description = "Increases movement speed.",
            MaxHealthIncrease = 0,
            Heal = 0,
            SpeedIncrease = (GD.Randf() * (max - min) + min) / 100.0f,
            CardBackground = background
        };
    }

    public static ActorStatUpgrade CreateRandomBoost(float healthMin = 2, float healthMax = 6, float healMin = 1, float healMax = 3, float speedMin = 4, float speedMax = 8)
    {
        Texture2D background;
        float speed, health, heal;

        if (GD.Randf() < 0.5f)
        {
            background = GD.Load<Texture2D>("res://Resources/Images/GameUI/Upgrades/speed_upgrade_plus.png");
            speed = (GD.Randf() * (speedMax - speedMin) + speedMin) / 100.0f;
            health = 0;
            heal = GD.Randf() * (healMax - healMin) + healMin;
        }
        else
        {
            background = GD.Load<Texture2D>("res://Resources/Images/GameUI/Upgrades/health_upgrade_plus.png");
            health = (GD.Randf() * (healthMax - healthMin) + healthMin) / 100.0f;
            speed = 0;
            heal = GD.Randf() * (healMax - healMin) + healMin;
        }

        return new ActorStatUpgrade
        {
            UpgradeName = "Boost Upgrade",
            Description = "Increases health or speed and heals.",
            MaxHealthIncrease = health,
            Heal = heal,
            SpeedIncrease = speed,
            CardBackground = background
        };
    }

    public override void ApplyToUpgradeCardUIComponent(Card card) 
    {
        card.TextComponent.Text = (MaxHealthIncrease > 0 ? $"Max HP: +{MaxHealthIncrease*100:F1}%\n" : "") +
            (Heal > 0 ? $"Heal: +{Heal:F0} HP\n" : "") +
            (SpeedIncrease > 0 ? $"Speed: +{SpeedIncrease*100:F1}%" : "");
        card.TextureButtonComponent.TextureNormal = CardBackground;
    }

    public static Resource CreateRandom()
    {
        float rand = GD.Randf();
        if (rand < 0.33f)
        {
            return CreateRandomHealthUpgrade();
        }
        else if (rand < 0.66f)
        {
            return CreateRandomSpeedUpgrade();
        }
        else
        {
            return CreateRandomBoost();
        }
    }
}