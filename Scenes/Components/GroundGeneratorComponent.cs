using Godot;
using GodotUtilities;

namespace Scenes.Components;

[Scene]
public partial class GroundGeneratorComponent : Node
{
	[Export]
	public TileMapLayer Ground { get; private set; }

	[Export]
	public int TileSize { get; set; } = 32;
	[Export]
	public int ChunkSize { get; set; } = 16;
	[Export]
	public int HeightGradientScale { get; set; } = 8;

	private Vector2I SandAtlasCoords { get; set; } = new Vector2I(0, 0);
	private Vector2I WaterAtlasCoords { get; set; } = new Vector2I(1, 0);
	private Vector2I GrassAtlasCoords { get; set; } = new Vector2I(2, 0);

	private int ChunkViewDistance => (ChunkSize / 2) - 1;

	private CharacterBody2D _player;

	public override void _Ready()
	{
		SetPhysicsProcess(false);
		LoadChunk(0, 0);

		Globals.Instance.PlayerChanged += (player) =>
		{
			SetPhysicsProcess(true);
			_player = player;
		};
	}

	public override void _PhysicsProcess(double delta)
	{
		var worldSize = Ground.GetUsedRect();
		var worldSizeStart = worldSize.Position;
		var worldSizeEnd = worldSize.End;
		var playerViewPositionPositive = (_player.Position / TileSize) + new Vector2(ChunkViewDistance, ChunkViewDistance);
		var playerViewPositionNegative = (_player.Position / TileSize) - new Vector2(ChunkViewDistance, ChunkViewDistance);

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

		for (int i = 0; i < ChunkSize; i++)
		{
			for (int j = 0; j < ChunkSize; j++)
			{
				float height = GetHeightAt(x + i, y + j);
				var cellPosition = new Vector2I((int)(x + i), (int)(y + j));

                Vector2I atlasCoords;
                if (height < 0)
                {
                    atlasCoords = WaterAtlasCoords;
                }
                else if (height > 1)
				{
					atlasCoords = GrassAtlasCoords;
				}
				else
                {
                    atlasCoords = SandAtlasCoords;
                }

                Ground.SetCell(cellPosition, 0, atlasCoords);
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

