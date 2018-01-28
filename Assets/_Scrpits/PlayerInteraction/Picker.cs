using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject>
{

}

public class Picker : MonoBehaviour
{
	[SerializeField]
	private LayerMask pickerMask;

	// Object picked in the last pick (if any)
	public GameObject pickedObject {get; private set;}

	public GameObjectEvent OnObjectPicked;

	public AudioSource ass { get { return Kernal.instance.ass;}}

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
				if(OnObjectPicked != null)
				{
					OnObjectPicked.Invoke(pickedObject);

					// try civvie noise
					PersonTag tagged = pickedObject.GetComponent<PersonTag>();
					switch(Kernal.instance.store.people[tagged.storeID].gender)
					{
						case Gender.Male:
							ass.PlayOneShot(Kernal.instance.maleSounds[Random.Range(0, Kernal.instance.maleSounds.Length)]);
							break;
						case Gender.Female:
							ass.PlayOneShot(Kernal.instance.femaleSounds[Random.Range(0, Kernal.instance.maleSounds.Length)]);
							break;
						default:
							ass.PlayOneShot(Kernal.instance.femaleSounds[Random.Range(0, Kernal.instance.femaleSounds.Length)]);
							break;
					}
				}
			}
		}
	}
}
