using Godot;

public class Exhibit
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Attractiveness { get; private set; }
    public CryptidType Cryptid { get; private set; }
    public ExhibitObjectType ObjectType { get; private set; }
    public Texture2D Icon { get; private set; }

    public Exhibit(string name, string description, int attractiveness,
                   CryptidType cryptid, ExhibitObjectType objectType, Texture2D icon)
    {
        Name = name;
        Description = description;
        Attractiveness = attractiveness;
        Cryptid = cryptid;
        ObjectType = objectType;
        Icon = icon;
    }
}
