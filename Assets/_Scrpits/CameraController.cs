using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Rigidbody RB;
    public float speed;

	// Use this for initialization
	void Start () {
        RB = GetComponent<Rigidbody>();
	}

    public Vector3 input;

    void Update()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

	// Update is called once per frame
	void LateUpdate () {

        RB.velocity = input * speed;

	}
}
