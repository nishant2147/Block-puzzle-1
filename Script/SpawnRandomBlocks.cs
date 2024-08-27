using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomBlocks : MonoBehaviour
{
    float startPos = .7f;
    float offset = 2.5f;

    [SerializeField]
    GameObject[] Pieces;

    [SerializeField]
    int SpawnStartPos;

    public List<BlockPieces> NewblockGenerate = new List<BlockPieces>(3);
    int cnt = 0;

    public void NewBlockGenerate(BlockPieces block)
    {
        NewblockGenerate.Remove(block);
        //cnt--;
        if (NewblockGenerate.Count == 0)
        {
            GenerateNewBlock();
        }
    }
    void GenerateNewBlock()
    {
        for (int i = cnt; i < 3; i++)
        {
            float k = startPos + (i * offset);
            var Piece = Instantiate(Pieces[Random.Range(SpawnStartPos, Pieces.Length)], new Vector2(k, 0), Quaternion.identity);
            Piece.transform.SetParent(transform, false);
            NewblockGenerate.Add(Piece.GetComponent<BlockPieces>());
            //cnt++;
        }
    }

    void Start()
    {
        GenerateNewBlock();
    }

    private void Update()
    {
        //var mainPieace = GameObject.FindGameObjectsWithTag("Blocks");
    }
}
