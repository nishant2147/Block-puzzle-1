using System;
using System.Collections;
using UnityEngine;


public class GridGenerate : MonoBehaviour
{
    public int size;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] SpawnRandomBlocks spawnRandomBlocks;

    GameObject[,] baseBlock;
    GameObject[,] fillBlock;

    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        baseBlock = new GameObject[size, size];
        fillBlock = new GameObject[size, size];
        grid();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void grid()
    {
        baseBlock = new GameObject[size, size];

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                GameObject tile = Instantiate(tilePrefab, transform);
                tile.transform.position = new Vector3(row, col, 0);
                baseBlock[row, col] = tile;
            }
        }
    }

    public bool inRange(Vector2 pos)
    {
        return pos.x > -0.5 && pos.y > -0.5 && pos.x < (size - 0.5) && pos.y < (size - 0.5);
    }
    public Vector2Int convertToVector2Int(Vector3 pos)
    {
        return new Vector2Int((int)(pos.x + 0.1f), (int)(pos.y + 0.5f));
    }

    internal bool isEmpty(BlockPieces block)
    {
        for (int i = 0; i < block.transform.childCount; i++)
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

        if (isEmptyBase(block))
        {
            //var Totalchild = block.transform.childCount;

            for (int i = 0; i < block.transform.childCount; i++)
            {
                var piece = block.transform.GetChild(i).gameObject;
                Vector2Int pos = vectorToInt(piece.transform.position);

                var _piece = baseBlock[pos.x, pos.y];

                piece.transform.position = new Vector2(pos.x, pos.y);
                block.transform.localScale = Vector3.one;
                fillBlock[pos.x, pos.y] = piece;
            }

            block.GetComponent<BoxCollider2D>().enabled = false;
            spawnRandomBlocks.NewBlockGenerate(block);
            Destroyblock();
            CheckGameOver();
        }
        else
        {
            block.moveToOriginalPosition();
        }
    }

    void CheckGameOver()
    {
        for (int r = 0; r < size; r++)
        {
            for (int c = 0; c < size; c++)
            {

                if (fillBlock[r, c] != null) continue;

                var basePiece = baseBlock[r, c];
                for (int i = 0; i < spawnRandomBlocks.NewblockGenerate.Count; i++)
                {
                    var dragPiece = spawnRandomBlocks.NewblockGenerate[i];
                    var tempPos = dragPiece.transform.position;
                    var tempScale = dragPiece.transform.localScale;

                    dragPiece.transform.localScale = Vector3.one;
                    dragPiece.transform.position = basePiece.transform.position;

                    if (isEmptyBase(dragPiece))
                    {
                        dragPiece.transform.position = tempPos;
                        dragPiece.transform.localScale = tempScale;
                        return;
                    }

                    for (int j = 0; j < dragPiece.transform.childCount; j++)
                    {
                        var child = dragPiece.transform.GetChild(j);
                        print($"{j} --> {child.transform.position}");
                    }

                    dragPiece.transform.position = tempPos;
                    dragPiece.transform.localScale = tempScale;
                }
            }
        }

        print("Game Over!!!");
    }

    void Destroyblock()
    {
        for (int i = 0; i < size; i++)
        {
            bool isDestoryVertical = true;
            bool isDestoryHorizontal = true;


            for (int j = 0; j < size; j++)
            {
                if (fillBlock[i, j] == null)
                {
                    isDestoryVertical = false;
                }

                if (fillBlock[j, i] == null)
                {
                    isDestoryHorizontal = false;
                }
            }

            //print(i + "=====>" + isDestoryVertical);

            if (isDestoryVertical)
            {
                for (int j = 0; j < size; j++)
                {
                    fillBlock[i, j].gameObject.transform.parent = null;
                    StartCoroutine(particle(fillBlock[i, j]));
                    //Destroy(fillBlock[i, j].gameObject);
                    fillBlock[i, j] = null;
                }
            }

            if (isDestoryHorizontal)
            {
                for (int j = 0; j < size; j++)
                {
                    fillBlock[j, i].gameObject.transform.parent = null;
                    StartCoroutine(particle(fillBlock[j, i]));
                    //Destroy(fillBlock[j, i].gameObject);
                    fillBlock[j, i] = null;
                }
            }
        }
    }
    IEnumerator particle(GameObject block)
    {
        ParticleSystem particale = block.GetComponent<ParticleSystem>();
        particale.Play();
        block.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1);

        particale.Stop();

        Destroy(block.gameObject);
    }

    public void Highlight(BlockPieces pos)
    {
        clearHighlight();

        if (!isEmptyBase(pos)) return;

        for (int i = 0; i < pos.transform.childCount; i++)
        {
            var piece = pos.transform.GetChild(i);
            //Vector2Int piecePos = vectorToInt(piece.transform.position);
            Vector2Int piecePos = convertToVector2Int(piece.transform.position);

            var block = baseBlock[piecePos.x, piecePos.y];

            block.GetComponent<SpriteRenderer>().sprite = piece.GetComponent<SpriteRenderer>().sprite;
            block.transform.localScale = Vector3.one;

        }
        /* var blocks = baseBlock[pos.x, pos.y].GetComponent<SpriteRenderer>();
         blocks.sprite = sprite;
         blocks.transform.localScale = Vector3.one;*/
    }

    public bool isEmptyBase(BlockPieces pos)
    {
        for (int i = 0; i < pos.transform.childCount; i++)
        {
            var piece = pos.transform.GetChild(i).gameObject;
            Vector2Int piecePos = convertToVector2Int(piece.transform.position);

            if (!inRange(piece.transform.position) || fillBlock[piecePos.x, piecePos.y] != null)
            {
                return false;
            }
        }
        return true;
    }

    internal void clearHighlight()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var block = baseBlock[i, j].GetComponent<SpriteRenderer>();
                block.sprite = sprite;
                block.transform.localScale = Vector3.one * 1.5f;
            }
        }
    }



    /*[SerializeField] Transform baseParent, pieceParent;
    [SerializeField] GameObject[] pieces;
    [SerializeField] GameObject emptyBase;

    [SerializeField] Transform startPos;
    [SerializeField] float offset;

    GameObject[,] baseBlock;

    int size = 10;

    private void Start()
    {
        generateBaseBlock();
        generatePieces();
    }

    private void generatePieces()
    {
        for (int i = 0; i < 3; i++)
        {
            var pos = startPos.position;
            pos.x += (i * offset);
            GameObject tile = Instantiate(pieces[0], pieceParent);
            tile.transform.position = pos;
        }
    }

    private void generateBaseBlock()
    {
        baseBlock = new GameObject[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject tile = Instantiate(emptyBase, baseParent);
                tile.transform.position = new Vector3(i, j, 0);
                baseBlock[i, j] = tile;
            }
        }
    }

    public Vector2Int convertToVector2Int(Vector3 pos)
    {
        return new Vector2Int((int)(pos.x + 0.5f), (int)(pos.y + 0.5f));
    }

    public bool inRange(Vector2 pos)
    {
        return pos.x > -0.5 && pos.y > -0.5 && pos.x < (size - 0.5) && pos.y < (size - 0.5);
    }

    public void highLight(GameObject piece)
    {
        clearHighlight();
        var piecePos = convertToVector2Int(piece.transform.position);
        var _base = baseBlock[piecePos.x, piecePos.y];
        var baseSprite = _base.GetComponent<SpriteRenderer>();
        var pieceSprite = piece.transform.GetChild(0).GetComponent<SpriteRenderer>();

        baseSprite.sprite = pieceSprite.sprite;
        _base.transform.localScale = Vector3.one;

    }

    public void clearHighlight()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                baseBlock[i, j].GetComponent<SpriteRenderer>().sprite = emptyBase.GetComponent<SpriteRenderer>().sprite;
                baseBlock[i, j].transform.localScale = Vector3.one * 1.5f;
            }
        }
    }*/

}
