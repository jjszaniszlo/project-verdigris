using Godot;
using GodotUtilities;

[Scene]
public partial class ShootFireworkComponent : Node2D
{
	public override void _Ready()
	{
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

