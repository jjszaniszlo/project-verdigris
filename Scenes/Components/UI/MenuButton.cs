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
		
		PivotOffset = Size / 2;
	}

	public void ButtonEnter()
	{
		CreateTween()
			.SetProcessMode(Tween.TweenProcessMode.Physics)
			.TweenProperty(this, "scale", new Vector2(1.2f, 1.2f), 0.2f);
	}

	public void ButtonExit()
	{
		CreateTween()
			.SetProcessMode(Tween.TweenProcessMode.Physics)
			.TweenProperty(this, "scale", new Vector2(1.0f, 1.0f), 0.2f)
			.FromCurrent();
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

