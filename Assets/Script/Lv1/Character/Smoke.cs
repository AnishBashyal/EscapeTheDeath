using System.Collections;
using System.Collections.Generic;
using UnityEngine;	


public class Smoke : MonoBehaviour {

	private MainCharacter main;
	// Use this for initialization
	void Start () {
		main = FindObjectOfType<MainCharacter>();

	}
	
	// Update is called once per frame
	void Update () {
		 transform.position = main.transform.position;
	}
}
