using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {

    public GameObject rock;
    public Transform target;

    private float Posx;
    private float Posy;
    private float Timer;
    private float Reset;
    private Vector2 randomPos;

    
	// Use this for initialization
	void Start () {
        Timer = 1f;
        Reset = 1f;
    }
	
	// Update is called once per frame
	void Update () {
       // print(Timer);
        Posx = Random.Range(target.position.x + 10, target.position.x + 50);
        Posy = Random.Range(-10, 10);
        randomPos = new Vector3(Posx, Posy);

      
        if (Timer > 0){
            Timer -= Time.deltaTime;
        }
        if (Timer <= 0){

            Instantiate(rock, randomPos, Quaternion.identity);
            Timer = Reset;
        }
    }
}
