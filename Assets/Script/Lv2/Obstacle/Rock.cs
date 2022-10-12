using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

    private float randomSpeed;
    public GameObject blood;
	// Use this for initialization
	void Start () {
        randomSpeed = Random.Range(-20, -1);
    }
	
	// Update is called once per frame
	void Update () {
        
  
        GetComponent<Rigidbody2D>().velocity = new Vector3(randomSpeed, 0, 0);
        //print(randomSpeed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
          //  print("hit player");
        }
        else{         
           Destroy(gameObject);
            Instantiate(blood, transform.position, Quaternion.identity);
        }
    }
}
