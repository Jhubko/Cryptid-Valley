using Godot;

public partial class CameraController : Camera2D
{
	[Export] public float ZoomSpeed = 0.1f;
	[Export] public float MinZoom = 0.5f;
	[Export] public float MaxZoom = 3.0f;

	private Vector2 dragStart;
	private bool dragging = false;

	public override void _Process(double delta)
	{
		bool leftDown = Input.IsMouseButtonPressed(MouseButton.Left);

		if (leftDown && !dragging)
		{
			dragging = true;
			dragStart = GetViewport().GetMousePosition();
		}

		if (!leftDown && dragging)
		{
			dragging = false;
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton)
		{
			if (mouseButton.ButtonIndex == MouseButton.WheelDown && mouseButton.Pressed)
			{
				Zoom -= new Vector2(ZoomSpeed, ZoomSpeed);
				Zoom = Zoom.Clamp(new Vector2(MinZoom, MinZoom), new Vector2(MaxZoom, MaxZoom));
			}
			else if (mouseButton.ButtonIndex == MouseButton.WheelUp && mouseButton.Pressed)
			{
				Zoom += new Vector2(ZoomSpeed, ZoomSpeed);
				Zoom = Zoom.Clamp(new Vector2(MinZoom, MinZoom), new Vector2(MaxZoom, MaxZoom));
			}
		}

		if (@event is InputEventMouseMotion motion && dragging)
		{
			Vector2 diff = motion.Relative;
			Position -= diff / Zoom;
		}
	}
}
