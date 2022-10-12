using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCheckPoint : MonoBehaviour {
    public AudioClip checkpoint;
    public bool IsTurnOfScene2;

    void OnTriggerEnter2D(Collider2D other) {
        if (!IsTurnOfScene2)
        {
            if (other.CompareTag("Player"))
            {
                CheckPoint.LastCheckPointPos = transform.position;
                AudioSource.PlayClipAtPoint(checkpoint, transform.position);
            }
        }
        if (IsTurnOfScene2)
        {
            if (other.CompareTag("Player2"))
            {
                CheckPoint2.LastCheckPointPos = transform.position;
                AudioSource.PlayClipAtPoint(checkpoint, transform.position);
            }
        }

    }
}
