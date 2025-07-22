using Godot;

public partial class CameraFollowComponent : Camera2D
{
	[Export]
	public CharacterBody2D Player { get; private set; }

	[Export]
	public float FollowRate { get; private set; } = 3f;

	public override void _Ready()
	{
		Position = Player.GlobalPosition;

		Globals.Instance.MainCamera = this;
	}

	public override void _PhysicsProcess(double delta)
	{
		Position = Position.Lerp(Player.GlobalPosition, (float)delta * FollowRate);
	}
}

