using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public BagItem slotItem;
    public Image slotImage;
    public Inventory thisInventory; //背包

    public UnityEvent rightClick;


    private void Start()
    {
        rightClick.AddListener(new UnityAction(ButtonRightClick));
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && slotItem.splitItem != null)
            rightClick.Invoke();

    }

    //右键点击分解
    private void ButtonRightClick()
    {
        Debug.Log("Button Right Click");
        if (!thisInventory.items.Contains(slotItem.splitItem))
        {
            thisInventory.items.Add(slotItem.splitItem);
        }

        thisInventory.items.Remove(slotItem);

        InventoryManager.updateItem();
    }
    //左键点击显示Info
    public void itemOnClick()
    {
        InventoryManager.UpdateItemInfo(slotItem.itemInfo);
    }
}
