using System.Threading.Tasks;
using Godot;

public partial class ShootEffect : Area2D 
{
	public async Task Shoot(Vector2 startPosition, Vector2 targetPosition, float speed)
	{
		var distance = startPosition.DistanceTo(targetPosition);
		var duration = distance / speed;

		var tween = CreateTween()
			.TweenProperty(this, "position", targetPosition, duration)
			.SetTrans(Tween.TransitionType.Linear)
			.SetEase(Tween.EaseType.Out)
			.From(startPosition);
		await ToSignal(tween, Tween.SignalName.Finished);

		QueueFree();
	}
}

