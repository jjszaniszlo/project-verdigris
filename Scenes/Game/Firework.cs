using Godot;
using GodotUtilities;

public partial class Firework : AnimatedSprite2D
{
	public override void _Ready()
	{
		AnimationFinished += QueueFree;
	}
}

