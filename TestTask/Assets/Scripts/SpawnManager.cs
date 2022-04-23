using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Singleton
    public static SpawnManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject newElement; //temporary slot for newly spawned element
    public GameObject newPlaceholder; //temporary slot for newly spawned placeholder

    private void Update()
    {
        if (SpawnData.instance.level1Cap) //when the cap of active level 1 elements has been reached
        {
            MergeSystem.instance.MergeAndUpgrade(SpawnData.instance.level1Elements, SpawnData.instance.element2, SpawnData.instance.level2Elements, SpawnData.instance.element2spawn);
        }
        else if (SpawnData.instance.level2Cap) //when the cap of active level 2 elements has been reached
        {
            MergeSystem.instance.MergeAndUpgrade(SpawnData.instance.level2Elements, SpawnData.instance.element3, SpawnData.instance.level3Elements, SpawnData.instance.element3spawn);
        }
        else if (SpawnData.instance.level3Cap) //when the cap of active level 3 elements has been reached
        {
            MergeSystem.instance.MergeAndUpgrade(SpawnData.instance.level3Elements, SpawnData.instance.element4, SpawnData.instance.level4Elements, SpawnData.instance.element4spawn);
        }
        else if (SpawnData.instance.level4Cap) //when the cap of active level 4 elements has been reached
        {
            MergeSystem.instance.MergeAndUpgrade(SpawnData.instance.level4Elements, SpawnData.instance.element5, SpawnData.instance.level5Elements, SpawnData.instance.element5spawn);
        }

        if (SpawnData.instance.spawnElement1)
        {
            newElement = Instantiate(SpawnData.instance.element1, PlaceholderSystem.instance.placeholders[SpawnData.instance.level1Elements.Count + SpawnData.instance.element1spawn]); //spawn the element1 object as a child of the according placeholder
            SpawnData.instance.numberOfActiveElements++; //increase the number of active elements on screen
            SpawnData.instance.level1Elements.Add(newElement); //add the new element1 to the list
            SpawnData.instance.spawnElement1 = false; //reset the spawn trigger
        }
    }

}

        

