using System.Linq;
using Godot;
using Godot.Collections;
using GodotUtilities;

namespace Scenes.UI.Game.UpgradeMenu;

[Scene]
public partial class UpgradeCardsUI : Control
{
	[Export]
	public Curve HandCurve { get; set; }
	[Export]
	public Curve RotationCurve { get; set; }

	[Export(PropertyHint.Range, "0.0, 360, radians_as_degrees")]
	public float MaxRotation { get; set; } = 0.0f;
	[Export]
	public float XSeparation { get; set; } = -10.0f;
	[Export]
	public float YMin { get; set; } = 0.0f;
	[Export]
	public float YMax { get; set; } = -15.0f;

	public override void _Ready()
	{
		UpdateCards();

		CreateTween()
			.SetProcessMode(Tween.TweenProcessMode.Physics)
			.TweenProperty(this, "modulate", new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.5f)
			.From(new Color(1.0f, 1.0f, 1.0f, 0.0f));
	}

	public override void _PhysicsProcess(double delta)
	{
		UpdateCards();
	}

	public void AddUpgradeCard(Card card)
	{
		AddChild(card);
		UpdateCards();
	}

	public void SetUpgrades(Array<Resource> upgrades)
	{
		foreach (var child in GetChildren())
		{
			child.QueueFree();
		}
		foreach (var upgrade in upgrades)
		{
			if (upgrade is BaseUpgrade baseUpgrade)
			{
				var card = ResourceLoader.Load<PackedScene>("res://Scenes/UI/Game/UpgradeMenu/card.tscn").Instantiate<Card>();
				baseUpgrade.ApplyToUpgradeCardUIComponent(card);
        		card.Upgrade = baseUpgrade;
				AddUpgradeCard(card);
			}
		}
	}

	private void UpdateCards()
	{
		var cardCount = GetChildCount();
		if (cardCount <= 0) return;

		var cardWidth = GetChildOrNull<Card>(0)?.Size.X ?? 0;
		var totalCardWidths = cardWidth * cardCount + XSeparation * (cardCount - 1);
		var finalXSeparation = XSeparation;

		if (totalCardWidths > Size.X)
		{
			finalXSeparation = (Size.X - cardWidth * cardCount) / (cardCount - 1);
			totalCardWidths = Size.X;
		}

		var offset = (Size.X - totalCardWidths) / 2.0f;

		foreach (var card in GetChildren().Select((c, i) => new { Card = (Card)c, Index = i }))
		{
			var yMultiplier = HandCurve.Sample(1.0f / (cardCount - 1.0f) * card.Index);
			var rotMultiplier = RotationCurve.Sample(1.0f / (cardCount - 1.0f) * card.Index);

			if (cardCount == 1)
			{
				yMultiplier = 0.0f;
				rotMultiplier = 0.0f;
			}

			var cardX = offset + (cardWidth + finalXSeparation) * card.Index;
			var cardY = YMin + YMax * yMultiplier;

			card.Card.Position = new Vector2(cardX, cardY);
			card.Card.Rotation = MaxRotation * rotMultiplier;
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

