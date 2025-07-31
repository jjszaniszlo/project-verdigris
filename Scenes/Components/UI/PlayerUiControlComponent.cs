using Godot;
using Godot.Collections;
using Scenes.UI.Game.UpgradeMenu;
using System;

public partial class PlayerUiControlComponent : Node
{
	[Export]
	public Control InGameUI { get; private set; }

	[Export]
	public PauseMenu PauseMenu { get; private set; }

	[Export]
	public UpgradeCardsUI UpgradeCards { get; private set; }

	[Export]
	public Control GameOverUI { get; private set; }

	private bool _IsPaused;
	private bool _IsUpgrading;
	private bool _IsGameOver;

	public override void _Ready()
	{
		PauseMenu.PauseMenuOptions.ResumeButton.Pressed += TogglePause;

		GameEventBus.Instance.OpenUpgradeSelection += OnOpenUpgradeSelection;
		GameEventBus.Instance.UpgradeSelected += (_) =>
		{
			_IsUpgrading = false;
			TreeUpdatePause();
			ToggleEffects();
			UpdateUIVisibility();
		};
		GameEventBus.Instance.GameOver += () =>
		{
			_IsGameOver = true;
			TreeUpdatePause();
			ToggleEffects();
			UpdateUIVisibility();
		};
	}

	private void OnOpenUpgradeSelection(Array<Resource> upgrades)
	{
		_IsUpgrading = true;
		TreeUpdatePause();
		ToggleEffects();
		UpdateUIVisibility();

		UpgradeCards.SetUpgrades(upgrades);
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.IsPressed() && keyEvent.Keycode == Key.Escape)
		{
			TogglePause();
		}
	}

	private void TogglePause()
	{
		_IsPaused = !_IsPaused;

		TreeUpdatePause();
		ToggleEffects();
		UpdateUIVisibility();
	}

	private void ToggleEffects()
	{
		if (_IsPaused || _IsUpgrading || _IsGameOver)
		{
			Globals.Instance.GrayscaleEffect.EnableEffect();
		}
		else
		{
			Globals.Instance.GrayscaleEffect.DisableEffect();
		}
	}

	private void UpdateUIVisibility()
	{
		InGameUI.Visible = !_IsPaused && !_IsUpgrading && !_IsGameOver;
		PauseMenu.Visible = _IsPaused;
		UpgradeCards.Visible = _IsUpgrading;
		GameOverUI.Visible = _IsGameOver;
	}

	private void TreeUpdatePause()
	{
		GetTree().Paused = _IsPaused || _IsUpgrading || _IsGameOver;
	}
}
