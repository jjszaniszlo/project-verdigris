using Godot;
using GodotUtilities;

namespace Scenes.UI.Game.PauseMenu;

[Scene]
public partial class PauseMenuOptions : Control
{
	[Node]
	public MenuButton ResumeButton { get; private set; }

	[Node]
	public MenuButton OptionsButton { get; private set; }

	[Node]
	public MenuButton QuitButton { get; private set; }


	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

