using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;


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
        if (eventData.pointerCurrentRaycast.gameObject.name=="Item Image")
        {
            return;
        }

        //直接监测到空slot
        transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
        transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;    }
}
