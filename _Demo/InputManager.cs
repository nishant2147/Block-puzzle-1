using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameObject PressBlock;

    [SerializeField] GameManager gameManager;

    

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

                if (gameManager.inRange(mousePos))
                {

                    gameManager.highLight(PressBlock);

                }

            }
        }
    }

  
}