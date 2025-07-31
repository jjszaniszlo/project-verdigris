using System.Threading.Tasks;
using Godot;
using GodotUtilities;

[Scene]
public partial class DamageNumbers : Node 
{
	public static DamageNumbers Instance { get; private set; }

	[Export]
	public LabelSettings LabelSettings { get; private set; }

	public override void _Ready()
	{
		Instance = this;
	}

	public static async void DisplayDamageNumber(int damage, Vector2 position, bool isCritical)
	{
        var number = new Label
        {
            GlobalPosition = position,
            Text = damage.ToString(),
            ZIndex = 5,
            LabelSettings = Instance.LabelSettings
        };

        var color = isCritical ? new Color(1, 0, 0) : new Color(1, 1, 1);
		number.LabelSettings.FontColor = color;

		Instance.AddChildDeferred(number);

		await Instance.ToSignal(number, Control.SignalName.Resized);
		number.PivotOffset = number.Size / 2;

		var tween = Instance.CreateTween()
			.SetProcessMode(Tween.TweenProcessMode.Physics)
			.SetParallel(true);
		tween.TweenProperty(number, "position:y", number.Position.Y - 24, 0.25f)
			.SetEase(Tween.EaseType.Out);		
		tween.TweenProperty(number, "position:y", number.Position.Y, 0.25f)
			.SetDelay(0.25f)
			.SetEase(Tween.EaseType.In);	
		tween.TweenProperty(number, "scale", Vector2.Zero, 0.25f)
			.SetDelay(0.5f)
			.SetEase(Tween.EaseType.In);

		await Instance.ToSignal(tween, Tween.SignalName.Finished);
		number.QueueFree();
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

