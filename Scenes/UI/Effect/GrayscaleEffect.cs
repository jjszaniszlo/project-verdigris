using Godot;
using GodotUtilities;

[Scene]
public partial class GrayscaleEffect : CanvasLayer
{
	[Node]
	public ColorRect ColorRect { get; private set; }

	public override void _Ready()
	{
		Globals.Instance.GrayscaleEffect = this;
	}

	public void EnableEffect()
	{
		if (ColorRect == null)
		{
			GD.PrintErr("GrayscaleEffect: ColorRect is not set. Cannot enable effect.");
			return;
		}
		CreateTween()
			.SetProcessMode(Tween.TweenProcessMode.Physics)
			.SetPauseMode(Tween.TweenPauseMode.Process)
			.TweenProperty(ColorRect, "material:shader_parameter/time", 1.0f, 0.5f);
	}

	public void DisableEffect()
	{
		if (ColorRect == null)
		{
			GD.PrintErr("GrayscaleEffect: ColorRect is not set. Cannot enable effect.");
			return;
		}
		CreateTween()
			.SetProcessMode(Tween.TweenProcessMode.Physics)
			.SetPauseMode(Tween.TweenPauseMode.Process)
			.TweenProperty(ColorRect, "material:shader_parameter/time", 0.0f, 0.5f);
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

}

