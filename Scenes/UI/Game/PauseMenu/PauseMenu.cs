using Godot;
using GodotUtilities;
using Scenes.UI.Game.PauseMenu;

[Scene]
public partial class PauseMenu : Control
{
	[Node]
	public PauseMenuOptions PauseMenuOptions { get; private set; }

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

