using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using GodotUtilities;
using Scenes.Components.Actor;

[Scene]
public partial class DamageNumbersComponent : Node2D
{
	[Export]
	public HurtboxComponent HurtboxComponent { get; private set; }

	public override void _Ready()
	{
		HurtboxComponent.HurtboxDamaged += OnHurtboxDamaged;
	}

    private void OnHurtboxDamaged(float amount)
	{
		var randomOffset = new Vector2(
			GD.RandRange(-5, 5),
			GD.RandRange(-10, 10)
		);
		DamageNumbers.DisplayDamageNumber((int)amount, GlobalPosition + randomOffset, false);
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

