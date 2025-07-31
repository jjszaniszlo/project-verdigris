using Godot;

[GlobalClass]
public abstract partial class BaseUpgrade : Resource
{
    [Export]
    public string UpgradeName { get; set; }

    [Export]
    public string Description { get; set; }

    [Export]
    public Texture2D CardBackground { get; set; }

    public void Apply(IModifier modifier)
    {
        modifier.ApplyUpgrade(this);
    }

    public abstract void ApplyToUpgradeCardUIComponent(Card card);
}