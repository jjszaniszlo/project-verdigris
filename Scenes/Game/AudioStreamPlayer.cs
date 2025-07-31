using Godot;
using GodotUtilities;

[Scene]
public partial class AudioStreamPlayer : Godot.AudioStreamPlayer
{
	public override void _Ready()
	{
		GameEventBus.Instance.GameOver += () =>
		{
			if (Playing)
			{
				Stop();
			}
		};
	}
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

