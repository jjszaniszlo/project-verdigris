using Godot;
using GodotUtilities;
using Verdigris.Autoloads;

[Scene]
public partial class GameOver : Control
{
	[Node]
	public BaseButton RestartButton { get; private set; }

	[Node]
	public BaseButton QuitButton { get; private set; }

	public override void _Ready()
	{
		RestartButton.Pressed += async () =>
		{
			SceneManager.Instance.LoadingScreen = GD.Load<PackedScene>("res://Scenes/UI/loading_screen.tscn");
			Globals.Instance.ScreenShake.StopShake();
			GetTree().Paused = false;
			GameEventBus.Reset();
			PlayerEventBus.Reset();
			await SceneManager.Instance.SwapScenes("res://Scenes/Game/main_game.tscn", null, GetTree().Root.GetNode<Node>("Main"));
			Globals.Instance.GrayscaleEffect.DisableEffect();
		};

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

