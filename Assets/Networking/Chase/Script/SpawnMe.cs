using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMe : MonoBehaviour
{
    public GameObject enemies;
    private float Posx;
    private float Posy;
    private float Timer;
    private float Vertical;
    private Vector2 randomPos;
    float Reset;

    // Use this for initialization
    void Start () {
        Timer = 10f;
        Reset = 3f;
	}

    // Update is called once per frame

    void Update(){
        
       enemySpawn();
        
    }
   
    void enemySpawn()
    {
        // print("wow");
        print(Time.timeSinceLevelLoad);
        Posx = Random.Range(-6, 6);
        Posy = Random.Range(-4, 4);
        randomPos = new Vector3(Posx, Posy);
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        if (Timer <= 0){

            Instantiate(enemies, randomPos, Quaternion.identity);
            Timer = Reset;
        }

        if (Time.timeSinceLevelLoad > 60f)
        {
            Reset = 2;
        }
        if (Time.timeSinceLevelLoad > 120f)
        {
            Reset = 1;
        }
        if (Time.timeSinceLevelLoad > 320f)
        {
            Reset = .5f;
        }

    }
}
