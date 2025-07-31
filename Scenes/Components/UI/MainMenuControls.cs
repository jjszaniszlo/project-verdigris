using System;
using System.Threading.Tasks;
using Godot;
using GodotUtilities;
using Verdigris.Autoloads;

[Scene]
public partial class MainMenuControls : Control
{
	[Node]
	public TextureButton PlayButton { get; private set; }

	public override void _Ready()
	{
		PlayButton.Pressed += OnPlayButtonPressed;
	}

    private async void OnPlayButtonPressed()
    {
		SceneManager.Instance.LoadingScreen = GD.Load<PackedScene>("res://Scenes/UI/loading_screen.tscn");
		Globals.Instance.ScreenShake.StopShake();
		await SceneManager.Instance.SwapScenes("res://Scenes/Game/main_game.tscn", null, GetTree().Root.GetNode<Node2D>("MainMenu"));
	}

    public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

