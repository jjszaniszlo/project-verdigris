using System;
using Godot;
using GodotUtilities;

[Scene]
public partial class HealthBarComponent : TextureProgressBar
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
		Label.Text = $"{Mathf.Round(value)} / {Mathf.Round(MaxValue)}";
    }

    private void OnChanged()
	{
		Label.Text = $"{Mathf.Round(Value)} / {Mathf.Round(MaxValue)}";
	}

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}

