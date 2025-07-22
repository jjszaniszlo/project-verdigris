using Godot;
using GodotUtilities;

public partial class Globals : Node
{
	public static Globals Instance { get; private set; }

	public Camera2D MainCamera { get; set; }

	public override void _Ready()
	{
		Instance = this;
	}
}

