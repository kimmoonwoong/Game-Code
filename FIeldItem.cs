using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIeldItem : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;

    public void SetItem(Item _itme)
    {
        item.itemname = _itme.itemname;
        item.itemImage = _itme.itemImage;
        item.itemType = _itme.itemType;
        item.efts = _itme.efts;
        image.sprite = item.itemImage;
    }

    public Item GetItem()
    {
        return item;
    }
    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
