using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float speed = 20f;
    public GameObject destroyEffect;
    private Character character;
    private bool Left;
    private bool Right;
    // Use this for initialization
    void Start()
    {

        Invoke("DestroyParticle", 3);
        character = FindObjectOfType<Character>();
        if (character.transform.localScale.x == 1)
        {
            Right = true;
           // print("firing right");

        }
        else
        {
            Right = false;
        }
         if (character.transform.localScale.x == -1)
        {
            Left = true;
           // print("firing left");
        }
        else
        {
            Left = false;
        }
    }

    void DestroyParticle()
    {
        Destroy(gameObject);
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Destroy(col.gameObject);
            print("enemy ahead");
            DestroyParticle();
        }
        if (col.tag == "Land")
        {
            DestroyParticle();
        }
   
    }
    // Update is called once per frame
    void Update()   {
        if (Right)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

        }
        if (Left){
            transform.Translate(Vector2.left * speed * Time.deltaTime);

        }
    }
}
