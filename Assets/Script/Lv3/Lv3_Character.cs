using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lv3_Character : MonoBehaviour {

    private bool needsTint;
    private float TimeforColorChange = 1f;
    private float speed;
    private float timeforReload;
    private bool fired;
    private bool winCondition;
    private SpriteRenderer tint;
    public GameObject blood;
    private Rigidbody2D rb;
    public GameObject hack;
    Image healthBar;
    Image ammoBar;
    Image enemyHealth;
    float Horizontal, Vertical;
    public Animator camAnimator;
    public Animator anime;
    public GameObject bullet;
    public static bool cheatEnbaleforLv3;

    public AudioClip[] audios;
    public static bool gyro3;
    // Use this for initialization
    void Awake(){
        enemyHealth = GameObject.FindGameObjectWithTag("LifeofEnemy").GetComponent<Image>();
        tint = GetComponent<SpriteRenderer>();
        healthBar = GameObject.FindGameObjectWithTag("Life").GetComponent<Image>();
        ammoBar = GameObject.FindGameObjectWithTag("AmmoBar").GetComponent<Image>();

        rb = GetComponent<Rigidbody2D>();
    }
    void Start () {
        timeforReload = 5f;
        speed = 30f;
        fired = false;
    }

    // Update is called once per frame

    void Update(){
        Tinter();
        Shoot();
        Speed();
        Cheat();
        Death();
        if (enemyHealth.fillAmount <= 0)
        {
            winCondition = true;
        }
        if (winCondition){
            StartCoroutine(Win());
        }
        //print("Player" + speed);
    }

    IEnumerator Win(){
        yield return new WaitForSeconds(3f);
       // AudioSource.PlayClipAtPoint(audios[0], transform.position);
        anime.SetTrigger("Fading");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Evolution3");
    }

    void Cheat(){
        if (cheatEnbaleforLv3) {
            hack.SetActive(true);
            healthBar.fillAmount = 1;
            ammoBar.fillAmount = 1;
        }else if (!cheatEnbaleforLv3)
        {
            hack.SetActive(false);
        }
    }
    void Speed(){
        if (enemyHealth.fillAmount < .8f && healthBar.fillAmount >= .5f)
        {
            //05-08
            speed = 40f;
        }

        else if (enemyHealth.fillAmount < .5f && healthBar.fillAmount >= .2f)
        {
            //02-0.5
            speed = 50f;
        }
        else if (enemyHealth.fillAmount < .2f)
        {
            //02-0
            speed = 60f;
        } else if (enemyHealth.fillAmount <= 0f)
        {
            winCondition = true;
        }

    }
    void FixedUpdate () {
        Movement();
    }


    void Shoot(){
        //print(timeforReload);
        if (Input.GetKeyDown(KeyCode.Space)){
            if (ammoBar.fillAmount > 0f){
                Instantiate(bullet, transform.position, Quaternion.identity);
                ammoBar.fillAmount -= .1f;
                AudioSource.PlayClipAtPoint(audios[1], transform.position);
                bullet.transform.localScale = transform.localScale;
            }
            fired = true;
        }

        if (fired) {
            timeforReload -= Time.deltaTime;
            if (timeforReload <= 0){
                timeforReload = 5f;
                fired = false;
            }
        }

        if (!fired){
            ammoBar.fillAmount += .1f;
        }
    }



    void Movement(){
        transform.rotation = Quaternion.identity;

        if (!Application.isMobilePlatform)
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            rb.velocity = new Vector3(Horizontal * speed, Vertical * speed);

            if (Horizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);

            }
            if (Horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        if (Application.isMobilePlatform){
            if (!gyro3){
                if (Input.touchCount > 0){
                    Touch touch = Input.GetTouch(0);
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    touchPosition.z = 0f;
                    if (touch.position.x > Screen.width / 2)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                        // print("right");

                    }
                    else if (touch.position.x < Screen.width / 2)
                    {
                        //print("left");
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    transform.position = Vector3.Lerp(transform.position, touchPosition, 2 * Time.deltaTime);
                } if (Input.acceleration.x>0.5f){
                    shootForMob();
                    bullet.transform.localScale = new Vector3(1,1,1);
                }else if (Input.acceleration.x < -0.5f){
                    shootForMob();
                    bullet.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            if (gyro3){
                //try accelerometer for attack even on touch mode!
                float Xspeed;
                float Yspeed;
                Xspeed = Input.acceleration.x;
                Yspeed = Input.acceleration.y;
                rb.velocity = new Vector3(Xspeed * speed, Vertical * speed);
                if (Yspeed < -0.8f){
                    Vertical = -1f;
                }
                else if (Yspeed > -0.5f){
                    Vertical = 1f;
                }
                if (Xspeed > 0){
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (Xspeed < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                if (Input.touchCount > 0){
                    bullet.transform.localScale = transform.localScale;
                    shootForMob();
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Enemy") {
            HitForEnter();
            AudioSource.PlayClipAtPoint(audios[2], transform.position);

        }

        if (col.gameObject.tag == "Obstacles"){
            HitForEnter();
            AudioSource.PlayClipAtPoint(audios[2], transform.position);
        }
    }
    void Death(){
        if (healthBar.fillAmount <= 0f){
            AudioSource.PlayClipAtPoint(audios[3], transform.position);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy"){
            HitForStay();
        }
        if (col.gameObject.tag == "Obstacles")
        {
            HitForStay();
        }
    }

    void HitForEnter(){
        healthBar.fillAmount -= 0.01f;
        camAnimator.SetTrigger("shake");
        needsTint = true;
        Instantiate(blood, transform.position, Quaternion.identity);
    }

    void HitForStay(){
        needsTint = true;
        healthBar.fillAmount -= Time.deltaTime / 5;
        camAnimator.SetTrigger("shake");
        Instantiate(blood, transform.position, Quaternion.identity);
    }

    private void Tinter(){
        if (needsTint == true){
            float randomColor = Random.Range(0f, 1f);
            tint.color = new Color(randomColor, .5f, .5f, randomColor);
            TimeforColorChange -= Time.deltaTime;
        }
        if (TimeforColorChange <= 0)
        {
            tint.color = new Color(1, 1, 1, 1);
            TimeforColorChange = 1f;
            needsTint = false;
        }
    }

    private void shootForMob(){
        if (ammoBar.fillAmount > 0f){
            Instantiate(bullet, transform.position, Quaternion.identity);
            ammoBar.fillAmount -= .1f;
            AudioSource.PlayClipAtPoint(audios[1], transform.position);
        }
        fired = true;
    }
}
