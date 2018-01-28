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
	private int _pickedPersonID;

	[SerializeField]
	private GameObject _inspectorPanel;
	
	[Header("ID Card")]
	public Text fullName;
	public Text genderField;

	public string GenderFormat = "Gender: {0}";

	[Header("Attributes")]
	public Text AggressionField;
	public Text CharismaField;
	public Text PopularityField;
	public Text MoodField;
	
	public string AggressionFormat = "Aggression | {0}";
	public string CharismaFormat = "Charisma | {0}";
	public string PopularityFormat = "Popularity | {0}";
	public string MoodFormat = "Mood | {0}";

	[Header("Social")]
	public RectTransform SocialMediaContentPanel;
	public GameObject SocialMediaEntryPrefab;

	[Header("Debug")]
	[SerializeField]
	private bool alwaysUpdate = true;

	public void UpdateIDCard()
	{
		fullName.text = _pickedPerson.name;
		AggressionField.text = string.Format(AggressionFormat, _pickedPerson.attributes.aggression);
		CharismaField.text = string.Format(CharismaFormat, _pickedPerson.attributes.charisma);
		PopularityField.text = string.Format(PopularityFormat, _pickedPerson.attributes.popularity);
		genderField.text = string.Format(GenderFormat, _pickedPerson.gender.ToString());
		//MoodFormat.text = string.Format(MoodFormat, _pickedPerson.aiController.mood)

		// social
		
		int socialCount = Kernal.instance.store.socialRecords[_pickedPersonID].Count;
		int childCount = SocialMediaContentPanel.childCount;
		int childDelta =  socialCount - SocialMediaContentPanel.childCount;

		// need more
		if(childDelta > 0)
		{
			for(int i = 0; i < childDelta; ++i)
			{
				GameObject babySocial = Instantiate(SocialMediaEntryPrefab,
													transform.position,
													Quaternion.identity,
													SocialMediaContentPanel);
				babySocial.transform.SetAsFirstSibling();
			}
		}
		// need less
		else if (childDelta < 0)
		{
			for(int i = childDelta; i < 0; ++i)
			{
				SocialMediaContentPanel.GetChild(childCount + i).gameObject.SetActive(false);
			}
		}

		int mediaEntry = 0;
		foreach(var entry in Kernal.instance.store.socialRecords[_pickedPersonID].Keys)
		{
			var controller = SocialMediaContentPanel.GetChild(mediaEntry).GetComponent<SocialMediaEntryController>();
			mediaEntry++;
			var target = Kernal.instance.store.people[entry];

			controller.nameField.text = target.name;
			controller.statusField.text = "???";
		}
	}

	private void Update()
	{
		bool pickerIsFocused = _pickerFocused;
		bool pickerHasChanged = (pickerWasFocused != pickerIsFocused);

		_inspectorPanel.SetActive(pickerIsFocused);

		if(pickerIsFocused && (pickerHasChanged || alwaysUpdate))
		{
			_pickedPersonID = _picker.pickedObject.GetComponent<PersonTag>().storeID;
			_pickedPerson = Kernal.instance.store.people[_pickedPersonID];
			UpdateIDCard(); // need to switch to event based later
		}

		pickerWasFocused = _pickerFocused;
	}
}
