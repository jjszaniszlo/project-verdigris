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
		CreateTween().TweenProperty(Material, "shader_parameter/current_time", 1.0f, 0.2f);
	}

	public void ButtonExit()
	{
		CreateTween().TweenProperty(Material, "shader_parameter/current_time", 0.0f, 0.01f);
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

