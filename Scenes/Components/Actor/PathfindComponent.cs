using Godot;
using GodotUtilities;

namespace Scenes.Components.Actor;

[Scene]
public partial class PathfindComponent : Node2D
{
	[Export]
	public VelocityComponent VelocityComponent { get; private set; }

	[Node]
	public NavigationAgent2D NavigationAgent { get; private set; }

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

