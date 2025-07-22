using Godot;
using GodotUtilities;
using System;
using System.Threading.Tasks;
using Verdigris.Autoloads;

namespace Verdigris.Scenes;

[Scene]
public partial class Scene : Node2D
{
    [Node]
    public Button ChangeScene { get; private set; }

    [Export]
    public string SceneChange { get; private set; }

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _Ready()
    {
        ChangeScene.Pressed += OnChangeScenePressed;
    }

    private async void OnChangeScenePressed()
    {
        SceneManager.Instance.LoadingScreen = GD.Load<PackedScene>("res://Scenes/UI/loading_screen.tscn");
        await SceneManager.Instance.SwapScenes(SceneChange, null, this);
    }
}
