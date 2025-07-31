using System.Threading.Tasks;
using Godot;
using Godot.Collections;
using GodotUtilities;
using Verdigris.UI;

namespace Verdigris.Autoloads;

public partial class LoadManager : Node
{
    public static LoadManager Instance { get; private set; }
    public bool UseSubThreads { get; set; } = false;

    private string _scenePath;
    private Node _loadInto;
    private LoadingScreen _loadingScreen;

    [Signal]
    public delegate void LoadProgressChangedEventHandler(float progress);
    [Signal]
    public delegate void LoadDoneEventHandler(PackedScene loadedScene);

    public override void _Ready()
    {
        Instance = this;
    }

    public async Task LoadScene(string scenePath, LoadingScreen loadingScreen, Node loadInto)
    {
        _loadInto = loadInto ?? GetTree().Root;
        _scenePath = scenePath;

        if (loadingScreen != null)
        {
            _loadInto.AddChild(loadingScreen);

            _loadingScreen = loadingScreen;

            LoadProgressChanged += _loadingScreen.UpdateProgressBar;
            LoadDone += _loadingScreen.LeaveTransition;

            await ToSignal(loadingScreen, LoadingScreen.SignalName.LoadingScreenFullyShown);
        }

        StartLoad();
    }

    private void StartLoad()
    {
        var resourceThreadRequest = ResourceLoader.LoadThreadedRequest(_scenePath, "", UseSubThreads, ResourceLoader.CacheMode.IgnoreDeep);
        if (resourceThreadRequest == Error.Ok)
        {
            SetProcess(true);
        }
    }

    public override void _Process(double delta)
    {
        var progress = new Array();
        var loadStatus = ResourceLoader.LoadThreadedGetStatus(_scenePath, progress);
        switch (loadStatus)
        {
            case ResourceLoader.ThreadLoadStatus.InvalidResource:
                SetProcess(false);
                return;
            case ResourceLoader.ThreadLoadStatus.Failed:
                SetProcess(false);
                return;
            case ResourceLoader.ThreadLoadStatus.InProgress:
                EmitSignal(SignalName.LoadProgressChanged, progress[0]);
                break;
            case ResourceLoader.ThreadLoadStatus.Loaded:
                var loadedResource = (PackedScene)ResourceLoader.LoadThreadedGet(_scenePath);
                EmitSignal(SignalName.LoadProgressChanged, progress[0]);
                EmitSignal(SignalName.LoadDone, loadedResource);
                LoadProgressChanged -= _loadingScreen.UpdateProgressBar;
                LoadDone -= _loadingScreen.LeaveTransition;
                break;

        }
    }
}
