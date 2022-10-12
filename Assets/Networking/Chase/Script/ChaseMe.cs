using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChaseMe : MonoBehaviour {
    private Transform target;
    private float speed;
    private Image img;
	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        img = GameObject.FindGameObjectWithTag("Life").GetComponent<Image>();
        speed = .1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (img.fillAmount <= 0){
            SceneManager.LoadScene("Start");
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        speed = speed + Time.deltaTime * .5f ;
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speed*Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Player")
        {
            img.fillAmount -= 0.01f;
        }
    }
        void OnCollisionStay2D(Collision2D col){
            if (col.gameObject.tag == "Player"){
                img.fillAmount -= Time.deltaTime*Time.deltaTime;
            }
        }
}
