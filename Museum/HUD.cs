using Godot;

public partial class HUD : Control
{
	private Label moneyLabel;
	private Label visitorsLabel;
	private Label attractionLabel;

	public override void _Ready()
	{
		moneyLabel = GetNode<Label>("HUD/VBoxContainer/MoneyLabel");
		visitorsLabel = GetNode<Label>("HUD/VBoxContainer/VisitorsLabel");
		attractionLabel = GetNode<Label>("HUD/VBoxContainer/AttractionLabel");

		UpdateMoney(0);
		UpdateVisitors(0);
		UpdateAttraction(0);
	}

	public void UpdateMoney(int money)
	{
		moneyLabel.Text = $"💰 Pieniądze: {money}";
	}

	public void UpdateVisitors(int visitors)
	{
		visitorsLabel.Text = $"👥 Goście: {visitors}";
	}

	public void UpdateAttraction(int attraction)
	{
		attractionLabel.Text = $"⭐ Atrakcyjność: {attraction}";
	}
}
