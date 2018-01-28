using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kernal : MonoBehaviour
{
	public List<Person> focusedIndividuals = new List<Person>();
	public PersonStore store = new PersonStore();

	public static Kernal instance;

	public AudioSource ass;

    public AudioClip[] maleSounds;
    public AudioClip[] femaleSounds;

	public AudioClip happyNoise;
	public AudioClip sadNoise;

	public void ClearFocus()
	{
		focusedIndividuals.Clear();
	}

	public void PlayPositiveConversationNoise()
	{
		ass = GetComponent<AudioSource>();
		ass.PlayOneShot(happyNoise);
	}

	public void PlayNegativeConversationNoise()
	{
		ass = GetComponent<AudioSource>();
		ass.PlayOneShot(sadNoise);
	}

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

		ass = GetComponent<AudioSource>();
		
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
