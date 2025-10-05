using Godot;
using System.Collections.Generic;

public partial class ExhibitPopup : Window
{
	private Label titleLabel;
	private VBoxContainer exhibitVBox;

	public override void _Ready()
	{
		titleLabel = GetNode<Label>("VBoxContainer/Title");
		exhibitVBox = GetNode<VBoxContainer>("VBoxContainer/ExhibitScroll/ExhibitVBox");

		CloseRequested += () => Hide();
		Hide();
	}

	public void ShowExhibits(string title, List<Exhibit> exhibits)
	{
		titleLabel.Text = title;

		foreach (Node child in exhibitVBox.GetChildren())
			child.QueueFree();

		foreach (var exhibit in exhibits)
		{
			var hbox = new HBoxContainer
			{
				SizeFlagsHorizontal = Control.SizeFlags.ExpandFill,
				SizeFlagsVertical = Control.SizeFlags.ShrinkCenter
			};

			var iconRect = new TextureRect
			{
				Texture = exhibit.Icon ?? GD.Load<Texture2D>("res://Assets/placeholder.png"),
				StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
				SizeFlagsHorizontal = Control.SizeFlags.ShrinkCenter,
				SizeFlagsVertical = Control.SizeFlags.ShrinkCenter,
				CustomMinimumSize = new Vector2(40, 40)
			};
			hbox.AddChild(iconRect);

			var label = new RichTextLabel
			{
				BbcodeEnabled = false,
				AutowrapMode = TextServer.AutowrapMode.Word,
				FitContent = true,
				Text = $"{exhibit.ObjectType}: {exhibit.Description}",
				SizeFlagsHorizontal = Control.SizeFlags.ExpandFill,
				SizeFlagsVertical = Control.SizeFlags.ShrinkCenter
			};
			hbox.AddChild(label);

			exhibitVBox.AddChild(hbox);
		}

		exhibitVBox.CustomMinimumSize = new Vector2(200, exhibits.Count * 50);
		Size = new Vector2I(400, 200);
		Show();
		this.PopupCentered();
	}
}
