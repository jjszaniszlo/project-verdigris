using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class LoadingScreen : CanvasLayer
{
    [Node]
    public ProgressBar LoadingProgress { get; private set; }

    [Node]
    public FadeEffect FadeEffectComponent { get; private set; }

    [Signal]
    public delegate void LoadingScreenFullyShownEventHandler();

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _Ready()
    {
        FadeEffectComponent.FadeIn();
        FadeEffectComponent.FadeInFinished += () =>
        {
            EmitSignal(SignalName.LoadingScreenFullyShown);
        };
        FadeEffectComponent.FadeOutFinished += () =>
        {
            QueueFree();
        };
    }

    public void UpdateProgressBar(float newProgress)
    {
        LoadingProgress.Value = newProgress * 100f;
    }

    public void LeaveTransition()
    {
        FadeEffectComponent.FadeOut();
    }
}
