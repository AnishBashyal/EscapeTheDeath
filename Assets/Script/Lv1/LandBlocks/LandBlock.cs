using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandBlock : MonoBehaviour{
    private float Timer;
    public bool goRight;

    // Use this for initialization
    void Start(){
        if (goRight == true) {
            Timer = 0;
        } else if(goRight == false){
            Timer = 150;
        }
    }

    // Update is called once per frame
    void Update()    {

        if (goRight)  {
            transform.Translate(Vector3.right * Time.deltaTime * 2);
            Timer += 1;
        } 
        if (Timer ==150){
            goRight = false;
        } else if (Timer == 0){
            goRight = true;
        }
        if (!goRight){
            transform.Translate(Vector3.left*Time.deltaTime*2);
            Timer -= 1;
        }    
    }
}
