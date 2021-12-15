using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public BagItem slotItem;
    public GameObject slotImage;
    public Inventory thisInventory; //背包

    public UnityEvent rightClick;
    public GameObject ItemInSlot;


    private void Start()
    {
        rightClick.AddListener(new UnityAction(ButtonRightClick));
    }

    private void Update()
    {
        
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

    //左键点击Log输出图片名称
    public void itemOnClick()
    {
        Debug.Log(slotItem.itemImage.name);
        //InventoryManager.UpdateItemInfo(slotItem.itemInfo);
    }

    public void SetUpSlot(BagItem bagitem)
    {
        if(bagitem == null)
        {
            ItemInSlot.SetActive(false);
            return;
        }

        Debug.Log(bagitem.name);
        Debug.Log(slotImage.name);
        slotImage.GetComponent<Image>().sprite = bagitem.itemImage;
    }
}
