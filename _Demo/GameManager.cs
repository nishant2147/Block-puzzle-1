using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] Transform baseParent, pieceParent;
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

    /*********************************/

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

        if (!isEmptyBase(piece)) return;

        for (int i = 0; i < piece.transform.childCount; i++)
        {
            var child = piece.transform.GetChild(i);
            var piecePos = convertToVector2Int(child.transform.position);
            var _base = baseBlock[piecePos.x, piecePos.y];
            var baseSprite = _base.GetComponent<SpriteRenderer>();
            var pieceSprite = child.GetComponent<SpriteRenderer>();

            baseSprite.sprite = pieceSprite.sprite;
            _base.transform.localScale = Vector3.one;
        }


    }

    public bool isEmptyBase(GameObject piece)
    {
        for (int i = 0; i < piece.transform.childCount; i++)
        {
            var child = piece.transform.GetChild(i);

            if (!inRange(child.transform.position))
            {
                return false;
            }

        }

        return true;
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
    }

}
