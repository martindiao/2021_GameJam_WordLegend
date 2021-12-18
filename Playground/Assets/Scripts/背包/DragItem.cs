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

    private GameObject Hit3DItem;



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
        //监测是否拖拽出了背包
        if(eventData.pointerCurrentRaycast.gameObject.name=="BagPanel"|| eventData.pointerCurrentRaycast.gameObject.name == "背包界面")
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Hit3DItem = hit.collider.gameObject;
                Debug.Log(Hit3DItem.name);
            }
            else
            {
                Hit3DItem = null;
            }
            
        }
    }

    //结束拖拽
    //1.如果射线检测有物体存在,则destroy这两个物品,结合
    //2.如果没有物体存在,则直接放置到此位置
    //3.
    public void OnEndDrag(PointerEventData eventData)
    {
        //如果监测到有物品存在
        if (eventData.pointerCurrentRaycast.gameObject.name == "Item")
        {
            
            //如果可以合体
            //射线检测到的item在拖拽的item的union列表中
            if (slotItem.unionItem.Contains(eventData.pointerCurrentRaycast.gameObject.GetComponent<DragItem>().slotItem))
            {
                //获取组合结果在结果列表中的index
                int index = slotItem.unionItem.IndexOf(eventData.pointerCurrentRaycast.gameObject.GetComponent<DragItem>().slotItem);
                //将射线检测的item和拖拽item移出背包
                thisInventory.items.Remove(eventData.pointerCurrentRaycast.gameObject.GetComponent<DragItem>().slotItem);
                //thisInventory.items.Remove(slotItem);

                Debug.Log(slotItem.targetItem[index]);
                //slotItem = slotItem.targetItem[index];
                thisInventory.items[thisInventory.items.IndexOf(slotItem)] = slotItem.targetItem[index];
                //补上一个空slot
                thisInventory.items.Add(null);
                InventoryManager.updateItem();
                return;
            }
            //如果不能合体,回到原来的位置
            else
            {
                Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
                transform.SetParent(originalParent.transform);
                transform.position = originalParent.transform.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }

        }
        
        //如果直接监测到空slot
        if (eventData.pointerCurrentRaycast.gameObject.tag == "Slot")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }

        //如果监测将料拖给了矮
        if (slotItem.itemName == "料" && Hit3DItem.name == "矮")
        {
            Debug.Log("给料");

            //下一段对话
            Hit3DItem.gameObject.GetComponent<Interaction>().DialogueIndex += 1;
            //Destory
            thisInventory.items.Remove(slotItem);
            //Add new empty Slot
            thisInventory.items.Add(null);
            //Update bag
            InventoryManager.updateItem();
            Destroy(this);
            return;

        }

        //如果检测到的是桥并且这个物品是“桥”字
        if (slotItem.itemName == "桥" && Hit3DItem != null && (Hit3DItem.name == "桥" || Hit3DItem.name == "断桥"))
        {
            Debug.Log("Fix");

            //Fix the Brige
            Hit3DItem.GetComponent<BrigeLogic>().FixBrige();
            //Destory 'Qiao'
            thisInventory.items.Remove(slotItem);
            //Add new empty Slot
            thisInventory.items.Add(null);
            //Update bag
            InventoryManager.updateItem();
            Destroy(this);
            return;
        }

        //如果检测到的是火把并且这个物品是“火”字
        if (slotItem.itemName == "火" && (Hit3DItem != null && Hit3DItem.name == "火把"))
        {
            Debug.Log("点燃");

            //Light the HuoBa
            Hit3DItem.GetComponent<HuoBaLogic>().LightIt();
            //Destory
            thisInventory.items.Remove(slotItem);
            //Add new empty Slot
            thisInventory.items.Add(null);
            //Update bag
            InventoryManager.updateItem();
            Destroy(this);
            return;

        }

        //如果检测到的是窗并且这个物品是“户”字
        if (slotItem.itemName == "户" && (Hit3DItem != null && Hit3DItem.name == "窗"))
        {
            Debug.Log("窗户");

            //Fix ChuangHu
            Hit3DItem.GetComponent<ChuangLogic>().FixChuangHu();
            //Destory
            thisInventory.items.Remove(slotItem);
            //Add new empty Slot
            thisInventory.items.Add(null);
            //Update bag
            InventoryManager.updateItem();
            Destroy(this);
            return;

        }

        //如果监测不是slot或item
        transform.SetParent(originalParent.transform);
        transform.position = originalParent.transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        foreach(var i in GameObject.FindGameObjectsWithTag("Bag"))
        {
            i.GetComponent<CanvasGroup>().enabled = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && slotItem.splitItem1 != null)
            rightClick.Invoke();
    }

    //右键点击分解
    private void ButtonRightClick()
    {
        Debug.Log("右键分解");
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
