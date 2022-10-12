using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour {

	public Transform player;
	public float offsetSmoothing;


	private float offSetX=5;
	private float offSetY=1;
	private MainCharacter mainCharacter;
	private Vector3 playerPos;
	// Use this for initialization
	void Awake(){
		mainCharacter = FindObjectOfType<MainCharacter>();
	}
	void Start () {
        transform.position = new Vector3(mainCharacter.transform.position.x, transform.position.y);
	}

	
	// Update is called once per frame
	void FixedUpdate ()
	{	

		playerPos = new Vector3 (player.position.x, player.position.y, transform.position.z);
		if (mainCharacter.Groundcheck == false) {
			playerPos = new Vector3 (playerPos.x, playerPos.y + offSetY, playerPos.z);
		
		}

		if (Input.GetAxis ("Horizontal") > 0) { 
			playerPos = new Vector3 (playerPos.x + offSetX, playerPos.y, playerPos.z);
		}

		if (Input.GetAxis ("Horizontal") < 0) {
			playerPos = new Vector3 (playerPos.x - offSetX, playerPos.y, playerPos.z);
		}
	
		transform.position = Vector3.Lerp(transform.position,playerPos,offsetSmoothing*Time.deltaTime);
		float PosX = Mathf.Clamp(transform.position.x,-18.5f,Mathf.Infinity);
        float PosY = Mathf.Clamp(transform.position.y,5.2f,Mathf.Infinity);
      //  float PosY = Mathf.Clamp(transform.position.y, 3.5f, Mathf.Infinity);
        transform.position = new Vector3 (PosX,PosY,transform.position.z);
	}
}
