using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ComparisonData
{
	public int id;
	public Text idNameField;
	public Text idGenderField;
	public Slider desireSlider;
}

public class CompareHUDController : MonoBehaviour
{
	public bool[] thingsOpen = new bool[2];
	public ComparisonData[] thingsToCompare = new ComparisonData[2];
	public bool isReadyToCompare
	{
		get
		{
			foreach(var thing in thingsOpen)
			{
				if(thing == true) {return false;}
			}

			return true;
		}
	}

	public void AssignPerson(Person person, int idx)
	{
		ComparisonData comp = thingsToCompare[idx];
		comp.idNameField.text = person.name;
		comp.idGenderField.text = person.gender.ToString();
		comp.id = person.aiController.GetComponent<PersonTag>().storeID;

		var personID = comp.id;
		comp.desireSlider.value = 0.0f;
		thingsOpen[idx] = false;

		// can we calc sliders?
		if(isReadyToCompare)
			PopulateDesireSliders();

		// can we assign focus?
		Kernal.instance.focusedIndividuals.Clear();
		foreach(var foc in thingsToCompare)
		{
			if(foc != null)
			{
				Kernal.instance.focusedIndividuals.Add(Kernal.instance.store.people[foc.id]);
			}
		}
}

	public void RemovePerson(int index)
	{
		if(thingsOpen[index]) { return; }
		thingsToCompare[index].idNameField.text = "";
		thingsToCompare[index].idGenderField.text = "";
		thingsToCompare[index].desireSlider.value = 0.0f;

		thingsOpen[index] = true;

		for(int i = 0; i < thingsToCompare.Length; ++i)
		{
			thingsToCompare[i].desireSlider.value = 0.0f;
		}
	}

	public void PopulateDesireSliders()
	{
		for(int i = 0; i < thingsToCompare.Length; ++i)
		{
			int curNum = thingsToCompare[i].id;
			float desireAvg = 0.0f;
			for(int j = 0; j < thingsToCompare.Length - 1; ++j)
			{
				if(Kernal.instance.store.socialRecords[curNum].ContainsKey(thingsToCompare[j % thingsToCompare.Length].id) == false) { continue; }
				desireAvg += Kernal.instance.store.socialRecords[curNum][thingsToCompare[j % thingsToCompare.Length].id].desire;
			}
			desireAvg /= thingsToCompare.Length - 1;
			thingsToCompare[i].desireSlider.value = desireAvg / 100.0f;
		}
	}

	public void HandlePlayerPick(GameObject potentialPick)
	{
		var tag = potentialPick.GetComponent<PersonTag>();
		for(int i = 0; i < thingsOpen.Length; ++i)
		{
			if(thingsToCompare[i].id == tag.storeID) { return;}
			if(thingsOpen[i] == true)
			{
				AssignPerson(Kernal.instance.store.people[tag.storeID],
				i);
				break;
			}
		}
	}
}
