using System;
using Godot;
using GodotUtilities;
using Scenes.Components.Actor;
using static Scenes.Components.Actor.PlayerControllerComponent;

namespace Scenes.Actors;

[Scene]
public partial class Player : CharacterBody2D
{
	[Node]
	public AnimatedActorComponent AnimatedActorComponent { get; private set; }
	[Node]
	public PlayerControllerComponent PlayerControllerComponent { get; private set; }

	public override void _Ready()
	{
		Globals.Instance.Player = this;

		PlayerControllerComponent.PlayerMovementChanged += OnPlayerFacingChanged;
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

    public override void _Notification(int what)
    {
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }
}
