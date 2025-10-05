using Godot;
using System;
using System.Numerics;

public partial class CryptidZone : Node2D
{
	[Export] private NodePath boardPath;
	[Export] private NodePath cabinetPath;
	[Export] private NodePath cagePath;
	[Export] private NodePath popupPath;
	[Export] private NodePath selectPopupPath;
	[Export] private NodePath buttonSelectCryptidPath;

	private ExhibitSpot board;
	private ExhibitSpot cabinet;
	private ExhibitSpot cage;

	private ExhibitPopup popup;
	private CryptidSelectPopup selectPopup;

	private Button selectButton;

	private CryptidType currentCryptid = CryptidType.Empty;

	public override void _Ready()
	{
		popup = GetNodeOrNull<ExhibitPopup>(popupPath);

		board = GetNodeOrNull<ExhibitSpot>(boardPath);
		cabinet = GetNodeOrNull<ExhibitSpot>(cabinetPath);
		cage = GetNodeOrNull<ExhibitSpot>(cagePath);

		if (board != null) board.SetPopup(popup);
		if (cabinet != null) cabinet.SetPopup(popup);
		if (cage != null) cage.SetPopup(popup);

		selectPopup = GetNodeOrNull<CryptidSelectPopup>(selectPopupPath);
		if (selectPopup != null)
			selectPopup.OnCryptidSelected += OnCryptidChosen;

		selectButton = GetNodeOrNull<Button>(buttonSelectCryptidPath);
		if (selectButton != null)
			selectButton.Pressed += () =>
			{
				if (selectPopup != null)
				{
					selectPopup.RefreshOptions(currentCryptid);
					selectPopup.PopupCentered();
				}
			};

	}

	public void SetCryptid(CryptidType chosen)
	{
		if (chosen == CryptidType.Empty)
		{
			if (currentCryptid != CryptidType.Empty)
				CryptidManager.Instance.ReleaseCryptid(currentCryptid);

			currentCryptid = CryptidType.Empty;

			if (board != null) board.SetCryptid(currentCryptid);
			if (cabinet != null) cabinet.SetCryptid(currentCryptid);
			if (cage != null) cage.SetCryptid(currentCryptid);
			return;
		}

		if (currentCryptid != CryptidType.Empty)
			CryptidManager.Instance.ReleaseCryptid(currentCryptid);

		if (CryptidManager.Instance.ReserveCryptid(chosen))
		{
			currentCryptid = chosen;

			if (board != null) board.SetCryptid(chosen);
			if (cabinet != null) cabinet.SetCryptid(chosen);
			if (cage != null) cage.SetCryptid(chosen);
		}
	}

	private void OnCryptidChosen(CryptidType chosen)
	{
		SetCryptid(chosen);
		selectPopup.Hide();
	}
}
