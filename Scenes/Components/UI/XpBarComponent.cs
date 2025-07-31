using System;
using Godot;
using GodotUtilities;

[Scene]
public partial class XpBarComponent : TextureProgressBar
{
	[Node]
	public Label Label { get; private set; }

	public override void _Ready()
	{
		Changed += OnChanged;
		ValueChanged += OnValueChanged;
	}

    private void OnValueChanged(double value)
    {
		Label.Text = $"Next Level: {Mathf.Round(MaxValue - value)} XP";
	}

    private void OnChanged()
    {
		Label.Text = $"Next Level: {Mathf.Round(MaxValue - Value)} XP";
    }

    public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

}

