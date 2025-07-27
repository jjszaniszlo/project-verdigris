using Godot;
using System;

public partial class PlayerUiControlComponent : Node
{
	[Export]
	public Control InGameUI { get; private set; }

	[Export]
	public PauseMenu PauseMenu { get; private set; }

	private bool _IsPaused;

	public override void _Ready()
	{
		PauseMenu.PauseMenuOptions.ResumeButton.Pressed += TogglePause;
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

		GetTree().Paused = _IsPaused;

		if (_IsPaused)
		{
			Globals.Instance.GrayscaleEffect.EnableEffect();
		}
		else
		{
			Globals.Instance.GrayscaleEffect.DisableEffect();
		}

		InGameUI.Visible = !_IsPaused;
		PauseMenu.Visible = _IsPaused;
	}
}
