using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;   //单例模式,只存在一个InventoryManager

    public Inventory Bag;   //字典背包的引用
    public GameObject slotGrid; //背包栏
    public GameObject slotPrefab; //
    //public Text itemInfo;
    public List<GameObject> slots = new List<GameObject>();
    public GameObject emptySlot;

    public int SlotNum;

    public bool isShow;    //背包是否出来了

    // Start is called before the first frame update
    void Start()
    {
        instance.Bag.items.Clear();
        for(int i = 0; i < SlotNum; i++)
        {
            instance.Bag.items.Add(null);
        }
        isShow = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    //public static void CreatNewItem(BagItem item)
    //{
    //    //创建新slot,储存item
    //    GameObject newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
    //    //设置新slot的的父为背包grid
    //    newItem.gameObject.transform.SetParent(instance.slotGrid.transform);

    //    //newItem.GetComponent<BagItem> = item.;
    //    newItem.GetComponent<Image>().sprite = item.itemImage;
    //    newItem.GetComponent<Slot>().slotItem = item;

    //}

    public void OnEnable()
    {
        //instance.itemInfo.text = "";
    }

    //字解
    //public static void UpdateItemInfo(string ItemInfo)
    //{
    //    instance.itemInfo.text = "字解:\n" + ItemInfo;
    //}

    public static void updateItem()
    {
        //清空背包,重新布局
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        instance.slots.Clear();

        for (int i = 0; i < instance.Bag.items.Count; i++)
        {
            //CreatNewItem(instance.Bag.items[i]);
            instance.slots.Add(Instantiate(instance.emptySlot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);
            instance.slots[i].GetComponent<Slot>().SetUpSlot(instance.Bag.items[i]);
        }
    }

    //背包逐渐出现
    IEnumerator ShowUpProcess(float distance, float time)
    {
        float number = 60 * time;
        float StepDis = distance / number;
        for (int i = 0; i < number; i++)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + StepDis, 0);
            yield return new WaitForFixedUpdate();
        }
    }

    public void ShowUp()
    {
        isShow = true;
        StartCoroutine(ShowUpProcess(50, 1.0f));
    }
}
