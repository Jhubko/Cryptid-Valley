using Godot;

public partial class CryptidSelector : Control
{
	[Export] private NodePath zonePath;
	private CryptidZone zone;

	public override void _Ready()
	{
		zone = GetNode<CryptidZone>(zonePath);
	}

	private void OnSelectBigfoot()
	{
		zone.SetCryptid(CryptidType.Bigfoot);
	}

	private void OnSelectChupacabra()
	{
		zone.SetCryptid(CryptidType.Chupacabra);
	}
}
