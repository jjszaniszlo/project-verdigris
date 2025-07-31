using Godot;

[GlobalClass]
public partial class EnemySpawn : Resource
{
    [Export]
    public PackedScene Scene { get; set; }
    [Export]
    public float Weight { get; set; } = 1.0f;
}