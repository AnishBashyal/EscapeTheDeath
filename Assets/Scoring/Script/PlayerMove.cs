using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerMove : MonoBehaviour {

    private float Horizontal;
    private float Vertical;
    public float speed;
    private Rigidbody2D rb;
    public Sprite[] movementSprites;
    public GameObject blood;
    public Text text;
    public Text highScoreText;
    private float Highscore;
    private float Score;

    public AudioClip hitsound;

    private bool gyroForScore;
    Image healhBar;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        healhBar = GameObject.FindGameObjectWithTag("Life").GetComponent<Image>();
    }
    
    // Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        gyroForScore = MainCharacter.gyroscope;
        Movement();
        ScoreSystem();
        print(Highscore);
        highScoreText.text = "HIGHSCORE :" + Highscore;
    }

    void Movement(){
        transform.localRotation = Quaternion.identity;
        if (!Application.isMobilePlatform){
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(Horizontal, Vertical) * speed;

            if (Horizontal > 0)
            {
                GetComponent<SpriteRenderer>().sprite = movementSprites[3];
            }
            else if (Horizontal < 0)
            {
                GetComponent<SpriteRenderer>().sprite = movementSprites[2];
            }

            if (Vertical > 0)
            {
                GetComponent<SpriteRenderer>().sprite = movementSprites[1];
            }
            else if (Vertical < 0)
            {
                GetComponent<SpriteRenderer>().sprite = movementSprites[0];
            }
            if (Horizontal == 0 && Vertical == 0)
            {
                GetComponent<SpriteRenderer>().sprite = movementSprites[0];

            }
        }
        if (Application.isMobilePlatform)
        {

         
            Horizontal = Input.acceleration.x;
            Vertical = Input.acceleration.y;

            float Yspeed;
            float Xspeed = Horizontal;

            if (Xspeed > 0){
                GetComponent<SpriteRenderer>().sprite = movementSprites[3];
            }
            else if (Xspeed < 0){
                GetComponent<SpriteRenderer>().sprite = movementSprites[2];
            }

            if (Vertical > -.5){
//                print("Up");
                Yspeed = 1f;
                GetComponent<SpriteRenderer>().sprite = movementSprites[1];
            }
            else if (Vertical < -.8){
  //              print("Down");
                GetComponent<SpriteRenderer>().sprite = movementSprites[0];
                Yspeed = -1f;
            }
            else
            {
                Yspeed = 0;
            }
            if (Xspeed == 0 && Yspeed == 0)
            {
                GetComponent<SpriteRenderer>().sprite = movementSprites[0];
            }
           rb.velocity = new Vector2(Xspeed * speed * 2, Yspeed * speed);
        }
        ScreenWrap();
        LifeCheck();
    }


    void ScreenWrap(){
        if (transform.position.x > 9.5f){
            //   print("too much right");
            transform.position = new Vector3(-9f, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -10.1f)
        {
            // print("too much left");
            transform.position = new Vector3(9f, transform.position.y, transform.position.z);

        }
        if (transform.position.y > 6f)
        {
            //print("too much up");
            transform.position = new Vector3(transform.position.x, -5.5f, transform.position.z);

        }
        if (transform.position.y < -6f)
        {
            //print("too much down");
            transform.position = new Vector3(transform.position.x, 5.5f, transform.position.z);

        }
    }
    void ScoreSystem(){

        Score = (Mathf.RoundToInt(Time.timeSinceLevelLoad * 10));
        speed = speed + Time.deltaTime*.5f;
        //print(Score);
        text.text = "Score: " + Score;        
        print (PlayerPrefs.GetFloat("Highscore"));
        Highscore = (PlayerPrefs.GetFloat("Highscore"));

    }

    void OnCollisionEnter2D(Collision2D col){
        Instantiate(blood, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(hitsound, transform.position);
        healhBar.fillAmount -= 0.05f;
    }

    void OnCollisionStay2D(Collision2D col){
        healhBar.fillAmount -= Time.deltaTime;
    }

    void LifeCheck(){
        if (healhBar.fillAmount <= 0f){
            if (Score > PlayerPrefs.GetFloat("Highscore")){
                PlayerPrefs.SetFloat("Highscore", Score);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
