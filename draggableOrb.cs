using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draggableOrb : MonoBehaviour
{
    public bool isDrag;
    public bool isInPlace;

    public GameObject cellSpace;



    void Start()
    {
        isDrag = false;

    }

    void Update()
    {

        isBeingDragged();

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cell Station")
        {
            cellSpace = collision.gameObject;

            if (Input.touchCount < 1)
            {
                transform.position = collision.transform.position;
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

                if (gameObject.transform.position != touchpos)
                {
                    collision.gameObject.transform.position = cellSpace.transform.position;
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
            Vector2 initialTouch = Camera.main.ScreenToWorldPoint(touch.position); //you need to find a way to make it just select the specific point where you FIRST TAPPED
            touchpos = new Vector2(Mathf.Clamp(touchpos.x, touchpos.x - 1, touchpos.x + 1), Mathf.Clamp(touchpos.y, touchpos.y - 1, touchpos.y + 1));
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
            isDrag = false;
        }
    }




}
