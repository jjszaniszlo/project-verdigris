using System;
using Godot;
using GodotUtilities;
using Scenes.Actors;
using Scenes.Components;

public partial class Globals : Node
{
	public static Globals Instance { get; private set; }

	public CameraFollowComponent MainCamera { get; set; }

	public ScreenShake ScreenShake { get; set; }
	public GrayscaleEffect GrayscaleEffect { get; set; }

	public EffectsManager EffectsManager { get; set; }

	private Player _player;
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

