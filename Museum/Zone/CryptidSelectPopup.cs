using Godot;
using System;
using System.Collections.Generic;

public partial class CryptidSelectPopup : Window
{
    public Action<CryptidType> OnCryptidSelected;

    public OptionButton optionButton;

    public override void _Ready()
    {
        Hide();
        this.CloseRequested += () => Hide();
        optionButton = GetNode<OptionButton>("VBoxContainer/OptionButton");
        optionButton.ItemSelected += OnItemSelected;
    }

    private CryptidType currentCryptid = CryptidType.Empty;

    public void RefreshOptions(CryptidType selectedCryptid)
    {
        currentCryptid = selectedCryptid;
        optionButton.Clear();

        optionButton.AddItem("Empty");

        List<CryptidType> available = new List<CryptidType>(CryptidManager.Instance.GetAvailableCryptids());

        if (selectedCryptid != CryptidType.Empty && !available.Contains(selectedCryptid))
        {
            available.Insert(0, selectedCryptid);
        }

        foreach (var cryptid in available)
        {
            optionButton.AddItem(cryptid.ToString());
        }

        if (selectedCryptid == CryptidType.Empty)
        {
            optionButton.Selected = 0;
        }
        else
        {
            int idx = available.IndexOf(selectedCryptid);
            optionButton.Selected = idx + 1;
        }
    }

    private void OnItemSelected(long idx)
    {
        if (idx == 0)
        {
            OnCryptidSelected?.Invoke(CryptidType.Empty);
        }
        else
        {
            var available = new List<CryptidType>(CryptidManager.Instance.GetAvailableCryptids());
            if (currentCryptid != CryptidType.Empty && !available.Contains(currentCryptid))
            {
                available.Insert(0, currentCryptid);
            }
            int listIdx = (int)idx - 1;
            if (listIdx >= 0 && listIdx < available.Count)
            {
                OnCryptidSelected?.Invoke(available[listIdx]);
            }
        }
    }
}
