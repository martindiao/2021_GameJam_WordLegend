using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItem : MonoBehaviour
{
    public BagItem thisItem;    //这个东西对应的背包物品
    public Inventory thisInventory; //背包

    public GameObject PreNoteE; //接近NPC时显示的E键素材

    private GameObject NoteE;   //创建出的E键
    private Renderer ShowOrNot; //E键的显示
    private GameObject EleParent;   //新生成的object需要是this的子object

    private GameObject BagPanel;    //背包的实例
    // Start is called before the first frame update
    void Start()
    {
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
        if(ShowOrNot.enabled == true)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                AddNewItem();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ShowOrNot.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ShowOrNot.enabled = false;
        }
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
}
