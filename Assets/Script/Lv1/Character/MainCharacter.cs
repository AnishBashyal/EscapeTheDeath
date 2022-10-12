using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainCharacter : MonoBehaviour {
	public GameObject Smoke;
    public GameObject Blood;
    public GameObject Power;
    public GameObject GoldenBuzzer;
    public GameObject GoldenToken;
    public GameObject Hack;
    public AudioClip[] audios;
    public Animator anime;
    public static float powerFillamount;
    public static List<string> myList = new List<string>();
	[SerializeField]
	private float speed;
    public static bool cheatEnbale;
    private bool checkPointEnable;
    private int cheatCode;
	private float Horizontal;
    private float Vertical;
	private float JumpXaxis;
    //public float powerLevel;
    private bool dead;
	private Rigidbody2D rb;
    private SpriteRenderer tint;
	public bool Groundcheck;
	private float RotationTimelimit;
	private bool Needsrotation;
    private bool needsTint;
    public static float Life;
    private float maxHealth = 100f;
    private float TimeforColorChange;
    private Camera camForfollow;
    private CamShake shake;

    private bool buttonTriggered;
    private bool TokenTriggered;

    private bool WinCondition;
    public static bool gyroscope;
    Image healthBar;
    Image powerBar;

    void Awake() {
        camForfollow = FindObjectOfType<Camera>();
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CamShake>();
        healthBar = GameObject.FindGameObjectWithTag("Life").GetComponent<Image>();
        powerBar = GameObject.FindGameObjectWithTag("Power").GetComponent<Image>();
        tint = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
	// Use this for initialization
	void Start () {

        TimeforColorChange = 1f;
		Instantiate(Smoke,transform.position,Quaternion.identity);
		RotationTimelimit = 10f;
        Life = maxHealth;
       // powerBar.fillAmount = 0;
        transform.position = CheckPoint.LastCheckPointPos;
        powerBar.fillAmount = powerFillamount;
   	}
	
	// Update is called once per frame
	void Update (){
		Movement ();
		RotationZ();
        Tinter();
        Death();
        Cheat();


        if (WinCondition)
        {
            StartCoroutine(Win());
        }
        if (powerBar.fillAmount >= 1 && !buttonTriggered){
            
            Instantiate(GoldenBuzzer, new Vector3(936, 1.45f), Quaternion.identity);
            buttonTriggered=true;
        }

    }
 
    private void Death(){
        if (Life <= 0 || transform.position.y < -5){
            powerFillamount = powerBar.fillAmount;
            AudioSource.PlayClipAtPoint(audios[2], transform.position);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
           // StartCoroutine(Win());
        }
    }

    IEnumerator Win(){
        yield return new WaitForSeconds(2f);
        anime.SetTrigger("Fading");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Evolution1");
    }
    private void Cheat()
    {
      /*  if (Input.GetKeyDown(KeyCode.C) && !cheatEnbale){
            cheatCode += 27;
        }
        if (Input.GetKeyDown(KeyCode.H) && !cheatEnbale){
            cheatCode += 99;
        }
        if (Input.GetKeyDown(KeyCode.E) && !cheatEnbale){
            cheatCode += 201;
        }
        if (Input.GetKeyDown(KeyCode.A) && !cheatEnbale){
            cheatCode += 133;
        }
        if (Input.GetKeyDown(KeyCode.T) && !cheatEnbale){
            cheatCode += 77;
        }

        if (Input.GetKeyDown(KeyCode.C) && cheatEnbale){
            cheatCode -= 27;
        }
        if (Input.GetKeyDown(KeyCode.H) && cheatEnbale){
            cheatCode -= 99;
        }
        if (Input.GetKeyDown(KeyCode.E) && cheatEnbale){
            cheatCode -= 201;
        }
        if (Input.GetKeyDown(KeyCode.A) && cheatEnbale){
            cheatCode -= 133;
        }
        if (Input.GetKeyDown(KeyCode.T) && cheatEnbale){
            cheatCode -= 77;
        }
        if (cheatCode == 537){
            //print("cheatenbaled");
           
            cheatEnbale = true;
        }
        else if (cheatCode == 0){
            //print("cheatDisabled");
           // Hack.SetActive(false);
            cheatEnbale = false;
        }*/

        if (cheatEnbale) {
            //Vertical = Input.GetAxis("Vertical");            
            rb.velocity = new Vector2(Horizontal*10, rb.velocity.y);
            powerBar.fillAmount = 1;
            Hack.SetActive(true);
            Hack.transform.position = new Vector3(camForfollow.transform.position.x, camForfollow.transform.position.y, 0);
            Life = 1000;

            if (Life <= 10) {
            Life = 100;
             }
            if (transform.position.y < -3f) {
            transform.position = new Vector3(transform.position.x, 8);
             }
         }
        if (!cheatEnbale)
        {
            Hack.SetActive(false);
        }
      }

    private void Movement (){

        if (Application.isMobilePlatform) {
            if (gyroscope) {
                MoveOnGyroscope();
            } else if (!gyroscope)
            {
                MoveonAndriod();
            }
        }
        if(!Application.isMobilePlatform){
            MoveonPc();   
        }

        rb.velocity = new Vector2(Horizontal * speed,rb.velocity.y);
	}

	private void RotationZ (){
		if (Horizontal == 0 && Needsrotation == false && Groundcheck) {
			RotationTimelimit = RotationTimelimit - Time.deltaTime;
		} 
		if (Horizontal > 0 || Horizontal < 0) {
			RotationTimelimit = 5f;
		}
		if (RotationTimelimit < 0) {
			Needsrotation = true;
			RotationTimelimit = 5f;
		}

		if (Needsrotation == true) {
			transform.Rotate (0, 0, -3);
			if (transform.eulerAngles.z > 358 || transform.eulerAngles.z < 2) {
				Needsrotation = false;
			}
		}
	}

    private void Evolution(){
        powerBar.fillAmount += 0.05f;
        AudioSource.PlayClipAtPoint(audios[1], transform.position);
        Instantiate(Power, transform.position, Quaternion.identity);
    }

    private void Hit() {
        AudioSource.PlayClipAtPoint(audios[3], transform.position);
        shake.camShake();
        Instantiate(Blood, transform.position, Quaternion.identity);
        needsTint = true;
        rb.velocity = new Vector2(rb.velocity.x, 10);
        Life -= 10;
        healthBar.fillAmount = Life / maxHealth;       
    }

    private void HitforBoulder(){
        shake.camShake();
        Instantiate(Blood, transform.position, Quaternion.identity);
        needsTint = true;
        Life -= 10;
        healthBar.fillAmount = Life / maxHealth;
        AudioSource.PlayClipAtPoint(audios[3], transform.position);
    }
    private void Tinter() {
        if (needsTint == true) {
            float randomColor = Random.Range(0f, 1f);
            tint.color = new Color(randomColor, .3f, .3f, randomColor);
            TimeforColorChange -= Time.deltaTime;
        }
        if (TimeforColorChange <= 0) {
            tint.color = new Color(1,1,1,1);
            TimeforColorChange = 1f;
            needsTint = false;
        }
    }

    private void MoveOnGyroscope()
    {
        Vector2 Acceleration = Input.acceleration;
        if (Acceleration.x > 0.05)
        {
            Horizontal = 0.7f;
        }
        else if (Acceleration.x < -0.05)
        {
            Horizontal = -0.4f;
        }
        else
        {
            Horizontal = 0;
        }

        if (Acceleration.y > -0.5 && Groundcheck){
            Jump();
        }
    }

    private void Jump()
    {
        AudioSource.PlayClipAtPoint(audios[4], transform.position);
        rb.velocity = new Vector2(Horizontal*speed, 10);
    }
    private void MoveonAndriod(){
        if (Input.touchCount == 1)
        {
             if (Input.GetTouch(0).position.x > Screen.width / 2 && Input.GetTouch(0).position.y < Screen.height / 2)
            {
                print("Right");
                if (Horizontal < 1.0f){
                    Horizontal += 0.1f;
                }

                Horizontal = 1;
            }
             if (Input.GetTouch(0).position.x < Screen.width / 2 && Input.GetTouch(0).position.y < Screen.height / 2)
            {
                print("Left");
                if (Horizontal > -1.0f){
                    Horizontal -= 0.1f;
                }
            }

             if (Input.GetTouch(0).position.y > Screen.height / 2){
                print("Up");
                if (Groundcheck){
                    Jump();
                }
            }
        }
        if (Input.touchCount > 1){
             if ((Input.GetTouch(0).position.x < Screen.width / 2 && Input.GetTouch(1).position.y > Screen.height / 2)|| (Input.GetTouch(1).position.x < Screen.width / 2 && Input.GetTouch(0).position.y > Screen.height / 2))
            {

                if (Horizontal > -1.0f){
                    Horizontal -= 0.1f;
                }
                if (Groundcheck) {
                    Jump();
                }

            }
            if ((Input.GetTouch(0).position.x > Screen.width / 2 && Input.GetTouch(1).position.y > Screen.height / 2) || (Input.GetTouch(1).position.x > Screen.width / 2 && Input.GetTouch(0).position.y > Screen.height / 2))
            {
                if (Horizontal < 1.0f){
                    Horizontal += 0.1f; 
                }
                if (Groundcheck){
                    Jump();
                }
            }
        }
        if (Input.touchCount==0){
            Horizontal = 0;
            print("not moving");
        }
    }
    private  void MoveonPc(){
        Horizontal = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && Groundcheck)
        {
            Jump();
        }
    }

    void OnCollisionEnter2D (Collision2D col){
        if (col.gameObject.CompareTag("Land"))
        {
            Groundcheck = true;
        }
        if (col.gameObject.CompareTag("Obstacles")){
            Hit();
        }

        if (col.gameObject.CompareTag("Boulder")){
            HitforBoulder();
        }


        if (col.gameObject.CompareTag("GoldenBuzzer") && !TokenTriggered){
           
            print("trigger token");
            
            Instantiate(GoldenToken, new Vector3(984, 3f), Quaternion.identity);
            TokenTriggered = true;
           AudioSource.PlayClipAtPoint(audios[1],transform.position);
            
        }

    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == "Token")
        {
            Destroy(trigger.gameObject);
            myList.Add(trigger.gameObject.name);
            Evolution();
        }

        if (trigger.tag == "GoldenToken"){
            //WOn the Game
            WinCondition = true;
            Instantiate(Power, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(audios[0], transform.position);
            Destroy(trigger.gameObject);
        }
    }

    void OnCollisionStay2D (Collision2D col){
        Groundcheck = true;
        if (col.gameObject.CompareTag("Boulder"))
        {
            HitforBoulder();
        }
    }

    void OnCollisionExit2D (Collision2D col){
        Groundcheck = false;
    }
}
