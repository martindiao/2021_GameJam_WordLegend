using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItem : MonoBehaviour
{
    public BagItem thisItem;    //这个东西对应的背包物品
    public Inventory thisInventory; //背包
    public GameObject PreNoteE; //接近NPC时显示的E键素材
    public float InterRange;    //交互范围

    private GameObject NoteE;   //创建出的E键
    private Renderer ShowOrNot; //E键的显示
    private GameObject EleParent;   //新生成的object需要是this的子object

    private GameObject BagPanel;    //背包的实例
    private GameObject Player;
    private float Distance; //和玩家的距离
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        EleParent = GameObject.FindGameObjectWithTag("EleParent");
        NoteE = Instantiate(PreNoteE);
        NoteE.transform.SetParent(EleParent.transform);
        NoteE.transform.position = transform.position;
        ShowOrNot = NoteE.GetComponent<Renderer>();

        BagPanel = GameObject.FindGameObjectWithTag("Bag");
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceBetweenPlayer();
    }

    private void AddNewItem()
    {
        Debug.Log(thisInventory.name);
        if (!thisInventory.items.Contains(thisItem))
        {
            for (int i = 0; i < thisInventory.items.Count; i++)
            {
                if (thisInventory.items[i] == null)
                {
                    thisInventory.items[i] = thisItem;
                    break;
                }
            }
            if (!BagPanel.GetComponent<InventoryManager>().isShow)
            {
                BagPanel.GetComponent<InventoryManager>().ShowUp();
            }
        }
        InventoryManager.updateItem();
    }

    private void CheckDistanceBetweenPlayer()
    {
        Distance = Vector2.Distance(transform.position, Player.transform.position);
        if (Distance <= InterRange && !ShowOrNot.enabled)
        {
            ShowOrNot.enabled = true;
        }
        if (Distance > InterRange && ShowOrNot.enabled)
        {
            ShowOrNot.enabled = false;
        }

        //按R键对话
        if (Distance <= InterRange)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                AddNewItem();
                Destroy(gameObject);
            }
        }
    }
}
