using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomBlocks : MonoBehaviour
{
    float k;
    public GameObject[] blockPrefabs;
    int numberOfBlocksToSpawns = 4;

    void Start()
    {
        SpawnBlocks();
    }

    void SpawnBlocks()
    {
        for (int i = 1; i < numberOfBlocksToSpawns; i++)
        {
            if (i == 1)
            {
                k = 0.6f;
            }
            else if (i == 2)
            {
                k = 2.7f;
            }
            else if (i == 3)
            {
                k = 5f;
            }

            var block = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)], new Vector2(k, 0), Quaternion.identity);
            block.transform.SetParent(transform, false);
        }
    }

}
