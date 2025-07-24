using Godot;
using GodotUtilities;

namespace Scenes.UI.Game.PauseMenu;

[Scene]
public partial class PauseMenuOptions : Control
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

