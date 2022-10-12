using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    public GameObject firePoint;
    public GameObject bullet;
    public Transform target;
    private float Ammo;
    private float Amothreshold;
    private bool FireState;

    Image Ammobar;
    // Use this for initialization
    void Start () {
        Ammobar = GameObject.FindGameObjectWithTag("AmmoBar").GetComponent<Image>();
        Ammobar.fillAmount = 1;
        Amothreshold = 10;
        Ammo = Amothreshold;
    }
	
	// Update is called once per frame
	void Update () {
        Ammobar.fillAmount = (Ammo/Amothreshold);
        //print(Ammo);
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float ClampedrotZ = Mathf.Clamp(rotZ, -45f, 45f);
        if (target.transform.localScale.x == 1)
        {
            ClampedrotZ *= 1;
        }
        if (target.transform.localScale.x==-1)
        {
            ClampedrotZ *= -1;
        }
        transform.rotation = Quaternion.Euler(0, 0, ClampedrotZ);
       
        //firePoint.transform.localEulerAngles = transform.eulerAngles;

        if (Ammo == Amothreshold)
        {
            FireState = true;
          //  print("Ammo Full");
        }
        if (FireState){
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bullet, firePoint.transform.position, transform.rotation);
                Ammo = Ammo - Time.deltaTime*10;
              //  print("MOuse Hold");
                //    print("Firing");
            }
            if (Input.GetMouseButtonUp(0))
            {
            //    print("Mouse Realese");
            }
            else if (Input.GetMouseButton(0))
            {
                Instantiate(bullet, firePoint.transform.position, transform.rotation);
                Ammo = Ammo - Time.deltaTime*10;
                //  print("Firing");
          //      print("MOuse Hold");
            }
        }
        if (Ammo <= 0)
        {
            FireState = false;
       //     print("Ammo finished");
        }

        if (!FireState){
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload(){
       // print("Reloading");
        yield return new WaitForSeconds(2f);
        Ammo = Amothreshold;
        //print("Reloaded Sucessfully");
    }

}
