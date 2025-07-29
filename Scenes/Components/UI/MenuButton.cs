using Godot;
using GodotUtilities;

namespace Scenes.UI.Game.PauseMenu;

[Scene]
public partial class MenuButton : TextureButton
{
	public override void _Ready()
	{
		MouseEntered += ButtonEnter;
		MouseExited += ButtonExit;
		FocusEntered += ButtonEnter;
		FocusExited += ButtonExit;
	}

	public void ButtonEnter()
	{
		CreateTween().TweenProperty(this, "instance_shader_parameters/current_time", 1.0f, 0.2f);
	}

	public void ButtonExit()
	{
		if (!new Rect2(Vector2.Zero, Size).HasPoint(GetLocalMousePosition()))
			CreateTween()
				.TweenProperty(this, "instance_shader_parameters/current_time", 0.0f, 0.1f)
				.From(1.0f);
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

