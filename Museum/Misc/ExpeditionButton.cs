using Godot;

public partial class ExpeditionButton : Button
{
	public override void _Pressed()
	{
		GD.Print("Wyruszamy na ekspedycję!");
		// Tu później zmienimy scenę na Expedition.tscn
	}
}
