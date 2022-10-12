using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {

   // public bool isTurnOfScene2;
    public GameObject pannel;
    private int i=1;
    private bool gyroCheck;
   
    
	public  void LoadLevel(string name){
            MainCharacter.myList.Clear();
            MainCharacter.powerFillamount = 0;
            CheckPoint.LastCheckPointPos = new Vector2(0, 5);
            SceneManager.LoadScene(name);
            AudioListener.volume = 1;

        
            Lv2_MainCharacter.myList.Clear();
            Lv2_MainCharacter.powerFill = 0;
            CheckPoint2.LastCheckPointPos = new Vector2(0, 0);
            SceneManager.LoadScene(name);
            AudioListener.volume = 1;
        

    }

    public void Audio(Text text) {

        if (AudioListener.volume == 0){
            text.text = "Audio";
            AudioListener.volume = 1;
        } else if (AudioListener.volume == 1){
            text.text = "Muted";
            AudioListener.volume = 0;
        }
       
    }

    public  void Gyro(Text text){
        if(text.text == "Touch Enabled"){
            text.text = "Gyroscope Enabled";
            //MainCharacter.gyroscope = true;
            gyroCheck = true;
        }else if (text.text == "Gyroscope Enabled")
        {
            text.text = "Touch Enabled";
            gyroCheck = false;
            //MainCharacter.gyroscope = false;
        }


    }
    public void QuitLevel()
    {
        Application.Quit();
    }

    

    public void EnableMenu() {

        i *= -1;

    }
    private void Update() {
      
        MainCharacter.gyroscope = gyroCheck;
        Lv2_MainCharacter.gyroscope2 = gyroCheck;
        Lv3_Character.gyro3 = gyroCheck;
        if (i == 1)
        {
            if (pannel)
            {
                pannel.SetActive(false);
                Time.timeScale = 1;
            }
        }
        else if (i == -1)
        {
            if (pannel)
            {
                pannel.SetActive(true);
                Time.timeScale = 0;
            }
        }
     }

    public void skipLevel(string name){

        
            if (MainCharacter.cheatEnbale == true)
            {
                MainCharacter.myList.Clear();
                MainCharacter.powerFillamount = 0;
                CheckPoint.LastCheckPointPos = new Vector2(0, 5);
                SceneManager.LoadScene(name);
                AudioListener.volume = 1;
            }
        
            if (Lv2_MainCharacter.cheatEnbale == true)
            {
                Lv2_MainCharacter.myList.Clear();
                Lv2_MainCharacter.powerFill = 0;
                CheckPoint2.LastCheckPointPos = new Vector2(0, 5);
                SceneManager.LoadScene(name);
                AudioListener.volume = 1;
            
        }
    }

    public void Cheat(Text text){
        if (text.text == "LOL")
        {
           // print("enabled");
            MainCharacter.cheatEnbale = true;
            Lv2_MainCharacter.cheatEnbale = true;
            Lv3_Character.cheatEnbaleforLv3 = true;
           
        } else
        {
            //print("Disabled");
            MainCharacter.cheatEnbale = false;
            Lv2_MainCharacter.cheatEnbale = false;
            Lv3_Character.cheatEnbaleforLv3 = false;
        }
    }
}
