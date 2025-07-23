using Godot;

namespace Scenes.Actors;

public partial class Player : CharacterBody2D
{
	public override void _Ready()
	{
		Globals.Instance.Player = this;
	}
}
