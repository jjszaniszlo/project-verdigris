using Godot;
using GodotUtilities;

[Scene]
public partial class Card : Control
{
	[Node]
	public RichTextLabel TextComponent { get; set; }

	[Node]
	public TextureButton TextureButtonComponent { get; set; }

	public Resource Upgrade { get; set; }

	public override void _Ready()
	{
		MouseEntered += OnHover;
		MouseExited += OffHover;
		FocusEntered += OnHover;
		FocusExited += OffHover;

		TextureButtonComponent.Pressed += () =>
		{
			GameEventBus.Instance.EmitUpgradeSelected(Upgrade);
		};
	}

	private void OnHover()
	{
		CreateTween()
			.SetProcessMode(Tween.TweenProcessMode.Physics)
			.TweenProperty(this, "scale", new Vector2(1.2f, 1.2f), 0.2f);
	}

	private void OffHover()
	{
		CreateTween()
			.SetProcessMode(Tween.TweenProcessMode.Physics)
			.TweenProperty(this, "scale", new Vector2(1.0f, 1.0f), 0.2f)
			.FromCurrent();
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

