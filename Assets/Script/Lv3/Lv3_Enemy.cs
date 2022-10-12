using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lv3_Enemy : MonoBehaviour {

    public Transform target;
    public float speed;

    private bool needsTint;
    private float TimeforColorChange = 1f;
    private SpriteRenderer tint;
    private float Damage;
    public Animator camAnimator;
    public GameObject blood;

    public AudioClip hit; 
    Image healthBar;

    void Awake(){
        tint = GetComponent<SpriteRenderer>();
        healthBar = GameObject.FindGameObjectWithTag("LifeofEnemy").GetComponent<Image>();
    }
    // Use this for initialization
    void Start () {
        Damage = 0.005f;
	}
	
	// Update is called once per frame
	void Update () {
        Tinter();
	}

    void FixedUpdate(){
        Movement();
        Difficulty();
    }

    void Movement()
    {
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
        if (target.position.x > transform.position.x){
            //he's infront
            transform.localScale = new Vector3(1.3f, .8f, 1);
        } else if (target.position.x < transform.position.x){
            //he's behind
            transform.localScale = new Vector3(-1.3f, .8f, 1);
        }
    }

    void OnParticleCollision(GameObject other){
        healthBar.fillAmount -= Damage;
        Destroy(other.gameObject);
        needsTint = true;
        Instantiate(blood, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(hit, transform.position);
        camAnimator.SetTrigger("shake");
    }

    void Difficulty(){
        //print("Enemy" + speed);
        if (healthBar.fillAmount < .8f && healthBar.fillAmount > .5f){
            //05-08
            Damage = 0.007f;
            speed = 1.5f;
        }

        else if (healthBar.fillAmount < .5f && healthBar.fillAmount > .2f)
        {
            //02-0.5
            speed = 2.5f;
            Damage = 0.009f;
        }
        else if (healthBar.fillAmount < .2f)
        {
            //02-0
            speed = 3f;
            Damage = 0.01f;
        }

        if (healthBar.fillAmount <= 0){
            Instantiate(blood,transform.position,Quaternion.identity);
            speed = 0;
        }
     }

    private void Tinter()
    {
        if (needsTint == true)
        {
            float randomColor = Random.Range(0f, 1f);
            tint.color = new Color(randomColor, .5f, .5f, randomColor);
            TimeforColorChange -= Time.deltaTime;
        }
        if (TimeforColorChange <= 0)
        {
            tint.color = new Color(1, 1, 1, .03f);
            TimeforColorChange = 1f;
            needsTint = false;
        }
    }
}
