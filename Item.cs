using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Equipment,
    Consumables,
    Etc
}
[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string itemname;
    public Sprite itemImage;
    public List<ItemEffect> efts;
    public bool Use()
    {
        bool isUsed = true;
        foreach(ItemEffect eft in efts)
        {
            isUsed = eft.ExecuteRole();
            return true;
        }
        return false;
    }
}
