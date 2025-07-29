using Godot;
using GodotUtilities;

public partial class EffectsManager : Node2D
{
	public override void _Ready()
	{
		Globals.Instance.EffectsManager = this;
	}

	public void AddEffect(Node effect)
	{
		AddChild(effect);
	}
}

