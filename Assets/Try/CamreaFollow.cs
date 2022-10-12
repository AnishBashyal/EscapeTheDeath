using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamreaFollow : MonoBehaviour {

    public Transform target;
    private float offsetX;
    private float offsetY;
    public float Smoothing = 2f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetAxis("Horizontal") > 0) {
            offsetX = 2;
        } else if((Input.GetAxis("Horizontal") < 0))
        {
            offsetX = -2;
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            offsetY = 2;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            offsetY = -2;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x+offsetX,target.position.y+offsetY,transform.position.z), Smoothing*Time.deltaTime);
	}
}
