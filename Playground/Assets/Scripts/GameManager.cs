using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject Player;

    public GameObject TalkRegion;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //对话框激活时,关闭玩家脚本
        SetPlayerState(!TalkRegion.activeSelf);
    }

    
    private void SetPlayerState(bool state)
    {
        Player.GetComponent<PlayerLogic>().enabled = state;
    }
}
