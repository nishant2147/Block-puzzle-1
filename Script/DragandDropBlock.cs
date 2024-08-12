using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class DragandDropBlock : MonoBehaviour
{
    public GridGenerate instance;
    BlockPieces PressBlock;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 newmousePos = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(newmousePos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Blocks")
                {
                    //print(hit.collider.gameObject.name);
                    PressBlock = hit.collider.gameObject.GetComponent<BlockPieces>();

                    if (PressBlock.blockPlaced)
                    {
                        PressBlock = null;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (PressBlock != null)
            {
                if (instance.inRange(PressBlock.transform.position))
                {
                    print("InRange====>");

                    if (instance.isEmpty(PressBlock))
                    {
                        instance.PlacesBlock(PressBlock);
                        PressBlock = null;
                        return;
                    }
                }
                PressBlock.moveToOriginalPosition();
            }
            PressBlock = null;
            instance.clearHighlight();
        }

        if (Input.GetMouseButton(0))
        {
            if (PressBlock != null)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                PressBlock.transform.position = mousePos;

                if (instance.inRange(mousePos))
                {
                    //Vector2Int pos = convertToVector2Int(PressBlock.transform.position);
                    instance.Highlight(PressBlock);
                    //instance.sprite = PressBlock.gameObject.GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    instance.clearHighlight();
                }
            }
        }
    }

    private Vector2Int convertToVector2Int(Vector3 pos)
    {
        return new Vector2Int((int)(pos.x + 0.5f), (int)(pos.y + 0.5f));
    }





    //**************************************    new Code ************************************//

    /*
        GameObject PressBlock;

        [SerializeField] GridGenerate gridGenerate;



        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

                if (hit.collider != null)
                {
                    PressBlock = hit.collider.gameObject;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                PressBlock = null;
            }

            if (Input.GetMouseButton(0))
            {
                if (PressBlock != null)
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0;
                    PressBlock.transform.position = mousePos;

                    if (gridGenerate.inRange(mousePos))
                    {

                        gridGenerate.highLight(PressBlock);

                    }

                }
            }
        }*/

}
