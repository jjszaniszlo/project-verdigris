using Godot;
using GodotUtilities;

public partial class DeathSoundComponent : AudioStreamPlayer2D
{
	[Export]
	public DieComponent DieComponent {get; private set; }

	public override void _Ready()
	{
		DieComponent.Died += OnDied;
	}

	private void OnDied()
	{
		Play();
	}
}

