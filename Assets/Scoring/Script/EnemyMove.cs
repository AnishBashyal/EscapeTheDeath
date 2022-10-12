using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    public Transform target;
    private float enemySpeed;

	// Use this for initialization
	void Start () {
        enemySpeed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localRotation = Quaternion.identity;
        transform.position = Vector3.Lerp(transform.position, target.position, enemySpeed*Time.deltaTime);
        if (target.position.x > transform.position.x){
            transform.localScale = new Vector3(1.5f, 1, 1);
        }else if (target.position.x < transform.position.x){
            transform.localScale = new Vector3(-1.5f, 1, 1);

        }


    }
}
