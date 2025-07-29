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
		DamageNumbers.DisplayDamageNumber((int)amount, GlobalPosition, false);
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

