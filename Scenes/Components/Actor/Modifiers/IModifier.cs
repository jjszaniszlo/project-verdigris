
using Godot;

public interface IModifier
{
    Resource GetRandomUpgradeResource();
    void ApplyUpgrade(Resource upgradeResource);
}