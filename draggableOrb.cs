using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draggableOrb : MonoBehaviour
{
    public bool isDrag;
    public bool isInPlace;
    public GameObject cellGrid;
    public gameGrid gridCellStructure;

    public int locationX;
    public int locationY;


    public int[,] currentLocation = new int[4, 4];
    public int[,] previousLocation = new int[4, 4];


    void Start()
    {
        gridCellStructure = cellGrid.GetComponent<gameGrid>();
        isInPlace = false;
        isDrag = false;
    }

    void Update()
    {
        if (gridCellStructure.canMoveOrbs == true)
        {
            isBeingDragged();
            originalPositionChanged();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cell Station")
        {
            if (Input.touchCount < 1)
            {
                transform.position = collision.transform.position;
            }

            if (transform.position == collision.transform.position && Input.touchCount == 0)
            {
                isInPlace = true;
            }
            else
            {
                isInPlace = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


    }

    void isBeingDragged()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchpos = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Began && Input.touchCount == 1)
            {
                if (gameObject.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchpos))
                {
                    isDrag = true;


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

    /** void originalPositionChanged()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Ended)
        {
            gridCellStructure.orbMoved = false;
        }
    }
    */

}
