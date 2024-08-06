using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class GridGenerate : MonoBehaviour
{
    public static GridGenerate instance;

    public int size;
    public GameObject tilePrefab;

    GameObject[,] baseBlock;
    GameObject[,] fillBlock;
    // Start is called before the first frame update
    void Start()
    {
        baseBlock = new GameObject[size, size];
        fillBlock = new GameObject[size, size];
        instance = this;
        grid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void grid()
    {
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                GameObject tile = Instantiate(tilePrefab, transform);
                tile.transform.position = new Vector3(col, row, 0);
                baseBlock[row, col] = tile;
            }
        }
    }

    internal bool inRange(Vector2 pos)
    {
        return pos.x > -0.5 && pos.y > -0.5 && pos.x < (size - 0.5) && pos.y < (size - 0.5);
    }

    internal bool isEmpty(BlockPieces block)
    {
        for(int i = 0; i < block.transform.childCount; i++)
        {
            var Pieces = block.transform.GetChild(i);
            Vector2Int pos = vectorToInt(Pieces.transform.position);
            print("Position.x = " + pos.x + "Position.y = " + pos.y);
            if (fillBlock[pos.x, pos.y])
            {
                print("block ====> ");
                return false;
            }
        }
        return true;
    }

    private Vector2Int vectorToInt(Vector3 pos)
    {
        return new Vector2Int((int)(pos.x + 0.5f), (int)(pos.y + 0.5f));
    }

    internal void PlacesBlock(BlockPieces block)
    {
        block.blockPlaced = true;

        for (int i = 0; i < block.transform.childCount; i++)
        {
            var Pieces = block.transform.GetChild(i).gameObject;
            Vector2Int pos = vectorToInt(Pieces.transform.position);
            fillBlock[pos.x, pos.y] = Pieces;
            Pieces.transform.position = new Vector3(pos.x, pos.y);
        }
    }
}
