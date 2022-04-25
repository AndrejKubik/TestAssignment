using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    private void Update()
    {
        if (SpawnData.instance.level5Cap) Debug.Log("EZ WIN");
    }
}
