using Godot;
using System;

public partial class FadeEffect : AnimationPlayer
{
    [Export]
    public CanvasItem NodeToFade { get; set; }

    [Export]
    public float FadeInDuration { get; set; } = 0.45f;
    [Export]
    public float FadeOutDuration { get; set; } = 0.45f;

    private const string FadeEffectAnimationLibraryName = "fade_effects";
    private const string FadeInAnimationName = "fade_in";
    private const string FadeOutAnimationName = "fade_out";

    [Signal]
    public delegate void FadeInFinishedEventHandler();

    [Signal]
    public delegate void FadeOutFinishedEventHandler();

    public override void _Ready()
    {
        if (NodeToFade == null)
        {
            GD.PrintErr("FadeEffect: NodeToFade is not set. Please assign a CanvasItem to fade.");
            return;
        }

        CreateAnimations();

        AnimationFinished += (animName) =>
        {
            if (animName == $"{FadeEffectAnimationLibraryName}/{FadeInAnimationName}")
            {
                EmitSignal(SignalName.FadeInFinished);
            }
            else if (animName == $"{FadeEffectAnimationLibraryName}/{FadeOutAnimationName}")
            {
                EmitSignal(SignalName.FadeOutFinished);
            }
        };
    }

    private void CreateAnimations()
    {
        var fadeInAnimation = new Animation();
        fadeInAnimation.Length = FadeInDuration;
        fadeInAnimation.AddTrack(Animation.TrackType.Value, 0);
        fadeInAnimation.TrackSetPath(0, NodeToFade.GetPath() + ":modulate");
        fadeInAnimation.TrackInsertKey(0, 0.0f, new Color(NodeToFade.Modulate.R, NodeToFade.Modulate.G, NodeToFade.Modulate.B, 0.0f));
        fadeInAnimation.TrackInsertKey(0, FadeInDuration, new Color(NodeToFade.Modulate.R, NodeToFade.Modulate.G, NodeToFade.Modulate.B, 1.0f));

        var fadeOutAnimation = new Animation();
        fadeOutAnimation.Length = FadeInDuration;
        fadeOutAnimation.AddTrack(Animation.TrackType.Value, 0);
        fadeOutAnimation.TrackSetPath(0, NodeToFade.GetPath() + ":modulate");
        fadeOutAnimation.TrackInsertKey(0, 0.0f, new Color(NodeToFade.Modulate.R, NodeToFade.Modulate.G, NodeToFade.Modulate.B, 1.0f));
        fadeOutAnimation.TrackInsertKey(0, FadeInDuration, new Color(NodeToFade.Modulate.R, NodeToFade.Modulate.G, NodeToFade.Modulate.B, 0.0f));

        var animationLibrary = new AnimationLibrary();
        animationLibrary.AddAnimation(FadeInAnimationName, fadeInAnimation);
        animationLibrary.AddAnimation(FadeOutAnimationName, fadeOutAnimation);

        AddAnimationLibrary(FadeEffectAnimationLibraryName, animationLibrary);
    }

    public void FadeIn()
    {
        Play($"{FadeEffectAnimationLibraryName}/{FadeInAnimationName}");
    }

    public void FadeOut()
    {
        Play($"{FadeEffectAnimationLibraryName}/{FadeOutAnimationName}");
    }
}
