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

    public bool mergerDestroyed;

    public void MergeAndUpgrade(List<GameObject> list, GameObject nextLevelPrefab, List<GameObject> nextLevelList, int lvlUpSpawnModifier)
    {
        Merge(list);
        StartCoroutine(SpawnNextLevelElement(nextLevelPrefab, nextLevelList, lvlUpSpawnModifier));
        ResetLevelCapBools();

        SpawnData.instance.spawnElement1 = false; //reset the spawn trigger
    }

    private void Merge(List<GameObject> list)
    {
        SpawnData.instance.canSpawn = false; //pause further spawning
        list[0].tag = "Merger"; //set the first element of the merging kind to be the main collision target
        merger = list[0].transform; //store the merger's transform
        foreach (GameObject element in list) //for every element in the list
        {
            cubeAnimations = element.GetComponent<CubeAnimations>(); //get the element's animation script reference

            if (element != list[0]) //if the current element is not the merger
            {
                element.GetComponent<Animator>().enabled = false; //disable the current element's animator so that the object can move
                element.tag = "Destroyable"; //change the element's tag
            }

            cubeAnimations.isMerging = true; //activate the element's merging sequence
            SpawnData.instance.numberOfActiveElements--; //reduce the number of active elements on screen
        }

        list.Clear(); //clear the list
    }

    private IEnumerator SpawnNextLevelElement(GameObject nextLevelPrefab, List<GameObject> nextLevelList, int lvlUpSpawnModifier)
    {
        yield return new WaitUntil(() => mergerDestroyed); //pause the method until the merger has been destroyed

        SpawnManager.instance.newElement = Instantiate(nextLevelPrefab, PlaceholderSystem.instance.placeholders[nextLevelList.Count + lvlUpSpawnModifier]); //spawn the level-up element at the first free placecholder
        nextLevelList.Add(SpawnManager.instance.newElement); //add the new level element to it's level list
        SpawnData.instance.numberOfActiveElements++; //increment the number of active elements on screen

        mergerDestroyed = false; //reset the trigger
    }

    private void ResetLevelCapBools()
    {
        //reset all level caps
        SpawnData.instance.level1Cap = false;
        SpawnData.instance.level2Cap = false;
        SpawnData.instance.level3Cap = false;
        SpawnData.instance.level4Cap = false;
        SpawnData.instance.level5Cap = false;

        SpawnData.instance.canSpawn = true; //resume further spawning
    }
}
