using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draggableOrb : MonoBehaviour
{
    public bool isDrag;
    public bool isInPlace;

    public Vector2 initialTouch;

    public GameObject originCellNew;
    public GameObject originCell;

    public bool xLock;
    public bool yLock;

    void Start()
    {
        isDrag = false;
        initialTouch = Vector2.zero;
        xLock = false;
        yLock = false;
    }

    void Update()
    {
        isBeingDragged();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cell Station")
        {
            if (originCell == null)
            {
                originCell = collision.gameObject;
                transform.position = originCell.transform.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Contains("Orb"))
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchpos = Camera.main.ScreenToWorldPoint(touch.position);
                draggableOrb otherOrbScript = collision.GetComponent<draggableOrb>();

                if (otherOrbScript.isDrag == false)
                {
                    originCellNew = otherOrbScript.originCell;
                    otherOrbScript.originCell = originCell;
                    collision.gameObject.transform.position = otherOrbScript.originCell.transform.position;
                    originCell = originCellNew;
                }
            }
        }
    }



    void isBeingDragged()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchpos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                initialTouch = originCell.transform.position;
            }


            if (xLock == true)
            {
                touchpos = new Vector2(touchpos.x, Mathf.Clamp(touchpos.y, initialTouch.y - .25f, initialTouch.y + .5f));

            }
            if (yLock == true)
            {
                touchpos = new Vector2(Mathf.Clamp(touchpos.x, initialTouch.x - .5f, initialTouch.x + .5f), touchpos.y);

            }


            if (touchpos.x > initialTouch.x + .5 || touchpos.x < initialTouch.x - .5)
            {
                xLock = true;
            }
            else if (touchpos.y > initialTouch.y + .5 || touchpos.y < initialTouch.y - .5)
            {
                yLock = true;
            }



            if (touch.phase == TouchPhase.Began && Input.touchCount == 1)
            {
                if (gameObject.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchpos))
                {
                    isDrag = true;
                    Vector2 originLocation = touchpos;
                }
            }

            if (touch.phase == TouchPhase.Moved && isDrag == true)
            {
                transform.position = touchpos;
            }
        }

        else
        {
            transform.position = originCell.transform.position;
            isDrag = false;
            xLock = false;
            yLock = false;
        }
    }




}
