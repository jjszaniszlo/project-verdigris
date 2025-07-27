using Godot;

namespace Scenes.Components.Actor;

public partial class PlayerControllerComponent : Node2D
{
	[Export]
	public VelocityComponent VelocityComponent { get; private set; }
	[Export]
	public AnimatedActorComponent AnimatedActorComponent { get; private set; }
	public CharacterBody2D Player { get; private set; }

	public Vector2 AimingAt => GetGlobalMousePosition();
	public Vector2 Facing => VelocityComponent.Velocity.Normalized();

	private FacingDirection _CurrentFacingDirection;
	public FacingDirection CurrentFacingDirection
	{
		get => _CurrentFacingDirection;
		private set
		{
			if (_CurrentFacingDirection != value)
			{
				_CurrentFacingDirection = value;
				OnPlayerFacingChanged(_CurrentFacingDirection, _IsMoving);
			}
		}
	}

	private bool _IsMoving;
	public bool IsMoving
	{
		get => _IsMoving;
		private set
		{
			if (_IsMoving != value)
			{
				_IsMoving = value;
				OnPlayerFacingChanged(_CurrentFacingDirection, _IsMoving);
			}
		}
	}

	private bool FacingUp => Facing.Dot(Vector2.Up) > Facing.Dot(Vector2.Right) && Facing.Dot(Vector2.Up) > Facing.Dot(Vector2.Left);
	private bool FacingDown => Facing.Dot(Vector2.Down) > Facing.Dot(Vector2.Right) && Facing.Dot(Vector2.Down) > Facing.Dot(Vector2.Left);
	private bool FacingLeft => Facing.Dot(Vector2.Left) > Facing.Dot(Vector2.Up) && Facing.Dot(Vector2.Left) > Facing.Dot(Vector2.Down);
	private bool FacingRight => Facing.Dot(Vector2.Right) > Facing.Dot(Vector2.Up) && Facing.Dot(Vector2.Right) > Facing.Dot(Vector2.Down);

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
		HandleMovement();
		UpdateFacingDirection();
	}

	public void HandleMovement()
	{
		var inputDirection = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");

		if (inputDirection.LengthSquared() == 0)
		{
			VelocityComponent.Decelerate();
			IsMoving = false;
		}
		else
		{
			IsMoving = true;
			VelocityComponent.Accelerate(inputDirection);
		}
		VelocityComponent.Move(Player);
	}

	public void UpdateFacingDirection()
	{
		if (FacingUp)
		{
			CurrentFacingDirection = FacingDirection.Up;
		}
		else if (FacingDown)
		{
			CurrentFacingDirection = FacingDirection.Down;
		}
		else if (FacingLeft)
		{
			CurrentFacingDirection = FacingDirection.Left;
		}
		else if (FacingRight)
		{
			CurrentFacingDirection = FacingDirection.Right;
		}
	}
	private void OnPlayerFacingChanged(FacingDirection direction, bool isMoving)
	{
		if (isMoving)
		{
			switch (direction)
			{
				case FacingDirection.Up:
					AnimatedActorComponent.PlayMoveAway();
					break;
				case FacingDirection.Down:
					AnimatedActorComponent.PlayMoveTowards();
					break;
				case FacingDirection.Left:
					AnimatedActorComponent.PlayMoveLeft();
					break;
				case FacingDirection.Right:
					AnimatedActorComponent.PlayMoveRight();
					break;
			}
			return;
		}

		switch (direction)
		{
			case FacingDirection.Up:
				AnimatedActorComponent.PlayIdleAway();
				break;
			case FacingDirection.Down:
				AnimatedActorComponent.PlayIdleTowards();
				break;
			case FacingDirection.Left:
				AnimatedActorComponent.PlayIdleLeft();
				break;
			case FacingDirection.Right:
				AnimatedActorComponent.PlayIdleRight();
				break;
		}
	}

	public enum FacingDirection
	{
		Up,
		Down,
		Left,
		Right
	}
}

