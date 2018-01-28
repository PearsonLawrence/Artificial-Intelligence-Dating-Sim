using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class WorldSpaceTracker : MonoBehaviour
{
	RectTransform transform2D;
	public Transform worldObject;

	void Start()
	{
		transform2D = GetComponent<RectTransform>();
		//Debug.Log(transform2D.anchoredPosition);
	}

	void LateUpdate()
	{
		//Debug.Log(Camera.main.WorldToScreenPoint(worldObject.position));
		transform2D.anchoredPosition = Camera.main.WorldToScreenPoint(worldObject.position);
	}
}