using Godot;
using Godot.Collections;
using GodotUtilities;
using System;
using System.Collections.Generic;
using Verdigris.Scenes.Components.Actor.Modifiers;

public partial class ModifierManagerComponent : Node
{
	[Export]
	public AttackManagerComponent AttackManagerComponent { get; set; }

	public ActorStatModifier ActorStatModifier { get; private set; }

	public override void _Ready()
	{
		if (AttackManagerComponent == null)
		{
			GD.PrintErr("AttackManagerComponent is not set in AttackModifierManagerComponent.");
			return;
		}
		ActorStatModifier = GetNodeOrNull<ActorStatModifier>("ActorStatModifier");
		AttackManagerComponent.AttackComponentReady += CreateAndInjectAttackComponentToModifier;
	}

	// for each child modifier, generate 3 random upgrade resources
	public Array<Resource> GetRandomUpgradeResources()
	{
		Array<Resource> allUpgradeResources = new();

		foreach (Node child in GetChildren())
		{
			if (child is IModifier modifier)
			{
				allUpgradeResources.Add(modifier.GetRandomUpgradeResource());
				allUpgradeResources.Add(modifier.GetRandomUpgradeResource());
				allUpgradeResources.Add(modifier.GetRandomUpgradeResource());
			}
		}

		return allUpgradeResources;
	}

	public void ApplyUpgrade(Resource upgradeResource)
	{
		foreach (Node child in GetChildren())
		{
			if (child is IModifier modifier)
			{
				modifier.ApplyUpgrade(upgradeResource);
			}
		}
	}

    private void CreateAndInjectAttackComponentToModifier(BaseAttackComponent attackComponent)
	{
		if (attackComponent.ModifierComponent?.Instantiate() is BaseAttackModifierComponent modifierComponent)
		{
			modifierComponent.InjectAttackComponent(attackComponent);
			AddChild(modifierComponent);
		}
	}
}
