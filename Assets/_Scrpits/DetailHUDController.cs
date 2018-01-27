using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailHUDController : MonoBehaviour
{
	public Text fullName;

	public Person trackerPerson;

	public void UpdateIDCard()
	{
		fullName.text = "John Doe";
	}
}
