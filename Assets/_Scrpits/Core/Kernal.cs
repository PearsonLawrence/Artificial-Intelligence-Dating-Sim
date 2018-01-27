using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kernal : MonoBehaviour
{
	public PersonStore store = new PersonStore();

	public static Kernal instance;

	[ExecuteInEditMode]
	void Start()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Debug.LogWarning("Multiple copies of the kernal were detected.");
			Destroy(this);
		}

		transform.SetAsFirstSibling();
	}

	[ExecuteInEditMode]
	void OnValidate()
	{
		transform.SetAsFirstSibling();
	}

	void OnDestroy()
	{
		instance = null;
	}
}
