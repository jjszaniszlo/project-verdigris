using Godot;
using GodotUtilities;
using Verdigris.Autoloads;

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


	public override void _Ready()
	{
		QuitButton.Pressed += async () =>
		{
			SceneManager.Instance.LoadingScreen = GD.Load<PackedScene>("res://Scenes/UI/loading_screen.tscn");
			Globals.Instance.ScreenShake.StopShake();
			GetTree().Paused = false;
			GameEventBus.Reset();
			PlayerEventBus.Reset();
			await SceneManager.Instance.SwapScenes("res://Scenes/UI/MainMenu/main_menu.tscn", null, GetTree().Root.GetNode<Node>("Main"));
			Globals.Instance.GrayscaleEffect.DisableEffect();
		};
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

