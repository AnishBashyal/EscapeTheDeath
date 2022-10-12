using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderTrigger : MonoBehaviour {

    public Sprite[] buttonSprite;
    public GameObject boulder;
    public AudioClip buttonpress;
    
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Player")) {
            this.GetComponent<SpriteRenderer>().sprite = buttonSprite[1];
            boulder.GetComponent<Rigidbody2D>().gravityScale = 1;
            AudioSource.PlayClipAtPoint(buttonpress, transform.position);
        }

    }
}
