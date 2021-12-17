using UnityEngine;

public class InteractiveItem : MonoBehaviour
{
    public BagItem thisItem;    //这个东西对应的背包物品
    public Inventory thisInventory; //背包
    public GameObject PreNoteE; //接近NPC时显示的E键素材
    public GameObject PreMoDian; //交互时出现的过渡动画素材
    public float InterRange;    //交互范围

    private GameObject NoteE;   //创建出的E键
    private GameObject MoDian;  //创建出的墨点
    private Renderer ShowOrNot; //E键的显示
    private GameObject EleParent;   //新生成的object需要是this的子object
    private int timer;

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

        //生成淡入淡出墨点
        MoDian = Instantiate(PreMoDian);
        MoDian.transform.SetParent(EleParent.transform);
        MoDian.transform.position = transform.position;
        MoDian.transform.localScale = transform.localScale;
        

        BagPanel = GameObject.FindGameObjectWithTag("Bag");
    }

    // Update is called once per frame
    void Update()
    {
        if (!EleParent.GetComponent<GameManager>().isInteraction)
        {
            CheckDistanceBetweenPlayer();
        }
    }

    private void FixedUpdate()
    {
        IfGuoduFinish();
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
                Debug.Log("ShowUp!");
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

        //按E键交互
        if (Distance <= InterRange && Player.GetComponent<PlayerLogic>().enabled == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MoDian.GetComponent<Animator>().Play("淡入淡出");
                timer = 0;
                Debug.Log(MoDian.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0));
                NoteE.SetActive(false);
                EleParent.GetComponent<GameManager>().isInteraction = true;
            }
        }
    }
    
    private void IfGuoduFinish()
    {
        if (MoDian.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0)
        {
            timer++;
            if (timer >= 60 && NoteE.activeSelf == false)
            {
                AddNewItem();
                gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
        
        if(MoDian.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && NoteE.activeSelf == false)
        {
            Destroy(MoDian);
            EleParent.GetComponent<GameManager>().isInteraction = false;
            Destroy(gameObject);
        }
    }
}
