using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerBUllet : MonoBehaviour {
    private float speed = 20f;
    public GameObject particles;
    void Awake(){
    }
    // Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector2(speed*Time.deltaTime,0));
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Enemy"){
            Destroy(col.gameObject);
            Instantiate(particles, transform.position, Quaternion.identity);

        }
        if (col.gameObject.tag == "Land")
        {
            Destroy(gameObject);
            Instantiate(particles, transform.position, Quaternion.identity);
        }
    }
}
