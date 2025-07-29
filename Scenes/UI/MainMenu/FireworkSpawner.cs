using System;
using Godot;
using GodotUtilities;

[Scene]
public partial class FireworkSpawner : Node2D
{
	[Export]
	public PackedScene FireworkScene;

	[Export]
	public float FireworkInterval = 1.5f;

	[Export]
	public Node2D SpawnPointCenter;

	[Export]
	public float SpawnRadius = 100f;


	private Timer _FireworkTimer = new();

	public override void _Ready()
	{
		_FireworkTimer.WaitTime = FireworkInterval;
		_FireworkTimer.OneShot = false;
		_FireworkTimer.Autostart = true;
		_FireworkTimer.Timeout += SpawnFirework;
		AddChild(_FireworkTimer);
	}

	private void SpawnFirework()
	{
		float randomAngle = Random.Shared.NextSingle() * (float)Math.PI * 2.0f;
		float randomDistance = Random.Shared.NextSingle() * SpawnRadius;
		Vector2 randomPosition = SpawnPointCenter.GlobalPosition + new Vector2(
			randomDistance * Mathf.Cos(randomAngle),
			randomDistance * Mathf.Sin(randomAngle)
		);

		Globals.Instance.ScreenShake.PlayShake();

		var firework = FireworkScene.Instantiate<Node2D>();
		firework.GlobalPosition = randomPosition;
		AddChild(firework);
	}

    public override void _Process(double delta)
	{
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

