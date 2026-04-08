using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class TilebaseaKhanh : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public List<TilebaseaKhanh> TileBase;


    public int id;
    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        ResetColor();
    }
    //[Button]
    //public void Test()
    //{
    //    Debug.Log("Test");
    //}
    public void ResetColor()
    {
        Debug.Log("Reset");
        foreach (TilebaseaKhanh tile in TileBase)
        {
            Debug.Log("foreach");
            if (tile.SpriteRenderer != null)
            {
                Debug.Log("AA");
                tile.SpriteRenderer.color = Color.white;
            }
        }
    }
    public void SetColor()
    {

    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        int currentLayerIndex = gameObject.layer;
        int collisionLayerIndex = collision.gameObject.layer;

        if(currentLayerIndex > collisionLayerIndex)
        {

            TilebaseaKhanh tile = collision.gameObject.GetComponent<TilebaseaKhanh>();
            if(tile != null) { 
                TileBase.Add(tile);
            }
        }    
    }
}
