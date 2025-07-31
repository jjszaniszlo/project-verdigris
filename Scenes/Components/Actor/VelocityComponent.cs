using Godot;

namespace Scenes.Components.Actor;

public partial class VelocityComponent : Node
{
	[Export]
	public float MaxSpeed { get; private set; } = 100f;

	[Export]
	public float Acceleration { get; private set; } = 10f;

	[Export]
	public float Deceleration { get; private set; } = 10f;

	public Vector2 Velocity { get; private set; }
	public float Speed => Velocity.Length();

	public float SpeedMultiplier { get; set; } = 1f;
	public float AccelerationMultiplier { get; set; } = 1f;

	public float CurrentDeceleration => Deceleration * AccelerationMultiplier;
	public float CurrentAcceleration => Acceleration * AccelerationMultiplier;
	public float CurrentMaxSpeed => MaxSpeed * SpeedMultiplier;

	public void Accelerate(Vector2 direction)
	{
		AccelerateToVelocity(GetMaxVelocity(direction));
	}

	public void Decelerate()
	{
		Velocity = Velocity.Lerp(Vector2.Zero, 1f - Mathf.Exp(-CurrentDeceleration * (float)GetProcessDeltaTime()));
	}

	public void MinimizeVelocity()
	{
		Velocity = Vector2.Zero;
	}

	public void MaximizeVelocity(Vector2 direction)
	{
		Velocity = GetMaxVelocity(direction);
	}

	public void Move(CharacterBody2D characterBody2D)
	{
		characterBody2D.Velocity = Velocity;
		characterBody2D.MoveAndSlide();
	}

	public Vector2 GetMaxVelocity(Vector2 direction)
	{
		return direction.Normalized() * CurrentMaxSpeed;
	}

	public void SetVelocityForced(Vector2 velocity)
	{
		Velocity = velocity;
	}

	private void AccelerateToVelocity(Vector2 velocity)
	{
		Velocity = Velocity.Lerp(velocity, 1f - Mathf.Exp(-CurrentAcceleration * (float)GetProcessDeltaTime()));
	}
}

