using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_2 : MonoBehaviour {
    public Transform target;
    private float offsetX;
    private float offsetY;
    public float Smoothing;
    
    // Use this for initialization
    void Start () {

        transform.position =new Vector3(CheckPoint2.LastCheckPointPos.x,CheckPoint2.LastCheckPointPos.y,transform.position.z);
    }

    void Update()
    {
        float PosY = Mathf.Clamp(transform.position.y, -5, 5);
        float PosX = Mathf.Clamp(transform.position.x, 0, Mathf.Infinity);
        transform.position = new Vector3(PosX, PosY, transform.position.z);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetAxis("Horizontal") > 0)
        {
            offsetX = 10;
        }
        else if ((Input.GetAxis("Horizontal") < 0))
        {
            offsetX = -10;
        }
         if (Input.GetAxis("Vertical") > 0)
        {
            offsetY = 5;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            offsetY = -5;
           
        }
         if(Input.GetAxis("Horizontal")==0){
            offsetX = 0;
        }  if (Input.GetAxis("Vertical") == 0)
        {
            offsetY = 0;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offsetX, target.position.y + offsetY, transform.position.z), Smoothing * Time.deltaTime);
    }
}
