using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory /New Item")]


public class BagItem : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    //public string itemInfo;
    public BagItem splitItem1;
    public BagItem splitItem2;
    public BagItem unionItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
