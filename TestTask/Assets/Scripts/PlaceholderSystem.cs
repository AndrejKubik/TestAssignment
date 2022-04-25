using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderSystem : MonoBehaviour
{
    #region Singleton
    public static PlaceholderSystem instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    private GameObject newPlaceholder; //temporary slot for newly spawned placeholder

    public GameObject placeholderTemplate;
    public List<Transform> placeholders;
    public int placeholderSpawnCounter;
    public int spawnDirection; // -1 is to the left, 1 is to the right 
    public float placeholderOffsetZ;
    public float placeholderOffsetX;

    private void Update()
    {

        if (SpawnData.instance.placeholderNeeded) //if new placeholder is requested
        {
            //spawn another placeholder next to the final placeholder in the array
            newPlaceholder = Instantiate(placeholderTemplate, new Vector3
            (placeholders[placeholders.Count - 1].transform.position.x +
            placeholderOffsetX * spawnDirection, placeholders[placeholders.Count - 1].transform.position.y,
            placeholders[placeholders.Count - 1].transform.position.z + placeholderOffsetZ), transform.rotation);

            placeholders.Add(newPlaceholder.transform); //add the newly spawned placeholder to the placeholders list
            placeholderSpawnCounter++; //increment the counter
            placeholderOffsetZ = 0; //clear out the Z offset so that only 1 spawns below

            SpawnData.instance.placeholderNeeded = false; //reset the trigger
        }

        if (placeholderSpawnCounter >= SpawnData.instance.rowCap) //when the placeholders row cap has been reached
        {
            placeholderOffsetZ = -3f; //change the Z-axis offset so that new placeholders spawn in a new row below
            spawnDirection = -spawnDirection; //change from 1 to -1 and inverse
            placeholderSpawnCounter = 0; //reset the counter
        }

        if (placeholderOffsetZ != 0) placeholderOffsetX = 0; //whenever there is a Z offset, remove the X offset
        else if (placeholderOffsetZ == 0) placeholderOffsetX = 3f; //when there is no Z offset, reset the X offset
    }
}
