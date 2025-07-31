using Godot;

namespace Verdigris;

[GlobalClass]
public partial class AnimatedSpriteAnimation : Resource
{
    [Export]
    public string AnimationName { get; set; }
    [Export]
    public bool FlipH { get; set; } = false;
    [Export]
    public bool FlipV { get; set; } = false;

    public void Play(AnimatedSprite2D sprite)
    {
        sprite.FlipH = FlipH;
        sprite.FlipV = FlipV;
        sprite.Play(AnimationName);
    }
}