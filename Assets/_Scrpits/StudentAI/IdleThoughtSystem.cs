using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IdleThoughtSystem : MonoBehaviour {
    public GameObject IdleThoughtBubble;

    public Image ThoughtPositive, ThoughtNevative, ThoughtSad, ThoughtLove;

    public bool IdleGrow;

	// Use this for initialization
	void Start () {
		
	}
    float ScaleGrow;
	// Update is called once per frame
	void Update () {

		if(IdleGrow)
        {
            ScaleGrow += Time.deltaTime;

            ScaleGrow = Mathf.Clamp(ScaleGrow, 0, 1);

            IdleThoughtBubble.transform.localScale = new Vector3(ScaleGrow, ScaleGrow, ScaleGrow);
        }
        else
        {
            ScaleGrow -= Time.deltaTime;

            ScaleGrow = Mathf.Clamp(ScaleGrow, 0, 1);

            IdleThoughtBubble.transform.localScale -= new Vector3(ScaleGrow, ScaleGrow, ScaleGrow);
        }
    }
}
