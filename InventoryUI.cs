using Godot;
using System.Collections.Generic;

public partial class InventoryUI : Control
{
	[Export] public PackedScene ExhibitSlotScene;
	private VBoxContainer vbox;
	private List<Exhibit> exhibits = new();

	private string jsonPath = "res://exhibits.json"; // ścieżka do pliku JSON

	public override void _Ready()
	{
		vbox = GetNode<VBoxContainer>("ScrollContainer/VBoxContainer");

		// Wczytaj istniejący ekwipunek
		LoadInventory();
		Populate();

		// Dodaj nowy przedmiot z poziomu kodu
		var newExhibit = new Exhibit(
			"Zdjęcie Nessie",
			"Tajemnicze zdjęcie z jeziora Loch Ness",
			88,
			CryptidType.Bigfoot,
			ExhibitObjectType.Board,
			GD.Load<Texture2D>("res://icon.svg")
		);

		AddExhibit(newExhibit);
	}


	public void LoadInventory()
	{
		exhibits = ExhibitLoader.LoadFromFile();
	}

	public void SaveInventory()
	{
		ExhibitLoader.SaveToFile(exhibits);
	}

	public List<Exhibit> GetExhibits(CryptidType cryptid, ExhibitObjectType type)
	{
		return exhibits.FindAll(e => e.Cryptid == cryptid && e.ObjectType == type);
	}

	private void Populate()
	{
		foreach (Node child in vbox.GetChildren())
			child.QueueFree();

		foreach (var ex in exhibits)
		{
			var slot = ExhibitSlotScene.Instantiate<Button>();
			var texture = slot.GetNode<TextureRect>("HBoxContainer/TextureRect");
			var label = slot.GetNode<Label>("HBoxContainer/Label");

			texture.Texture = ex.Icon;
			texture.CustomMinimumSize = new Vector2(40, 40);
			texture.StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered;

			label.Text = ex.Name;

			slot.Pressed += () => OnExhibitSelected(ex);

			vbox.AddChild(slot);
		}
	}

	private void OnExhibitSelected(Exhibit ex)
	{
		GD.Print($"Wybrano: {ex.Name} | {ex.Description} | Atrakcyjność: {ex.Attractiveness}");
	}

	// Dodatkowo możesz dodać metody do dodawania/usuwania przedmiotów w runtime:
	public void AddExhibit(Exhibit ex)
	{
		exhibits.Add(ex);
		Populate();
		SaveInventory();
	}

	public void RemoveExhibit(Exhibit ex)
	{
		exhibits.Remove(ex);
		Populate();
		SaveInventory();
	}
}
