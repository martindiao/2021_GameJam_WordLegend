using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject Player;
    public List<GameObject> NPC;
    public GameObject Bag;

    public GameObject TalkRegion;

    public bool isInteraction;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

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
}
