using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuangLogic : MonoBehaviour
{
    public float InterRange;    //交互范围

    public GameObject NoteE;   //创建出的E键
    private Renderer ShowOrNot; //E键的显示

    private GameObject Player;
    private float Distance; //和玩家的距离
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        ShowOrNot = NoteE.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("EleParent").GetComponent<GameManager>().isInteraction)
        {
            if (gameObject.GetComponent<Renderer>().enabled)
            {
                CheckDistanceBetweenPlayer();
            }
        }
        if (!gameObject.GetComponent<Renderer>().enabled)
            ShowOrNot.enabled = false;
    }

    private void FixedUpdate()
    {
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
                if (true)
                    return;
                NoteE.SetActive(false);
                GameObject.FindGameObjectWithTag("EleParent").GetComponent<GameManager>().isInteraction = true;
            }
        }
    }


    public void FixChuangHu()
    {
        gameObject.GetComponent<Renderer>().enabled = true;
    }
}
