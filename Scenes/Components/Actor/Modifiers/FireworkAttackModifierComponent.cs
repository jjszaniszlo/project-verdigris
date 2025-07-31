using Godot;
using System;

public partial class FireworkAttackModifierComponent : BaseAttackModifierComponent, IModifier
{
	[Export]
	public FireworkAttackComponent FireworkAttackComponent { get; set; }

	public void AddToExplosionDamageMultiplier(float multiplier)
	{
		FireworkAttackComponent.DamageMultiplier += multiplier;
		GD.Print($"Adding to explosion damage multiplier: {multiplier}, new multiplier: {FireworkAttackComponent.DamageMultiplier}");
	}

	public void AddToProjectileDamageMultiplier(float multiplier)
	{
		FireworkAttackComponent.FireworkProjectileDamageMultiplier += multiplier;
		GD.Print($"Adding to projectile damage multiplier: {multiplier}, new multiplier: {FireworkAttackComponent.FireworkProjectileDamageMultiplier}");
	}

	public void AddToExplosionRadius(float radius)
	{
		FireworkAttackComponent.FireworkExplosionRadius += radius;
        GD.Print($"Adding to explosion radius: {radius}, new radius: {FireworkAttackComponent.FireworkExplosionRadius}");
	}

	public void AddToCooldownReduction(float reduction)
	{
		FireworkAttackComponent.CooldownReductionMultiplier += reduction;
		GD.Print($"Adding to cooldown reduction: {reduction}, new multiplier: {FireworkAttackComponent.CooldownReductionMultiplier}");
	}

	public override void InjectAttackComponent(BaseAttackComponent attackComponent)
	{
		if (attackComponent is FireworkAttackComponent fireworkAttackComponent)
		{
			FireworkAttackComponent = fireworkAttackComponent;
		}
	}

	public Resource GetRandomUpgradeResource()
	{
		return FireworkUpgrade.CreateRandom();
	}

	public void ApplyUpgrade(Resource upgradeResource)
	{
		if (upgradeResource is FireworkUpgrade fireworkUpgrade)
		{
			AddToExplosionDamageMultiplier(fireworkUpgrade.FireworkExplosionDamageIncrease);
			AddToProjectileDamageMultiplier(fireworkUpgrade.FireworkProjectileDamageIncrease);
			AddToExplosionRadius(fireworkUpgrade.FireworkExplosionRadiusIncrease);
			AddToCooldownReduction(fireworkUpgrade.FireworkCooldownReduction);
		}
	}
}
