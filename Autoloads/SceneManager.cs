using System.Threading;
using System.Threading.Tasks;
using Godot;
using Verdigris.UI;

namespace Verdigris.Autoloads;

public partial class SceneManager : Node
{
    public static SceneManager Instance { get; private set; }
    public PackedScene LoadingScreen { get; set; }

    private bool _isLoading = false;
    private Node _unloadFrom;
    private Node _loadInto;

    public override void _Ready()
    {
        Instance = this;
    }

    /// <summary>
    /// Swaps a scene with another.
    /// If unloadNode is nil, it replaces the current scene in the root of the scene tree, otherwise it unloads the specified node.
    /// </summary>
    public async Task SwapScenes(string scenePath, Node loadParent, Node unloadNode)
    {
        if (_isLoading)
        {
            GD.PrintErr("SceneManager: Already loading a scene.");
            return;
        }

        _isLoading = true;
        _unloadFrom = unloadNode;
        _loadInto = loadParent ?? GetTree().Root;

        var loadingScreen = LoadingScreen != null ? LoadingScreen.Instantiate<LoadingScreen>() : null;
        LoadManager.Instance.LoadDone += OnLoadDone;
        await LoadManager.Instance.LoadScene(scenePath, loadingScreen, _loadInto);
    }

    private async void OnLoadDone(PackedScene loadedScene)
    {
        _isLoading = false;
        LoadManager.Instance.LoadDone -= OnLoadDone;

        if (_unloadFrom != null)
        {
            _unloadFrom.QueueFree();
            await ToSignal(_unloadFrom, Node.SignalName.TreeExited);
        }
        _loadInto.AddChild(loadedScene.Instantiate());
    }
}
