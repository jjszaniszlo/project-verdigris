using Godot;
using GodotUtilities;
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace Scenes.Components.Actor;

public partial class PlayerController : Node
{
	[Export]
	public VelocityComponent VelocityComponent { get; private set; }	
	public CharacterBody2D Player { get; private set; }

    public override void _Ready()
    {
		Player = GetParentOrNull<CharacterBody2D>();
		if (Player == null)
		{
			GD.PrintErr("PlayerController must be a child of a CharacterBody2D node.");
			return;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		var inputDirection = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");

		if (inputDirection.LengthSquared() == 0)
		{
			VelocityComponent.MinimizeVelocity();
		}
		else
		{
			VelocityComponent.AccelerateToMaxVelocity(inputDirection);
		}
		VelocityComponent.Move(Player);
	}
}

