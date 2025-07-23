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
	public float SpeedMultiplier { get; set; } = 1f;
	public float AccelerationMultiplier { get; set; } = 1f;

	public float CurrentAcceleration => Acceleration * AccelerationMultiplier;
	public float CurrentMaxSpeed => MaxSpeed * SpeedMultiplier;

	public void AccelerateToVelocity(Vector2 velocity)
	{
		Velocity = Velocity.Lerp(velocity, 1f - Mathf.Exp(-Acceleration * AccelerationMultiplier * (float)GetProcessDeltaTime()));
	}

	public void AccelerateToMaxVelocity(Vector2 direction)
	{
		Velocity = Velocity.Lerp(GetMaxVelocity(direction), 1f - Mathf.Exp(-Acceleration * AccelerationMultiplier * (float)GetProcessDeltaTime()));
	}

	public Vector2 GetMaxVelocity(Vector2 direction)
	{
		return direction.Normalized() * CurrentMaxSpeed;
	}

	public void MinimizeVelocity()
	{
		Velocity = Velocity.Lerp(Vector2.Zero, 1f - Mathf.Exp(-Deceleration * AccelerationMultiplier * (float)GetProcessDeltaTime()));
	}

	public void MaximizeVelocity(Vector2 direction)
	{
		Velocity = GetMaxVelocity(direction);
	}

	public void Decelerate()
	{
		AccelerateToVelocity(Vector2.Zero);
	}

	public void Move(CharacterBody2D characterBody2D)
	{
		characterBody2D.Velocity = Velocity;
		characterBody2D.MoveAndSlide();
	}
}

