using Godot;
using GodotUtilities;

namespace Scenes.Components;

[Scene]
public partial class CameraFollowComponent : Camera2D
{
	[Export]
	public CharacterBody2D Player { get; private set; }

	[Export]
	public float FollowRate { get; private set; } = 5f;

	[Node]
	public SpawnPointComponent SpawnPointComponent { get; private set; }

	public override void _Ready()
	{
		Position = Player.GlobalPosition;

		Globals.Instance.MainCamera = this;
		MakeCurrent();
	}

	public override void _PhysicsProcess(double delta)
	{
		Position = Position.Lerp(Player?.GlobalPosition ?? Position, (float)delta * FollowRate);
	}

    public override void _Notification(int what)
    {
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }
}

