using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocializePrompt : MonoBehaviour
{
	[SerializeField]
	private CompareHUDController compareHUD;

	public Button confirmButton;
	public Button denyButton;

	public bool isReadyToSocialize
	{
		get
		{
			return compareHUD.isReadyToCompare;
		}
	}

	void Update()
	{
		confirmButton.interactable = isReadyToSocialize; //denyButton.interactable = isReadyToSocialize;
	}

	public void TriggerSocalizePrompt()
	{
		Kernal.instance.store.people[compareHUD.thingsToCompare[0].id].aiController.ForceInteract(
			Kernal.instance.store.people[compareHUD.thingsToCompare[1].id].aiController
		);
	}
}
