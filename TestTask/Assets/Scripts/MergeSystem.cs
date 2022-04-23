using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSystem : MonoBehaviour
{
    #region Singleton
    public static MergeSystem instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public void MergeAndUpgrade(List<GameObject> list, GameObject nextLevelPrefab, List<GameObject> nextLevelList, int lvlUpSpawnModifier)
    {
        foreach (GameObject element in list) //for every element in the list
        {
            Destroy(element); //destroy the game object
            SpawnData.instance.numberOfActiveElements--; //reduce the number of active elements on screen
        }
        list.Clear(); //clear the list

        SpawnManager.instance.newElement = Instantiate(nextLevelPrefab, PlaceholderSystem.instance.placeholders[nextLevelList.Count + lvlUpSpawnModifier]); //spawn the level-up element at the first free placecholder
        nextLevelList.Add(SpawnManager.instance.newElement); //add the new level element to it's level list
        SpawnData.instance.numberOfActiveElements++; //increment the number of active elements on screen

        //reset all level caps
        SpawnData.instance.level1Cap = false;
        SpawnData.instance.level2Cap = false;
        SpawnData.instance.level3Cap = false;
        SpawnData.instance.level4Cap = false;
        SpawnData.instance.level5Cap = false;

        SpawnData.instance.spawnElement1 = false; //reset the spawn trigger
    }
}
