using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Lv2_MainCharacter : MonoBehaviour
{

    public Sprite[] movementSprites;
    public AudioClip[] audios;
    public Animator anim;
    private Rigidbody2D rb;
    private float TimeforColorChange;
    private bool needsTint;
    private SpriteRenderer tint;
    float Horizontal;
    float Vertical;
    private float Xspeed;
    private float Yspeed;
    float speed = 8;

    private int cheatCode;
    public static bool cheatEnbale;

    public Animator anime;

    public static List<string> myList = new List<string>();
    public GameObject powerParticle;
    public GameObject blood;
    public GameObject Hack;
    public GameObject GoldenToken;

    public static bool gyroscope2;


    public static float powerFill;
    private bool tokenTriggered;
    private bool WinCondition;
    Image healthBar;
    Image staminaBar;
    Image powerBar;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        staminaBar = GameObject.FindGameObjectWithTag("Stamina").GetComponent<Image>();
        healthBar = GameObject.FindGameObjectWithTag("Life").GetComponent<Image>();
        powerBar = GameObject.FindGameObjectWithTag("Power").GetComponent<Image>();
        tint = GetComponent<SpriteRenderer>();


    }
    // Use this for initialization
    void Start(){
        transform.position = CheckPoint2.LastCheckPointPos;
        powerBar.fillAmount = powerFill;
        TimeforColorChange = 1f;
    }

    // Update is called once per frame

    void Update(){

        if (WinCondition)
        {
            StartCoroutine(Win());
        }
        if (powerBar.fillAmount >= 1 && !tokenTriggered){
            Instantiate(GoldenToken,new Vector2(1454,0), Quaternion.identity);
            tokenTriggered=true;
        }
        transform.rotation = Quaternion.identity;
        Tinter();
        Cheat();
    }
    void FixedUpdate()
    {
        Movement();
    }
    


    IEnumerator Win(){
        yield return new WaitForSeconds(2f);
        anime.SetTrigger("Fading");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Evolution2");
    }

    void MoveOnPc(){
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        if (staminaBar.fillAmount <= 0)
        {
            rb.gravityScale = 1;
            rb.velocity = new Vector2(Horizontal * speed, rb.velocity.y);
        }
        if (staminaBar.fillAmount > 0)
        {
            rb.velocity = new Vector2(Horizontal * speed, Vertical * speed);
            rb.gravityScale = 0;

        }

        if (Horizontal > 0)
        {
            GetComponent<SpriteRenderer>().sprite = movementSprites[3];
        }
        else if (Horizontal < 0)
        {
            GetComponent<SpriteRenderer>().sprite = movementSprites[2];
        }
        if (Vertical < 0)
        {
            GetComponent<SpriteRenderer>().sprite = movementSprites[0];
        }
        else if (Vertical > 0)
        {
            GetComponent<SpriteRenderer>().sprite = movementSprites[1];
            staminaBar.fillAmount -= Time.deltaTime / 2;
        }
        if (Horizontal == 0 && Vertical == 0)
        {
            GetComponent<SpriteRenderer>().sprite = movementSprites[0];
        }

        if (staminaBar.fillAmount <= 0)
        {
            Invoke("Stamina", 2);
        }

        if ((Vertical <= 0) && (staminaBar.fillAmount < 1f && staminaBar.fillAmount > 0))
        {
            Invoke("Stamina", 1);
        }


        float YPos = Mathf.Clamp(transform.position.y, -9, 9);
        transform.position = new Vector2(transform.position.x, YPos); 
    }

    void MoveOnAndriod()
    {
            if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;
            if (touch.position.x > Screen.width / 2)
            {
                GetComponent<SpriteRenderer>().sprite = movementSprites[3];
                print("right");
               
            } else if (touch.position.x < Screen.width / 2)
            {
                print("left");
                GetComponent<SpriteRenderer>().sprite = movementSprites[2];
            }
            if (touch.position.y > Screen.height/2){
                staminaBar.fillAmount -= Time.deltaTime;
            }

            if (staminaBar.fillAmount <= 0)
            {
                Invoke("Stamina", 2);
            }

            if ((touch.position.y<Screen.height/2) && (staminaBar.fillAmount < 1f && staminaBar.fillAmount > 0)){
                Invoke("Stamina", 1);
            }
            transform.position = Vector3.Lerp(transform.position, touchPosition, 2 * Time.deltaTime);
        }
        if (Input.touchCount == 0){
            GetComponent<SpriteRenderer>().sprite = movementSprites[0];
        }
        if (staminaBar.fillAmount <= 0)
        {
            rb.gravityScale = 1;
        }
        if (staminaBar.fillAmount > 0)
        {
            rb.gravityScale = 0;
        }
    }
    void Movement(){
        if (!Application.isMobilePlatform){
            MoveOnPc();
        }
        if (Application.isMobilePlatform){
            if (!gyroscope2){
                MoveOnAndriod();
            }
            if (gyroscope2){
                MoveOnGyroscope();
            }
        }
    }

    void MoveOnGyroscope(){
        Horizontal = Input.acceleration.x;
        Vertical = Input.acceleration.y;

        Xspeed = Horizontal;
        if (staminaBar.fillAmount <= 0)
        {
            rb.velocity = new Vector2(Horizontal * speed * 4, rb.velocity.y);
            rb.gravityScale = 1;
            
        }
        if (staminaBar.fillAmount > 0)
        {
            rb.velocity = new Vector2(Horizontal * speed * 4, Yspeed * speed / 1f);
            rb.gravityScale = 0;

        }

        if (Xspeed > 0)
        {
            GetComponent<SpriteRenderer>().sprite = movementSprites[3];
        }
        else if (Xspeed < 0)
        {
            GetComponent<SpriteRenderer>().sprite = movementSprites[2];
        }

        if (Vertical > -.5)
        {
            print("Up");
            Yspeed = .3f;
            GetComponent<SpriteRenderer>().sprite = movementSprites[1];
            staminaBar.fillAmount -= Time.deltaTime / 2;
        }
        else if (Vertical < -.8)
        {
            print("Down");
            GetComponent<SpriteRenderer>().sprite = movementSprites[0];
            Yspeed = -.3f;
        }
        else
        {
            Yspeed = 0;
        }
        if (Xspeed == 0 && Yspeed == 0){
            GetComponent<SpriteRenderer>().sprite = movementSprites[0];
        }

        if (staminaBar.fillAmount <= 0){
            Invoke("Stamina", 2);
        }

        if ((Yspeed <= 0) && (staminaBar.fillAmount < 1f && staminaBar.fillAmount > 0))
        {
            Invoke("Stamina", 1);
        }

         
     
    }

    void Death(){
        powerFill = powerBar.fillAmount;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioSource.PlayClipAtPoint(audios[1], transform.position);
    }

    void Stamina()
    {
       // print("Adding");
        if (staminaBar.fillAmount < 1)
        {
            staminaBar.fillAmount += Time.deltaTime;
        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Obstacles")){
            //   print("Hit Obstacle");
            healthBar.fillAmount -= .05f;
            AudioSource.PlayClipAtPoint(audios[2], transform.position);
            Hit();
            
        }
        if (col.gameObject.CompareTag("Boulder"))
        {
            //   print("Hit Obstacle");
            healthBar.fillAmount -= .05f;
            AudioSource.PlayClipAtPoint(audios[2], transform.position);

            Hit();

        }
    }
    void OnCollisionStay2D(Collision2D col){
        if (col.gameObject.CompareTag("Obstacles"))
        {
           // print("Hit Obstacle");
            healthBar.fillAmount -= Time.deltaTime/2;
            Hit();
         }
        if (col.gameObject.CompareTag("Boulder"))
        {
          //  print("Hit Rock");
            healthBar.fillAmount -= Time.deltaTime / 2;
            Hit();
        }
    }

    void OnTriggerEnter2D(Collider2D trigger){
        if (trigger.tag == "Token"){
            Destroy(trigger.gameObject);
            myList.Add(trigger.gameObject.name);
            Evolution();
        }

        if (trigger.tag == "GoldenToken")
        {
           //WOn the Game
            WinCondition = true;
            Instantiate(powerParticle, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(audios[0], transform.position);
            Destroy(trigger.gameObject);}
      }

    private void Hit(){
        Instantiate(blood, transform.position, Quaternion.identity);
        anim.SetTrigger("shake");
        needsTint = true;
        if (healthBar.fillAmount <= 0)
        {
            Death();
        }
    }


    private void Evolution(){
        powerBar.fillAmount += 0.05f;
        Instantiate(powerParticle, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(audios[3], transform.position);
    }
    private void Tinter()
    {
        if (needsTint == true)
        {
            float randomColor = Random.Range(0f, 1f);
            tint.color = new Color(randomColor, .3f, .3f, randomColor);
            TimeforColorChange -= Time.deltaTime;
        }
        if (TimeforColorChange <= 0)
        {
            tint.color = new Color(1, 1, 1, 1);
            TimeforColorChange = 1f;
            needsTint = false;
        }
    }

    private void Cheat(){
      /*  if (Input.GetKeyDown(KeyCode.C) && !cheatEnbale)
        {
            cheatCode += 27;
        }
        if (Input.GetKeyDown(KeyCode.H) && !cheatEnbale)
        {
            cheatCode += 99;
        }
        if (Input.GetKeyDown(KeyCode.E) && !cheatEnbale)
        {
            cheatCode += 201;
        }
        if (Input.GetKeyDown(KeyCode.A) && !cheatEnbale)
        {
            cheatCode += 133;
        }
        if (Input.GetKeyDown(KeyCode.T) && !cheatEnbale)
        {
            cheatCode += 77;
        }

        if (Input.GetKeyDown(KeyCode.C) && cheatEnbale)
        {
            cheatCode -= 27;
        }
        if (Input.GetKeyDown(KeyCode.H) && cheatEnbale)
        {
            cheatCode -= 99;
        }
        if (Input.GetKeyDown(KeyCode.E) && cheatEnbale)
        {
            cheatCode -= 201;
        }
        if (Input.GetKeyDown(KeyCode.A) && cheatEnbale)
        {
            cheatCode -= 133;
        }
        if (Input.GetKeyDown(KeyCode.T) && cheatEnbale)
        {
            cheatCode -= 77;
        }
        if (cheatCode == 537)
        {
            //print("cheatenbaled");

            cheatEnbale = true;
        }
        else if (cheatCode == 0)
        {
            //print("cheatDisabled");
         //   Hack.SetActive(false);
            cheatEnbale = false;
           // speed = 8;
        }*/

        if (cheatEnbale)
        {
            speed = 20;
            staminaBar.fillAmount = 1;
            powerBar.fillAmount = 1;
            Hack.SetActive(true);
            
            if (healthBar.fillAmount < 0.2f){
                healthBar.fillAmount = 1;
            }
            
        }
        if (!cheatEnbale)
        {
            speed = 8;
            Hack.SetActive(false);
        }
    }
}
