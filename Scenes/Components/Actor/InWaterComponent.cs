using System;
using Godot;
using GodotUtilities;

public partial class InWaterComponent : Sprite2D
{
	[Export]
	public Sprite2D DropShadowComponent { get; set; }

	public override void _PhysicsProcess(double delta)
	{
		// TODO: Poll from world.  and make this more robust. 
		var height = GlobalPosition.Y / 8.0f;
		if (height < 2.0f)
		{
			Frame = Math.Clamp((int)Math.Abs(height) + 2, 0, Hframes - 1);
		}
		else
		{
			Frame = 0;
		}

		if (DropShadowComponent != null)
		{
			DropShadowComponent.Modulate = new Color(1, 1, 1, Math.Clamp(height - 2.0f, 0.0f, 1.0f));
		}
	}
}

