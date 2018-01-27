using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopularityBubble : MonoBehaviour {

    public StudentAI Owner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        StudentAI Temp = other.GetComponent<StudentAI>();
        if (Temp != null && Temp != Owner && Temp.ChatCooldown <= 0)
        {
            Owner.SocialTarget = Temp;
            Temp.SocialTarget = Owner;
            Owner.ChatEngaged = true;
            Temp.ChatEngaged = true;

            Debug.Log("dido");
        }
    }

}
