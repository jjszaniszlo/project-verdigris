using Godot;
using GodotUtilities;
using Verdigris;

[Scene]
public partial class AnimatedActorComponent : AnimatedSprite2D
{
	[ExportCategory("Idle")]
	[Export]
	public AnimatedSpriteAnimation IdleTowards { get; set; }
	[Export]
	public AnimatedSpriteAnimation IdleAway { get; set; }
	[Export]
	public AnimatedSpriteAnimation IdleLeft { get; set; }
	[Export]
	public AnimatedSpriteAnimation IdleRight { get; set; }

	[ExportCategory("Movement")]
	[Export]
	public AnimatedSpriteAnimation MoveTowards { get; set; }
	[Export]
	public AnimatedSpriteAnimation MoveAway { get; set; }
	[Export]
	public AnimatedSpriteAnimation MoveLeft { get; set; }
	[Export]
	public AnimatedSpriteAnimation MoveRight { get; set; }

	public override void _Ready()
	{
	}

	public void PlayIdleTowards() => IdleTowards.Play(this);
	public void PlayIdleAway() => IdleAway.Play(this);
	public void PlayIdleRight() => IdleRight.Play(this);
	public void PlayIdleLeft() => IdleLeft.Play(this);
	public void PlayMoveTowards() => MoveTowards.Play(this);
	public void PlayMoveAway() => MoveAway.Play(this);
	public void PlayMoveRight() => MoveRight.Play(this);
	public void PlayMoveLeft() => MoveLeft.Play(this);

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

