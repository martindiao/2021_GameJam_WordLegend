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

    public GameObject HeiMu;    //黑幕


    public bool isInteraction;
    //游戏阶段:1修桥前 2出山洞前 3矮死之前 4结局前
    public int GameStep;


    private bool IfLiao = false;
    private bool IfJiao = false;

    private GameObject targetPot;

    float HeiMuTimer = 0f;
    bool switched;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.position = InitialPot.transform.position;
        GameStep = 1;
        HeiMu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    private void FixedUpdate()
    {
        if (HeiMu.activeSelf)
        {
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
                GameStep += 1;
                Player.GetComponent<PlayerLogic>().enabled = true;
            }
        }
    }

    private void SetTalkState(bool state)
    {
        if (!state)
        {
            Player.GetComponent<Animator>().SetBool("isMoving", false);
        }
        Player.GetComponent<PlayerLogic>().enabled = state;
        foreach(var npc in NPC)
        {
            npc.GetComponent<Interaction>().enabled = state;
        }
    }

    private void SetPickItemState(bool state)
    {
        Player.GetComponent<PlayerLogic>().enabled = state;
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
    }


}
