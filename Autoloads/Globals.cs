using System;
using Godot;
using GodotUtilities;
using Scenes.Actors;
using Scenes.Components;

public partial class Globals : Node
{
	public static Globals Instance { get; private set; }

	[Export]
	public CameraFollowComponent MainCamera { get; set; }

	[Export]
	public ScreenShake ScreenShake { get; set; }
	[Export]
	public GrayscaleEffect GrayscaleEffect { get; set; }

	[Export]
	public EffectsManager EffectsManager { get; set; }

	private Player _player;
	[Export]
	public Player Player
	{
		get => _player;
		set
		{
			if (value != null && _player != value)
			{
				_player = value;
				EmitSignal(SignalName.PlayerChanged, _player);
			}
		}
	}

	[Signal]
	public delegate void PlayerChangedEventHandler(CharacterBody2D newPlayer);

	public override void _Ready()
	{
		Instance = this;
	}
}

