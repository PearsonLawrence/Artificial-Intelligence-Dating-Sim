using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConversationPopup : MonoBehaviour {

    public Image[] Emotions;
    public int Feeling;
    public GameObject One, Two;
	// Use this for initialization
	void Start () {

       

	}
	
	// Update is called once per frame
	void Update () {
		transform.position =( One.transform.position + Two.transform.position) / 2;
        for (int i = 0; i < Emotions.Length; i++)
        {
            if (Feeling != i)
            {
                Emotions[i].gameObject.SetActive(false);
            }
            else
            {
                Emotions[i].gameObject.SetActive(true);
            }
        }
    }
}
