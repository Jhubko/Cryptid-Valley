using Godot;
using System.Collections.Generic;

public partial class Inventory : Node
{
	private List<Exhibit> exhibits = new();

	public void AddExhibit(Exhibit exhibit)
	{
		if (!exhibits.Contains(exhibit))
			exhibits.Add(exhibit);
	}

	public bool HasExhibit(Exhibit exhibit) => exhibits.Contains(exhibit);

	public List<Exhibit> GetExhibits(CryptidType cryptid, ExhibitObjectType objectType)
	{
		return exhibits.FindAll(ex => ex.Cryptid == cryptid && ex.ObjectType == objectType);
	}

	public IReadOnlyList<Exhibit> AllExhibits => exhibits.AsReadOnly();
}
