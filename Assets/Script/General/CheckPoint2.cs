using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint2 : MonoBehaviour {
    private static CheckPoint2 instance;
    public static Vector2 LastCheckPointPos;
    public List<string> toDestroy = new List<string>();
    // Use this for initialization
    void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start() { 
        LastCheckPointPos = new Vector2(0, 0);
        toDestroy = Lv2_MainCharacter.myList;
    }
    // Update is called once per frame
    void Update()
    {
        foreach (string target in toDestroy){
            GameObject destroy = GameObject.Find(target);
            if (destroy)
            {
                Destroy(destroy.gameObject);
            }
        }
    }
}
