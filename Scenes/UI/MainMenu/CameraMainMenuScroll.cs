using Godot;
using System;

public partial class CameraMainMenuScroll : Camera2D
{
	[Export]
	public float ScrollSpeed = 10f;

	[Export]
	public float HeightAmplitude = 10f;

	[Export]
	public float HeightOffset = 10f;

	public override void _Ready()
	{
		MakeCurrent();
	}

	public override void _PhysicsProcess(double delta)
	{
		var sinWaveOffset = Mathf.Sin(Time.GetTicksMsec() / 1000f * ScrollSpeed * delta) * HeightAmplitude;
		Position = new Vector2(Position.X - ScrollSpeed * (float)delta, (float)sinWaveOffset + HeightOffset);
	}
}
