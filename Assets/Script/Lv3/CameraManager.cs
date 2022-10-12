using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public Transform target;
    public float offset;
    public float speed;
    Vector3 Position; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offset, target.position.y, transform.position.z), speed*Time.deltaTime);
        Position.x = Mathf.Clamp(Position.x, -69, 69);
        Position.y=Mathf.Clamp(Position.y, -25, 75);
        transform.position = Position;
	}
}
