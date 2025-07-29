using Godot;
using GodotUtilities;
using Scenes.Components;

[Scene]
public partial class World : Node2D
{
	[Export]
	public Node2D ShowCenterTarget { get; private set; }

	[Node]
	public GroundGeneratorComponent GroundGeneratorComponent { get; private set; }

    public override void _Ready()
    {
		GroundGeneratorComponent.ShowCenterTarget = ShowCenterTarget;
    }

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

