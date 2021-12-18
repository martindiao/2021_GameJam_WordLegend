using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject Player;
    public List<GameObject> NPC;    //预置NPC
    public GameObject Bag;
    public GameObject TalkRegion;

    public bool isInteraction;
    //游戏阶段:1修桥前 2出山洞前 3矮死之前 4结局前
    public int GameStep;

    public int PangBaiIndex;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        GameStep = 1;
        PangBaiIndex = 0;
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
    }

    private void FixedUpdate()
    {
        
    }

    private void SetTalkState(bool state)
    {
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

    public void NextPangBai()
    {
        PangBaiIndex += 1;
    }
}
