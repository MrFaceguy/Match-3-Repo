using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class gameGrid : MonoBehaviour
{
    public GameObject cell;
    public Transform gridPos;
    public float distributeIt;
    public bool isGridOK = false;

    public bool orbMoved = false;
    public bool canMoveOrbs = false;


    public GameObject redOrb;
    public GameObject blueOrb;
    public GameObject greenOrb;

    public GameObject[,] gridArray = new GameObject[4, 4];
    public GameObject[,] orbArray = new GameObject[4, 4];
    public bool[,] setToDelete = new bool[4, 4];

    public enum phases { StartingPhase, PlayerPhase, OrbExecutePhase, EnemyPhase }
    public phases currentPhase;

    void Start()
    {
        currentPhase = phases.StartingPhase;

        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                gridArray[x, y] = Instantiate(cell, new Vector3(gridPos.position.x + (x * distributeIt), gridPos.position.y - (y * distributeIt), 0), transform.rotation);
                gridArray[x, y].name = "cell " + x + "-" + y;
            }
        }
        currentPhase = phases.PlayerPhase;
    }

    void Update()
    {
        if (currentPhase == phases.PlayerPhase)
        {
            canMoveOrbs = true;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    setOrbArray(x, y); //reset the orb array to be correct
                    if (orbMoved == true)
                    {
                        currentPhase = phases.OrbExecutePhase;
                    }
                }
            }
        }

        if (currentPhase == phases.OrbExecutePhase)
        {
            canMoveOrbs = false;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    isThereAMatch(x, y);
                    deleteOrbs(x, y);
                }
            }
        }

        if (currentPhase == phases.EnemyPhase)
        {

        }
    }



    bool newGridOK()
    {
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                if (setToDelete[x, y] == true)
                {
                    return false;
                }
            }
        }
        return true;
    }


    void orbOrganisation(int x, int y)
    {
        if (orbArray[x, y] == null)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    makeARedOrb(x, y);
                    break;

                case 1:
                    makeABlueOrb(x, y);

                    break;

                case 2:
                    makeAGreenOrb(x, y);
                    break;
            }
        }
    }

    void makeARedOrb(int theX, int theY)
    {
        orbArray[theX, theY] = Instantiate(redOrb, gridArray[theX, theY].transform.position, Quaternion.identity);
    }

    void makeABlueOrb(int theX, int theY)
    {
        orbArray[theX, theY] = Instantiate(blueOrb, gridArray[theX, theY].transform.position, Quaternion.identity);
    }

    void makeAGreenOrb(int theX, int theY)
    {
        orbArray[theX, theY] = Instantiate(greenOrb, gridArray[theX, theY].transform.position, Quaternion.identity);
    }


    void setOrbArray(int i, int j)
    {

        isThereOrb currentCellScript = gridArray[i, j].GetComponent<isThereOrb>();

        if (currentCellScript.orbHighlight != null)
        {
            orbArray[i, j] = currentCellScript.orbHighlight;

        }
    }

    void isThereAMatch(int x, int y)
    {

        //detect if there is a match

    }

    void deleteOrbs(int i, int j)
    {
        if (setToDelete[i, j] == true)
        {
            Destroy(orbArray[i, j].gameObject);

            setToDelete[i, j] = false;

        }
    }
}
