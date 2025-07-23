using System;
using Godot;
using GodotUtilities;

public partial class Globals : Node
{
	public static Globals Instance { get; private set; }

	public Camera2D MainCamera { get; set; }

	private CharacterBody2D _player;
	public CharacterBody2D Player
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

