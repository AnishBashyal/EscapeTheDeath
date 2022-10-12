using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Evolution : MonoBehaviour
{
    public GameObject Particles;
    public GameObject Lv2;
    //public bool IsEvolution2;
    private bool Entered;
    public string SceneName;
    public Animator anime;
    public GameObject enemy;
    // Use this for initialization
    void Start()
    {
     

    }

    // Update is called once per frame
    void Update()
    {

        if (Entered)
        {
            StartCoroutine(EvolutionCharacterSetActive());
        }
        GetComponent<Rigidbody2D>().velocity = new Vector3(2, 0, 0);

        if (Input.GetMouseButtonDown(0)){
            StartCoroutine(SceneChange());

        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        Instantiate(Particles, transform.position, Quaternion.identity);
        Entered = true;
        print("Entered");


    }

    IEnumerator SceneChange(){
        yield return new WaitForSeconds(1f);
        anime.SetTrigger("Fading");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneName);
    }
    IEnumerator EvolutionCharacterSetActive()
    {
        yield return new WaitForSeconds(4f);
        Lv2.SetActive(true);
        if (enemy) { 
        yield return new WaitForSeconds(4f); 
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector3(10, 0, 0);
        }
        yield return new WaitForSeconds(10f);
        anime.SetTrigger("Fading");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneName);

    }
}
    


