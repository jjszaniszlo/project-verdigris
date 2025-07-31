using Godot;
using GodotUtilities;

[Scene]
public partial class SpawnPointComponent : Node2D
{
	[Node]
	public Node2D Top {get; private set; }
	[Node]
	public Node2D Bottom {get; private set; }
	[Node]
	public Node2D Left {get; private set; }
	[Node]
	public Node2D Right {get; private set; }

	public Vector2 TopPosition => Top.GlobalPosition;
	public Vector2 BottomPosition => Bottom.GlobalPosition;
	public Vector2 LeftPosition => Left.GlobalPosition;
	public Vector2 RightPosition => Right.GlobalPosition;

	public bool TopInWater => Top.GlobalPosition.Y < -1;
	public bool BottomInWater => Bottom.GlobalPosition.Y < -1;
	public bool LeftInWater => Left.GlobalPosition.Y < -1;
	public bool RightInWater => Right.GlobalPosition.Y < -1;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

