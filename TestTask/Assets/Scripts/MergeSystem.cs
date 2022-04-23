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

    public Transform merger;
    private CubeAnimations cubeAnimations;
    public void MergeAndUpgrade(List<GameObject> list, GameObject nextLevelPrefab, List<GameObject> nextLevelList, int lvlUpSpawnModifier)
    {
        list[0].tag = "Merger"; //set the first element of the merging kind to be the main collision target
        merger = list[0].transform; //store the merger's transform
        foreach (GameObject element in list) //for every element in the list
        {
            cubeAnimations = element.GetComponent<CubeAnimations>(); //get the element's animation script reference
            element.GetComponent<Animator>().enabled = false; //disable the current element's animator so that the object can move
            list[0].GetComponent<Animator>().enabled = true; //enable the merger's animator component

            cubeAnimations.isMerging = true; //activate it's merging sequence

            SpawnData.instance.numberOfActiveElements--; //reduce the number of active elements on screen
        }

        //for (int i = 0; i < list.Count; i++) //for every element in the list
        //{
        //    cubeAnimations = list[i].GetComponent<CubeAnimations>(); //get the element's animation script reference
        //    list[i].GetComponent<Animator>().enabled = false; //disable the current element's animator so that the object can move
        //    list[0].GetComponent<Animator>().enabled = true; //enable the merger's animator component

        //    cubeAnimations.isMerging = true; //activate it's merging sequence

        //    SpawnData.instance.numberOfActiveElements--; //reduce the number of active elements on screen
        //}

        list.Clear(); //clear the list

        //SpawnManager.instance.newElement = Instantiate(nextLevelPrefab, PlaceholderSystem.instance.placeholders[nextLevelList.Count + lvlUpSpawnModifier]); //spawn the level-up element at the first free placecholder
        //nextLevelList.Add(SpawnManager.instance.newElement); //add the new level element to it's level list
        //SpawnData.instance.numberOfActiveElements++; //increment the number of active elements on screen

        //reset all level caps
        SpawnData.instance.level1Cap = false;
        SpawnData.instance.level2Cap = false;
        SpawnData.instance.level3Cap = false;
        SpawnData.instance.level4Cap = false;
        SpawnData.instance.level5Cap = false;

        SpawnData.instance.spawnElement1 = false; //reset the spawn trigger
    }
}
