using System.Linq;
using Godot;
using Scenes.Actors;

public partial class PlayerUpgradeManager : Node
{
	[Export]
	public Player Player { get; private set; }

	public override void _Ready()
	{
		PlayerEventBus.Instance.PlayerLevelUp += OnPlayerLevelUp;

		GameEventBus.Instance.UpgradeSelected += (upgrade) =>
		{
			Player.ModifierManagerComponent.ApplyUpgrade(upgrade);
		};
	}

	private void OnPlayerLevelUp()
	{
		var upgrades = Player.ModifierManagerComponent.GetRandomUpgradeResources();
		upgrades.Shuffle();
		var selectedUpgrades = upgrades.Take(3).ToArray();

		GameEventBus.Instance.EmitOpenUpgradeSelection(selectedUpgrades);
    }
}