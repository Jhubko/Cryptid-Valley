using Godot;
using System;
using System.Collections.Generic;

public partial class CryptidManager : Node
{
    public static CryptidManager Instance;

    private List<CryptidType> availableCryptids = new List<CryptidType>();

    public override void _Ready()
    {
        if (Instance == null) Instance = this;

        // Wszystkie cryptidy dostępne na start (oprócz Empty)
        availableCryptids = new List<CryptidType>((CryptidType[])Enum.GetValues(typeof(CryptidType)));
        availableCryptids.Remove(CryptidType.Empty);
    }

    public List<CryptidType> GetAvailableCryptids()
    {
        return new List<CryptidType>(availableCryptids);
    }

    public bool ReserveCryptid(CryptidType cryptid)
    {
        if (cryptid == CryptidType.Empty) return false;

        if (availableCryptids.Contains(cryptid))
        {
            availableCryptids.Remove(cryptid);
            return true;
        }
        return false;
    }

    public void ReleaseCryptid(CryptidType cryptid)
    {
        if (cryptid != CryptidType.Empty && !availableCryptids.Contains(cryptid))
            availableCryptids.Add(cryptid);
    }
}
