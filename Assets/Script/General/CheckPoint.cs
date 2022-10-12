using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour {
   // public bool isTurnOfScene2;
    private static CheckPoint instance;
    public static  Vector2 LastCheckPointPos;
    public List<string> toDestroy = new List<string>();
   // public Image blueBar;
 
	// Use this for initialization
	void Awake () {

            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance);
            }
            else
            {
                Destroy(gameObject);
            }
        
    
    }
	
    void Start() {
        //  blueBar.fillAmount = MainCharacter.powerFillamount;
       
     
        
            LastCheckPointPos = new Vector2(0, 5);
            toDestroy = MainCharacter.myList;
        
      }
    // Update is called once per frame
    void Update()
    {
        foreach (string target in toDestroy)
        {
            GameObject destroy = GameObject.Find(target);
            if (destroy){
                Destroy(destroy.gameObject);
            }
        }
    }
}
