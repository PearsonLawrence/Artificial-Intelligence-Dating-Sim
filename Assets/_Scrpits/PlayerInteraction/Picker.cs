using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
	[SerializeField]
	private LayerMask pickerMask;

	// Object picked in the last pick (if any)
	public GameObject pickedObject {get; private set;}

	Vector2 cursorPositionScreenSpace
	{
		get
		{
			return Input.mousePosition;
		}
	}

	Vector3 cursorPositionWorldSpace
	{
		get
		{
			return Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}

	void Update()
	{
		// did a click occur?
		if(Input.GetButtonDown("Fire1"))
		{
			pickedObject = null;
			Ray pickerRay = Camera.main.ScreenPointToRay(cursorPositionScreenSpace);

			RaycastHit hit;
			if(Physics.Raycast(pickerRay, out hit, Mathf.Infinity, pickerMask))
			{
				pickedObject = hit.collider.gameObject;
			}
		}
	}
}
