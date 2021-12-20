using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject Player;
    public List<GameObject> NPC;    //预置NPC
    public GameObject Bag;
    public GameObject TalkRegion;

    public GameObject InitialPot;
    public GameObject CavePot;
    public GameObject OutCavePot;
    public GameObject WaterPot;

    public GameObject ShePot1;
    public GameObject ShePot2;

    public GameObject AiPot;

    public GameObject HeiMu;    //黑幕
    public GameObject TuiChu;

    public GameObject KaiChang;
    public Sprite Zhuibing;

    public GameObject Jiewei;
    public Sprite Zhuan;
    public GameObject Ren;
    public Sprite She;

    public AudioClip JueDou;
    public AudioClip Feng;
    public AudioSource Zhuyinyue;


    bool started;
    float kaichangTimer;


    public bool isInteraction;
    //游戏阶段:1修桥前 2出山洞前 3矮死之前 4结局前
    public int GameStep;


    private bool IfLiao = false;
    private bool IfJiao = false;

    private GameObject targetPot;

    float Kaishi = 0f;
    float HeiMuTimer = 0f;
    float HuangHuTimer = 0f;
    float WanJieTimer = 0f;
    float XuanyunTimer = 0f;

    int xunyunCounter = 0;

    bool switched;
    private List<GameObject> Baobiao = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.position = InitialPot.transform.position;
        GameStep = 0;
        HeiMu.SetActive(false);
        KaiChang.SetActive(true);
        started = false;

        NPC[2].SetActive(false);
        
        foreach (var baobiao in GameObject.FindGameObjectsWithTag("baobiao"))
        {
            Debug.Log(baobiao.name);
            Baobiao.Add(baobiao);
            baobiao.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!started && Input.GetKeyDown(KeyCode.Space))
        {
            KaiChang.GetComponent<Animator>().Play("kaishi");
            //KaiChang.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            //kaichangTimer = 0;
            started = true;
            GameStep+=1;
        }
        if (started)
        {
            Kaishi += Time.deltaTime;
            if (Kaishi >= 10.5f)
                KaiChang.SetActive(false);
        }
        //if (started && KaiChang.activeSelf)
        //{
        //    if (kaichangTimer < 2.0f)
        //    {
        //        kaichangTimer += Time.deltaTime;
        //        //KaiChang.GetComponent<Image>().color = new Color(1, 1, 1, 1-kaichangTimer/2.0f);
        //    }
        //    if (kaichangTimer >= 2.0f)
        //    {
        //        KaiChang.SetActive(false);
        //        kaichangTimer = 0;
        //    }  
        //}

        if (GameStep != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape)&& Time.timeScale != 0)
            {
                TuiChu.SetActive(true);
                Time.timeScale = 0;
                
            }
            else if (Time.timeScale == 0)
            {
                Debug.Log(Time.timeScale);
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log("取消");
                    TuiChu.SetActive(false);
                    Time.timeScale = 1;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Application.Quit();
                }
            }
        }

        if (GameStep == 5)
        {
            Player.GetComponent<PlayerLogic>().enabled = false;
            return;
        }

        if (GameStep == 6)
        {
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Player.GetComponent<PlayerLogic>().enabled = false;
            Player.GetComponent<Animator>().enabled = false;
            Jiewei.SetActive(true);
            if (!Jiewei.GetComponent<Jiewei>().jiewei)
            {
                Jiewei.GetComponent<Jiewei>().jiewei = true;
                Destroy(GameObject.FindGameObjectWithTag("TA"));
                NPC[2].GetComponent<SpriteRenderer>().sprite = Zhuan;
                NPC[2].transform.localScale = new Vector3(2.5f, 2.5f, 1);
                foreach(var xiaobing in Baobiao)
                {
                    Destroy(xiaobing);
                }
                Player.GetComponent<SpriteRenderer>().enabled = false;
                Ren.SetActive(true);

                NPC[1].GetComponent<SpriteRenderer>().sprite = She;

                Bag.SetActive(false);
            }

            if(!Jiewei.GetComponent<Jiewei>().Wanjie && Jiewei.GetComponent<Jiewei>().jiewei && Jiewei.GetComponent<Image>().color == new Color(1, 1, 1, 0))
            {
                WanJieTimer += Time.deltaTime;
                if (WanJieTimer >= 4.0f)
                {
                    Jiewei.GetComponent<Jiewei>().Wanjie = true;
                    Camera.main.GetComponent<CameraRotation>().ended = true;
                }
            }
            return;
        }

        //对话框激活时,关闭玩家脚本
        SetTalkState(!TalkRegion.activeSelf);
        if (!TalkRegion.activeSelf)
        {
            SetPickItemState(!isInteraction);
        }

        if (!IfLiao && Liao())
        {
            IfLiao = true;
            TalkRegion.GetComponent<TalkSystem>().ChangeNPC("主角旁白", TalkRegion.GetComponent<TalkSystem>().PangBai.FindIndex(item => item.name.Equals("mi3")));
            TalkRegion.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 35f, Player.transform.position.z);
        }

        if (JiaoZi() && !IfJiao)
        {
            IfJiao = true;
            TalkRegion.GetComponent<TalkSystem>().ChangeNPC("主角旁白", TalkRegion.GetComponent<TalkSystem>().PangBai.FindIndex(item => item.name.Equals("Jiao")));
            TalkRegion.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 35f, Player.transform.position.z);
        }

        if (GameStep == 3 && NPC[1].transform.position != ShePot1.transform.position)
        {
            NPC[1].transform.position = ShePot1.transform.position;
            NPC[1].GetComponent<Interaction>().DialogueIndex += 1;
        }

        if (GameStep == 3 && NPC[1].GetComponent<Interaction>().DialogueIndex==5 && NPC[0].GetComponent<Interaction>().DialogueIndex == 1)
        {
            NPC[0].transform.position = AiPot.transform.position;
            NPC[0].GetComponent<Interaction>().DialogueIndex += 1;
        }
        

        if (GameStep == 4 && !Zhuyinyue.isPlaying)
        {
            Zhuyinyue.clip = Feng;
            Zhuyinyue.Play();
            NPC[2].SetActive(true);
            foreach(var baobiao in Baobiao)
            {
                Debug.Log(baobiao.name);
                baobiao.SetActive(true);
            }
            NPC[1].transform.position = ShePot2.transform.position;
            GameObject.FindGameObjectWithTag("Box").GetComponent<BoxLogic>().JiaMu.SetActive(false);
            NPC[0].SetActive(false);
        }

        //Xuanyun
        if(GameStep == 4 && XuanyunTimer <= 3.0f)
        {
            isInteraction = true;
            XuanyunTimer += Time.deltaTime;
        }
        if(isInteraction && XuanyunTimer >= 3.0f)
        {
            isInteraction = false;
        }

        //XuanYunheiping
        if(GameStep == 4)
        {
            if (xunyunCounter < 3)
            {
                if (HuangHuTimer >= 1.5f)
                {
                    HeiMu.SetActive(true);
                    HuangHuTimer = 0f;
                    xunyunCounter += 1;
                }
                HuangHuTimer += Time.deltaTime;
            }
            
        }

    }

    private void FixedUpdate()
    {
        if (HeiMu.activeSelf)
        {
            if (GameStep == 4)
            {
                HeiMuTimer += Time.fixedDeltaTime;
                if (HeiMuTimer < 0.5f)
                    HeiMu.GetComponent<Image>().color = new Color(0, 0, 0, HeiMuTimer);
                if (HeiMuTimer > 0.5f)
                {
                    HeiMu.GetComponent<Image>().color = new Color(0, 0, 0, 1.0f - HeiMuTimer);
                }
                if (HeiMuTimer >= 1.0f)
                {
                    HeiMu.SetActive(false);
                }
                return;
            }

            HeiMuTimer += Time.fixedDeltaTime;
            if (HeiMuTimer < 1.0f)
                HeiMu.GetComponent<Image>().color = new Color(0, 0, 0, HeiMuTimer);
            if (HeiMuTimer > 1.0f)
            {
                if (!switched)
                {
                    Debug.Log(HeiMuTimer);
                    Player.transform.position = targetPot.transform.position;
                    switched = true;
                }
                HeiMu.GetComponent<Image>().color = new Color(0, 0, 0, 2.0f - HeiMuTimer);
            }
                
            if (HeiMuTimer >= 2.0f)
            {
                HeiMu.SetActive(false);
                HeiMuTimer = 0f;
                if (GameStep != 4)
                    GameStep += 1;
                Player.GetComponent<PlayerLogic>().enabled = true;
                isInteraction = false;
            }
        }

        
    }

    private void SetTalkState(bool state)
    {
        if (!state)
        {
            Player.GetComponent<Animator>().SetBool("isMoving", false);
        }
        Player.GetComponent<Animator>().enabled = state;
        Player.GetComponent<PlayerLogic>().enabled = state;
        if (!state)
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        foreach(var npc in NPC)
        {
            npc.GetComponent<Interaction>().enabled = state;
        }
    }

    private void SetPickItemState(bool state)
    {
        if (!state)
        {
            Player.GetComponent<Animator>().SetBool("isMoving", false);
        }
        Player.GetComponent<Animator>().enabled = state;
        Player.GetComponent<PlayerLogic>().enabled = state;
        if (!state)
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    private bool Liao()
    {
        foreach(var item in Bag.GetComponent<InventoryManager>().Bag.items)
        {
            if (item != null && item.name == "料")
            {
                return true;
            }
        }
        return false;
    }

    private bool JiaoZi()
    {
        foreach (var item in Bag.GetComponent<InventoryManager>().Bag.items)
        {
            if (item != null && item.name == "轿")
            {
                return true;
            }
        }
        return false;
    }

    public void switchPosition(GameObject pot)
    {
        HeiMu.SetActive(true);
        switched = false;
        targetPot = pot;
        isInteraction = true;
    }

    public void TeleNPC(int index, GameObject pot)
    {

    }

}
