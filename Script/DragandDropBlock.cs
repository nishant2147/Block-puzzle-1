using UnityEngine;

public class DragandDropBlock : MonoBehaviour
{
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
            if(PressBlock != null)
            {
                if(GridGenerate.instance.inRange(PressBlock.transform.position))
                {
                    print("InRange====>");

                    if(GridGenerate.instance.isEmpty(PressBlock))
                    {
                        GridGenerate.instance.PlacesBlock(PressBlock);
                        PressBlock = null;
                        return;
                    }
                }
                PressBlock.moveToOriginalPosition();
            }
            PressBlock = null;
        }

        if(Input.GetMouseButton(0))
        {
            if(PressBlock != null)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                PressBlock.transform.position = mousePos;
            }
        }
    }
}
