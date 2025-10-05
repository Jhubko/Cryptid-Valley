using Godot;
using System.Collections.Generic;

public partial class ExhibitSpot : Area2D
{
    [Export] public ExhibitObjectType ObjectType;

    private CryptidType cryptid = CryptidType.Empty;
    private ExhibitPopup popup;

    private InventoryUI inventoryUI;

    public void SetPopup(ExhibitPopup p) => popup = p;
    public void SetCryptid(CryptidType newCryptid) => cryptid = newCryptid;

    public override void _Ready()
    {
        inventoryUI = GetNodeOrNull<InventoryUI>("/root/Museum/CanvasLayer/Inventory");
    }

    public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
    {
        if (@event is InputEventMouseButton mouseEvent &&
            mouseEvent.Pressed &&
            mouseEvent.ButtonIndex == MouseButton.Left &&
            popup != null &&
            inventoryUI != null)
        {
            List<Exhibit> exhibits = inventoryUI.GetExhibits(cryptid, ObjectType);

            string title = $"{ObjectType} - {cryptid}";
            popup.ShowExhibits(title, exhibits);
        }
    }
}
