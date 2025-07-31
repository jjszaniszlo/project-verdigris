using System.Linq;
using Godot;
using Godot.Collections;
using GodotUtilities;
using Scenes.Components;

[Scene]
public partial class EnemySpawner : Node2D
{
	private int _Difficulty = 0;
	[Export]
	public int Difficulty
	{
		get => _Difficulty;
		set
		{
			_Difficulty = value;
			UpdateSpawnTimer();
		}
	}

	[Export]
	public Array<EnemySpawn> EnemyScenes { get; private set; } = new();

	[Export]
	public CameraFollowComponent CameraFollowComponent { get; private set; }

	public SpawnPointComponent SpawnPointComponent { get; private set; }

	[ExportCategory("Spawn Settings")]
	[Export]
	public float SpawnCooldown { get; private set; } = 15.0f;
	[Export]
	public float DifficultyCooldownMultiplier { get; private set; } = 0.5f;
	[Export]
	public float MinSpawnCooldown { get; private set; } = 10.0f;

	public float CurrentSpawnCooldown => Mathf.Max(SpawnCooldown - (Difficulty * DifficultyCooldownMultiplier), MinSpawnCooldown);

	[Export]
	public float SpawnAmount { get; private set; } = 5.0f;
	[Export]
	public float DifficultySpawnMultiplier { get; private set; } = 1f;

	public int CurrentMaxSpawnAmount => (int)(SpawnAmount + (Difficulty * DifficultySpawnMultiplier));

	[Export]
	public int HardMaxEnemyCount { get; private set; } = 100;

	[Export]
	public int BaseMaxEnemyCount { get; private set; } = 20;

	[Export]
	public int AddEnemyCountPerDifficulty { get; private set; } = 5;

	public int CurrentMaxEnemyCount => Mathf.Min(BaseMaxEnemyCount + Difficulty * AddEnemyCountPerDifficulty, HardMaxEnemyCount);
	public int CurrentEnemyCount => GetChildCount();
	public int CurrentSpawnAmount => Mathf.Clamp(0, CurrentMaxSpawnAmount, CurrentMaxEnemyCount - CurrentEnemyCount);
	public bool CanSpawn => CurrentEnemyCount < CurrentMaxEnemyCount;

	private Timer _SpawnTimer = new()
	{
		ProcessCallback = Timer.TimerProcessCallback.Physics,
		OneShot = false,
		Autostart = true
	};

	private Timer _DifficultyTimer = new()
	{
		WaitTime = 60.0f,
		ProcessCallback = Timer.TimerProcessCallback.Physics,
		OneShot = false,
		Autostart = true
	};

	public override void _Ready()
	{
		SpawnPointComponent = CameraFollowComponent.SpawnPointComponent;

		_SpawnTimer.WaitTime = CurrentSpawnCooldown;
		_SpawnTimer.Timeout += SpawnEnemies;
		AddChild(_SpawnTimer);

		_DifficultyTimer.Timeout += IncreaseDiffuculty;
		AddChild(_DifficultyTimer);

		SpawnEnemies();
	}

	private void IncreaseDiffuculty()
	{
		Difficulty++;
		UpdateSpawnTimer();
	}

	private void SpawnEnemies()
	{
		if (!CanSpawn) return;

		for (int i = 0; i < CurrentSpawnAmount; i++)
		{
			var enemyScene = GetRandomEnemyScene();
			if (enemyScene == null) continue;

			var enemyInstance = enemyScene.Instantiate<Node2D>();
			enemyInstance.GlobalPosition = SpawnPointComponent.RandomSpawnPoint() + new Vector2(GD.Randf() * 10, GD.Randf() * 10);
			enemyInstance.GetNodeOrNull<DifficultyModifierComponent>("DifficultyModifierComponent")?.ModifyDifficulty(Difficulty);
			AddChild(enemyInstance);
		}
	}

	private PackedScene GetRandomEnemyScene()
	{
		if (EnemyScenes.Count == 0)
		{
			return null;
		}

		var totalWeight = EnemyScenes.Sum(enemySpawn => enemySpawn.Weight);
		float randomWeight = GD.Randf() * totalWeight;
		float cumulativeWeight = 0.0f;

		foreach (var enemySpawn in EnemyScenes)
		{
			cumulativeWeight += enemySpawn.Weight;
			if (randomWeight < cumulativeWeight)
			{
				return enemySpawn.Scene;
			}
		}

		throw new System.Exception("Failed to select a random enemy scene. This should never happen.");
	}
	
	private void UpdateSpawnTimer()
	{
		_SpawnTimer.WaitTime = CurrentSpawnCooldown;
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

