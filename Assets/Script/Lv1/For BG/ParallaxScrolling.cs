using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParallaxScrolling : MonoBehaviour {

	public Transform[] backgrounds;
	private Vector3 previousCamPos;
	private float[] parallaxScales;
	public float paralllaxSmoothing;
	private Transform cam;

	// Use this for initialization

	void Awake (){
		cam = Camera.main.transform;
	}
	void Start ()
	{
		previousCamPos = cam.position;
		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales[i]=backgrounds[i].position.z;
        }
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < backgrounds.Length; i++) {
			float parallaxForX = (previousCamPos.x-cam.position.x)*parallaxScales[i];
            float parallaxForY = (previousCamPos.y - cam.position.y) * parallaxScales[i];
			float backgroundTargetPosX = backgrounds[i].position.x + parallaxForX;
            float backgroundTargetPosY = backgrounds[i].position.y + parallaxForY *-1;
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX,backgroundTargetPosY,backgrounds[i].position.z);
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position,backgroundTargetPos,paralllaxSmoothing);

		}
	
		previousCamPos = cam.position;

	}
}
