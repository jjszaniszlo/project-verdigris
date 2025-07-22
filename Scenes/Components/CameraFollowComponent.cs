using Godot;

public partial class CameraFollowComponent : Camera2D
{
	[Export]
	public CharacterBody2D Player { get; private set; }

	private Vector2 _cameraPosition = Vector2.Zero;

	[Export]
	public float FollowRate { get; private set; } = 3f;

	public override void _Ready()
	{
		_cameraPosition = Player.GlobalPosition;

		Globals.Instance.MainCamera = this;
	}

	public override void _PhysicsProcess(double delta)
	{
		_cameraPosition = _cameraPosition.Lerp(Player.GlobalPosition, (float)delta * FollowRate);

		var cameraSubpixelOffset = _cameraPosition.Round() - _cameraPosition;

		if (Globals.Instance != null && Globals.Instance.SubViewportShaderMaterial != null)
		{
			Globals.Instance.SubViewportShaderMaterial.SetShaderParameter("camera_offset", cameraSubpixelOffset);
		}

		Position = _cameraPosition.Round();
	}
}

