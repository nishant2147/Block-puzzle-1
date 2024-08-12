using UnityEngine;

public class SpawnRandomBlocks : MonoBehaviour
{
    float startPos = .6f;
    float offset = 2f;

    [SerializeField]
    GameObject[] Pieces;

    [SerializeField]
    int SpawnStartPos;

    public void NewBlockGenerate(GameObject block)
    {
        if (block == null)
        {
            GenerateNewBlock();
        }
    }

    void GenerateNewBlock()
    {
        for (int i = 0; i < 3; i++)
        {
            float k = startPos + (i * offset);
            var Piece = Instantiate(Pieces[Random.Range(SpawnStartPos, Pieces.Length)], new Vector2(k, 0), Quaternion.identity);
            Piece.transform.SetParent(transform, false);
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
