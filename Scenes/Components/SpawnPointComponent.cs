using System.Linq;
using Godot;
using GodotUtilities;

[Scene]
public partial class SpawnPointComponent : Node2D
{
	[Node]
	public Area2D Top {get; private set; }
	[Node]
	public Area2D Bottom {get; private set; }
	[Node]
	public Area2D Left {get; private set; }
	[Node]
	public Area2D Right {get; private set; }

	public bool PointCanSpawn(Area2D point) => point.GetOverlappingAreas().Count == 0 && point.GetOverlappingBodies().Count == 0;

	public Vector2 RandomSpawnPoint()
	{
		var spawnPoints = new Area2D[] { Top, Bottom, Left, Right }
			.Where(PointCanSpawn)
			.Select(area => area.GlobalPosition)
			.ToArray();

		GD.Print($"Spawn points available: {spawnPoints.Length}");

		return spawnPoints[GD.Randi() % spawnPoints.Length];
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

