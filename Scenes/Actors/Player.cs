using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public override void _Ready()
	{
		Globals.Instance.Player = this;
	}
}
