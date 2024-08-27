using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPieces : MonoBehaviour
{
    Vector2 startPos = Vector2.zero;
    internal bool blockPlaced = false;
    private Vector3 originalScale = new Vector3(0.5f, 0.5f, 0.5f);
    Vector3 largedScale = new Vector3(1f, 1f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    internal void moveToOriginalPosition()
    {
        transform.DOMove(startPos, 0.5f);
        transform.DOScale(originalScale, 0.5f);
        //transform.position = startPos;
        //transform.localScale = originalScale;
    }
    void OnMouseDown()
    {
        transform.DOScale(largedScale, 0.5f);
        //transform.localScale = largedScale;
    }
    void OnMouseUp()
    {
        transform.localScale = largedScale;
    }
}
