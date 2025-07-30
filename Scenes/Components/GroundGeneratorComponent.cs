using System.ComponentModel.DataAnnotations;
using Godot;
using Godot.Collections;
using GodotUtilities;

namespace Scenes.Components;

[Scene]
public partial class GroundGeneratorComponent : Node
{
	[ExportCategory("Ground Layer")]
	[Export]
	public TileMapLayer Ground { get; private set; }
	[Export]
	public Vector2I SandTileCoords { get; set; } = new Vector2I(0, 0);
	[Export]
	private Vector2I GrassTileCoords { get; set; } = new Vector2I(2, 0);
	[Export]
	private Vector2I InvisibleCollisionTileCoords { get; set; } = new Vector2I(3, 0);

	[ExportCategory("Water Layer")]
	[Export]
	public TileMapLayer Water { get; private set; }
	[Export]
	public Vector2I WaterTileCoords { get; set; } = new Vector2I(0, 0);
	[Export]
	public Vector2I WaterEdgeCoords { get; set; } = new Vector2I(1, 0);

	[ExportCategory("Atlas Settings")]
	[Export]
	public int GroundAtlasSourceID { get; private set; }
	[Export]
	public int WaterAtlasSourceID { get; private set; }

	[ExportCategory("Generator Settings")]
	[Export]
	public float WaterCollisionHeight { get; set; } = -0.5f;
	[Export]
	public int TileSize { get; set; } = 32;
	[Export]
	public int ChunkSize { get; set; } = 16;
	[Export]
	public int HeightGradientScale { get; set; } = 8;

	private int ChunkViewDistance => (ChunkSize / 2) - 1;

	public Node2D ShowCenterTarget { get; set; }

	public override void _Ready()
	{
		LoadChunk(0, 0);
	}

	public override void _PhysicsProcess(double delta)
	{
		var worldSize = Ground.GetUsedRect();
		var worldSizeStart = worldSize.Position;
		var worldSizeEnd = worldSize.End;
		var playerViewPositionPositive = (ShowCenterTarget.GlobalPosition / TileSize) + new Vector2(ChunkViewDistance, ChunkViewDistance);
		var playerViewPositionNegative = (ShowCenterTarget.GlobalPosition / TileSize) - new Vector2(ChunkViewDistance, ChunkViewDistance);

		if (playerViewPositionPositive.X > worldSizeEnd.X)
		{
			LoadChunk(worldSizeStart.X + 1, worldSizeStart.Y);
		}
		else if (playerViewPositionNegative.X < worldSizeStart.X)
		{
			LoadChunk(worldSizeStart.X - 1, worldSizeStart.Y);
		}
		else if (playerViewPositionPositive.Y > worldSizeEnd.Y)
		{
			LoadChunk(worldSizeStart.X, worldSizeStart.Y + 1);
		}
		else if (playerViewPositionNegative.Y < worldSizeStart.Y)
		{
			LoadChunk(worldSizeStart.X, worldSizeStart.Y - 1);
		}
	}

	// TODO: Variance depending on X?
	private float GetHeightAt(float x, float y)
	{
		return y / HeightGradientScale;
	}

	private void LoadChunk(float x, float y)
	{
		Ground.Clear();

		var rockyCells = new Array<Vector2I>();
		var grassyCells = new Array<Vector2I>();
		var fencyCells = new Array<Vector2I>();
		var roadyCells = new Array<Vector2I>();

		for (int i = 0; i < ChunkSize; i++)
		{
			for (int j = 0; j < ChunkSize; j++)
			{
				var height = GetHeightAt(x + i, y + j);
				var cellPosition = new Vector2I((int)(x + i), (int)(y + j));

				if (height < WaterCollisionHeight)
				{
					Ground.SetCell(cellPosition, GroundAtlasSourceID, InvisibleCollisionTileCoords);
				}
				else if (height > 1)
				{
					Ground.SetCell(cellPosition, GroundAtlasSourceID, GrassTileCoords);
				}
				else
				{
					Ground.SetCell(cellPosition, GroundAtlasSourceID, SandTileCoords);
				}

				if (height > 1 && height < 1.2)
				{
					rockyCells.Add(cellPosition);
				}
				else if (height >= 1.1 && height < 1.2)
				{
					grassyCells.Add(cellPosition);
				}
				else if (height >= 1.2 && height < 1.3)
				{
					fencyCells.Add(cellPosition);
				}
				else if (height >= 1.3)
				{
					roadyCells.Add(cellPosition);
				}
			}
		}

		Ground.SetCellsTerrainConnect(rockyCells, 0, 0);
		Ground.SetCellsTerrainConnect(grassyCells, 0, 1);
		Ground.SetCellsTerrainConnect(fencyCells, 0, 5);
		Ground.SetCellsTerrainConnect(roadyCells, 0, 3);

		Water.Clear();
		for (int i = 0; i < ChunkSize; i++)
		{
			for (int j = 0; j < ChunkSize; j++)
			{
				var height = GetHeightAt(x + i, y + j);
				var cellPosition = new Vector2I((int)(x + i), (int)(y + j));

				if (height == 0)
				{
					Water.SetCell(cellPosition, WaterAtlasSourceID, WaterEdgeCoords);
				}
				else if (height < 0)
				{
					Water.SetCell(cellPosition, WaterAtlasSourceID, WaterTileCoords);
				}
			}
		}
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

