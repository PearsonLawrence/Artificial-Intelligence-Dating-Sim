using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchMakerHUDController : MonoBehaviour
{
	[SerializeField]
	private Picker _picker;

	private bool pickerWasFocused;
	private bool _pickerFocused
	{
		get
		{
			return _picker.pickedObject != null;
		}
	}
	private bool _pickedPerson;

	[SerializeField]
	private GameObject _inspectorPanel;
	
	[Header("ID Card")]
	public Text fullName;

	[Header("Attributes")]
	public Text AggressionField;
	public Text CharismaField;
	public Text PopularityField;

	public string AggressionFormat = "Aggression | {0}";
	public string CharismaFormat = "Charisma | {0}";
	public string PopularityFormat = "Popularity | {0}";

	public void UpdateIDCard()
	{
		fullName.text = "John Doe";
		AggressionField.text = string.Format(AggressionFormat, 0);
		CharismaField.text = string.Format(CharismaFormat, 0);
		PopularityField.text = string.Format(PopularityFormat, 0);
	}

	private void Update()
	{
		bool pickerIsFocused = _pickerFocused;
		_inspectorPanel.SetActive(pickerIsFocused);

		bool pickerHasChanged = pickerWasFocused != pickerIsFocused;

		if(pickerIsFocused && pickerHasChanged)
		{
			UpdateIDCard(); // need to switch to event based later
		}

		pickerWasFocused = _pickedPerson;
	}
}
