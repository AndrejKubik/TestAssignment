using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnData: MonoBehaviour
{
    #region Singleton
    public static SpawnData instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    //prefabs for every level elements
    public GameObject element1;
    public GameObject element2;
    public GameObject element3;
    public GameObject element4;
    public GameObject element5;

    //separate lists of different level elements
    public List<GameObject> level1Elements;
    public List<GameObject> level2Elements;
    public List<GameObject> level3Elements;
    public List<GameObject> level4Elements;
    public List<GameObject> level5Elements;

    //list index of a placeholder
    public int element1spawn;
    public int element2spawn;
    public int element3spawn;
    public int element4spawn;
    public int element5spawn;

    //spawn variables
    public int numberOfActiveElements;
    public int levelCap = 10;
    public int rowCap = 5;

    public bool level1Cap;
    public bool level2Cap;
    public bool level3Cap;
    public bool level4Cap;
    public bool level5Cap;

    public bool spawnElement1;
    public bool placeholderNeeded;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) spawnElement1 = true; //when player presses space trigger the element1 spawn

        if(spawnElement1)
        {
            if (level1Elements.Count >= levelCap) //when the number of active level 1 elements reaches the level cap
            {
                level1Cap = true; //when the number of active level 1 elements reaches the level cap, trigger the bool
                element1spawn++; //increment the level 1 spawn modifier
            }
            else if (level2Elements.Count >= levelCap) //when the number of active level 2 elements reaches the level cap
            {
                element1spawn -= level2Elements.Count - 1; //return the spawn point to the first empty slot for level 1 element
                element2spawn++; //increment the spawn index for level 2 elements
                level2Cap = true; //trigger the bool
            }
            else if (level3Elements.Count >= levelCap) //when the number of active level 3 elements reaches the level cap
            {
                //correct the spawn points to the first empty placeholder
                element1spawn -= level3Elements.Count - 1;
                element2spawn -= level3Elements.Count - 1;
                element3spawn++; //increment the spawn index for level 3 elements
                level3Cap = true; //when the number of active level 3 elements reaches the level cap, trigger the bool
            }
            else if (level4Elements.Count >= levelCap) //when the number of active level 4 elements reaches the level cap
            {
                //correct the spawn points to the first empty placeholder
                element1spawn -= level4Elements.Count - 1;
                element2spawn -= level4Elements.Count - 1;
                element3spawn -= level4Elements.Count - 1;
                element4spawn++; //increment the spawn index for level 4 elements
                level4Cap = true; //when the number of active level 4 elements reaches the level cap, trigger the bool
            }
            else if (level5Elements.Count >= levelCap) //when the number of active level 5 elements reaches the level cap
            {
                level5Cap = true; //when the number of active level 5 elements reaches the level cap, trigger the bool
            }
            else if (numberOfActiveElements == PlaceholderSystem.instance.placeholders.Count) placeholderNeeded = true; //if there is not enough slots for the new spawn, ask for another
        }
    }
}
