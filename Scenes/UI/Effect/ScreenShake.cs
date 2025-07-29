using System.ComponentModel.DataAnnotations;
using Godot;
using GodotUtilities;

[Scene]
public partial class ScreenShake : CanvasLayer
{
	[Node]
	public ColorRect ColorRect { get; private set; }

	[Export]
	public float Strength { get; set; } = 0.3f; 

    public override void _Ready()
    {
		Globals.Instance.ScreenShake = this;
		StopShake();
    }

	public void PlayShake()
	{
		CreateTween()
			.SetProcessMode(Tween.TweenProcessMode.Physics)
			.SetPauseMode(Tween.TweenPauseMode.Process)
			.TweenProperty(ColorRect, "material:shader_parameter/ShakeStrength", 0.0f, 0.3f)
			.From(Strength);
	}

	public void StopShake()
	{
		(ColorRect.Material as ShaderMaterial).SetShaderParameter("ShakeStrength", 0.0f);
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

