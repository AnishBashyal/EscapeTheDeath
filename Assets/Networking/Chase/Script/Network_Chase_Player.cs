using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//using UnityEngine.UI;

public class Network_Chase_Player : NetworkBehaviour {
    private float speed = 10f;
    public Sprite indicator;
    //private Image healthBar;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;


    void Awake() {

    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // transform.rotation = Quaternion.identity;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        if (!isLocalPlayer) {
            return;
        }



        if (Application.isMobilePlatform)
        {
            float Vertical = 0f;
            float Xspeed;
            float Yspeed;
            Xspeed = Input.acceleration.x;
            Yspeed = Input.acceleration.y;
            if (Yspeed < -0.8f)
            {
                Vertical = -1f;
            }
            else if (Yspeed > -0.3f)
            {
                Vertical = 1f;
            }
            else
            {
                Vertical = 0;
            }
            if (Xspeed > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);

            }
            else if (Xspeed < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                Xspeed = -Xspeed;
            }
            transform.Translate(new Vector2(Xspeed * 2, Vertical) * Time.deltaTime * speed);
            if (Input.GetMouseButtonDown(0)) {
                CmdFire();
            }
        } 
        if (!Application.isMobilePlatform)
        {

            float Horizontal = Input.GetAxis("Horizontal");
            float Vertical = Input.GetAxis("Vertical");
            if (Input.GetAxis("Horizontal") > 0)
            {
               // transform.Translate(Vector2.right * Time.deltaTime * speed);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetAxis("Horizontal") < 0){
                transform.rotation = Quaternion.Euler(0, 180, 0);
                Horizontal = -Horizontal;
            }

            transform.Translate(new Vector3(Horizontal, Vertical,transform.position.z) * Time.deltaTime * speed);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CmdFire();
            }
        }

        if (transform.position.x > 9.5f)
            {
                //   print("too much right");
                transform.position = new Vector3(-8f, transform.position.y, transform.position.z);
            }
            if (transform.position.x < -10f)
            {
                // print("too much left");
                transform.position = new Vector3(8f, transform.position.y, transform.position.z);

            }
            if (transform.position.y > 5.8f)
            {
                //print("too much up");
                transform.position = new Vector3(transform.position.x, -5f, transform.position.z);

            }
            if (transform.position.y < -6f)
            {
                //print("too much down");
                transform.position = new Vector3(transform.position.x, 4f, transform.position.z);

            }
        }
    [Command]
    void CmdFire(){
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        NetworkServer.Spawn(bullet);
    }
  

    public override void OnStartLocalPlayer()
    {
        GetComponent<SpriteRenderer>().sprite = indicator;
    }
    
    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Enemy"){
           // SceneManager.LoadScene("Start");
        }
    }
  }

