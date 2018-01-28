using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDSwitcher : MonoBehaviour
{
	public GameObject[] targets;

	[SerializeField]
	private int _activeIndex;
	public int activeIndex {get {return _activeIndex;}}

	public void SetActiveIndex(int newActiveIndex)
	{

		for(int i = 0; i < targets.Length; ++i)
		{
			targets[(newActiveIndex + i) % targets.Length].SetActive(false);
		}
		targets[newActiveIndex % targets.Length].SetActive(true);
	}

	public void OnValidate()
	{
		_activeIndex = Mathf.Max(0, _activeIndex);
		_activeIndex %= targets.Length;
		SetActiveIndex(_activeIndex);
	}
}
