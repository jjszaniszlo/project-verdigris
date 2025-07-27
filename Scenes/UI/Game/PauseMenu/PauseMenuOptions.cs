using Godot;
using GodotUtilities;

namespace Scenes.UI.Game.PauseMenu;

[Scene]
public partial class PauseMenuOptions : Control
{
	[Node]
	public PauseMenuButton ResumeButton { get; private set; }

	[Node]
	public PauseMenuButton OptionsButton { get; private set; }

	[Node]
	public PauseMenuButton QuitButton { get; private set; }


	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

