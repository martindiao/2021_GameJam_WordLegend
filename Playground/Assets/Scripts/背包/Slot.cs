using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject ItemInSlot;


    private void Start()
    {
    }

    private void Update()
    {
        
    }

    public void SetUpSlot(BagItem bagitem)
    {
        if(bagitem == null)
        {
            ItemInSlot.SetActive(false);
            return;
        }
        Debug.Log(bagitem.name);
        //Debug.Log(ItemInSlot.GetComponent<Image>().sprite.name);
        ItemInSlot.GetComponent<Text>().text = bagitem.itemName;
        ItemInSlot.GetComponent<DragItem>().slotItem = bagitem;
    }
}
