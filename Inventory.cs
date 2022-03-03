using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private int slotCnt;
    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;
    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;
    public List<Item> items = new List<Item>();
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }
    void Start()
    {
        SlotCnt = 4;   
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public bool AddItem(Item _item)
    {
        if(items.Count < SlotCnt)
        {
            items.Add(_item);
            if(onChangeItem != null)
                onChangeItem.Invoke();
            return true;
        }
        return false;
    }
    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FieldItem"))
        {
            FIeldItem fieldItem = collision.GetComponent<FIeldItem>();
            if (AddItem(fieldItem.GetItem())){
                fieldItem.DestroyItem();
            }
        }
    }
}
