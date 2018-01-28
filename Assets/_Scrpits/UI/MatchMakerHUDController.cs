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
	private Person _previousPickedObject;

	private Person _pickedPerson;

	

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
		fullName.text = _pickedPerson.name;
		AggressionField.text = string.Format(AggressionFormat, _pickedPerson.attributes.aggression);
		CharismaField.text = string.Format(CharismaFormat, _pickedPerson.attributes.charisma);
		PopularityField.text = string.Format(PopularityFormat, _pickedPerson.attributes.popularity);
	}

	private void Update()
	{
		bool pickerIsFocused = _pickerFocused;
		bool pickerHasChanged = (pickerWasFocused != pickerIsFocused);

		_inspectorPanel.SetActive(pickerIsFocused);

		if(pickerIsFocused && pickerHasChanged)
		{
			_pickedPerson = Kernal.instance.store.people[_picker.pickedObject.GetComponent<PersonTag>().storeID];
			UpdateIDCard(); // need to switch to event based later
		}

		pickerWasFocused = _pickerFocused;
	}
}
