using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Transform originalParent;

    public UnityEvent rightClick;

    public Inventory thisInventory; //背包
    public BagItem slotItem;



    private void Start()
    {
        rightClick.AddListener(new UnityAction(ButtonRightClick));
    }

    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.parent.parent);   //将拖拽物设为父级的上级
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //拖拽中
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    //结束拖拽
    //1.如果射线检测有物体存在,则destroy这两个物品,结合
    //2.如果没有物体存在,则直接放置到此位置
    public void OnEndDrag(PointerEventData eventData)
    {
        //有物品存在
        if (eventData.pointerCurrentRaycast.gameObject.name == "Item Image")
        {
            return;
        }

        //直接监测到空slot
        transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
        transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && slotItem.splitItem1 != null)
            rightClick.Invoke();

    }

    //右键点击分解
    private void ButtonRightClick()
    {
        Debug.Log("Button Right Click");
        if (!thisInventory.items.Contains(slotItem.splitItem1))
        {
            for (int i = 0; i < thisInventory.items.Count; i++)
            {
                if (thisInventory.items[i] == null)
                {
                    thisInventory.items[i] = slotItem.splitItem1;
                    break;
                }
            }
        }
        if (!thisInventory.items.Contains(slotItem.splitItem2))
        {
            for (int i = 0; i < thisInventory.items.Count; i++)
            {
                if (thisInventory.items[i] == null)
                {
                    thisInventory.items[i] = slotItem.splitItem2;
                    break;
                }
            }
        }
        thisInventory.items.Remove(slotItem);
        thisInventory.items.Add(null);

        InventoryManager.updateItem();
    }
}
