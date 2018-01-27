using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchMakerHUDController : MonoBehaviour
{
	[SerializeField]
	private Picker _picker;

	private bool _pickerFocused
	{
		get
		{
			return _picker.pickedObject != null;
		}
	}

	[SerializeField]
	private GameObject _inspectorPanel;
	
	private void Update()
	{
		_inspectorPanel.SetActive(_pickerFocused);
	}
}
