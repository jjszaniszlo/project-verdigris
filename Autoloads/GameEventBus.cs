
using Godot;
using Godot.Collections;

public partial class GameEventBus : Node
{
    public static GameEventBus Instance { get; private set; }

    [Signal]
    public delegate void GameUIReadyEventHandler();
    [Signal]
    public delegate void UpgradeSelectedEventHandler(Resource upgrade);
    [Signal]
    public delegate void OpenUpgradeSelectionEventHandler(Array<Resource> upgrades);
    [Signal]
    public delegate void GameOverEventHandler();

    public override void _Ready()
    {
        Instance = this;
    }

    public static void Reset()
    {
        Instance = null;
        Instance = new GameEventBus();
    }

    public void EmitGameUIReady()
    {
        EmitSignal(SignalName.GameUIReady);
    }

    public void EmitUpgradeSelected(Resource upgrade)
    {

        EmitSignal(SignalName.UpgradeSelected, upgrade);
    }

    public void EmitOpenUpgradeSelection(Resource[] upgrades)
    {
        EmitSignal(SignalName.OpenUpgradeSelection, upgrades);
    }

    public void EmitGameOver()
    {
        EmitSignal(SignalName.GameOver);
    }
}