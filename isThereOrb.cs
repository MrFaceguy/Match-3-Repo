using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isThereOrb : MonoBehaviour
{
    public GameObject orbHighlight;

    private void OnTriggerEnter2D(Collider2D collsion)
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(Input.touchCount < 1)
        {
            orbHighlight = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        orbHighlight = null;
    }
}
