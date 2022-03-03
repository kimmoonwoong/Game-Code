using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDateBase : MonoBehaviour
{
    // Start is called before the first frame update
    public static ItemDateBase instance;
    int count = 0;
    int count1 = 0;
    private void Awake()
    {
        instance = this;
    }
    public List<Item> itemDB = new List<Item>();

    public GameObject fieldItemPrefab;
    public Vector2[] pos;
    GameObject monster = null;

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    public void ReturnCount()
    {
        count = 0;
        count1 = 0;
    }
}
