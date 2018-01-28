using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    

    Rigidbody RB;
    public float speed;
    public float xMin, xMax, yMin, yMax;
	// Use this for initialization
	void Start () {
        RB = GetComponent<Rigidbody>();

        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    public Vector3 input;

    void Update()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

	// Update is called once per frame
	void LateUpdate () {

        RB.velocity = input * speed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y, Mathf.Clamp(transform.position.z, yMin, yMax));
	}
}
