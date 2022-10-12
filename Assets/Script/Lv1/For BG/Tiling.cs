using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

    public int offsetX = 25;
    public bool HasDupliateLayer = false;
    private float spriteWidth = 0f;
    private Camera myCam;
    private Transform myTransform;
    
    void Awake(){
        myCam = Camera.main;
        myTransform = transform;
    }
	// Use this for initialization
	void Start () {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.sprite.bounds.size.x;
}
	
	// Update is called once per frame
	void Update () {
        if (HasDupliateLayer == false) {
            float camHorizontalExtend = myCam.orthographicSize * Screen.width / Screen.height;
            float EdgeVisibleRight = (myTransform.position.x + spriteWidth / 2)- camHorizontalExtend;
                 if (myCam.transform.position.x >= EdgeVisibleRight - offsetX && HasDupliateLayer == false) {
                    MakeDuplicateLayer();
                    HasDupliateLayer = true;
                }
        }
    }

    void MakeDuplicateLayer() {
        Vector3 TargetPos = new Vector3(myTransform.position.x + spriteWidth, myTransform.position.y, myTransform.position.z);
        Transform DuplicateLayer = Instantiate(myTransform, TargetPos, myTransform.rotation) as Transform;
        DuplicateLayer.localScale = new Vector3(DuplicateLayer.localScale.x * -1, DuplicateLayer.localScale.y, DuplicateLayer.localScale.z);
        DuplicateLayer.parent = myTransform.parent;
    }
}
