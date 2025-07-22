// meta-name: GodotUtilities Template
// meta-description: GodotUtilities Boilerplate for a script at the root of a scene.
// meta-default: true
// meta-space-indent: 4

using _BINDINGS_NAMESPACE_;
using GodotUtilities;

[Scene]
public partial class _CLASS_ : _BASE_
{
    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _Ready()
    {
    }

    public override void _Process(double delta)
    {
    }
}
