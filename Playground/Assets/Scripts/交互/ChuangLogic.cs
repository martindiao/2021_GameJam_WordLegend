using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuangLogic : MonoBehaviour
{
    public float InterRange;    //交互范围

    public GameObject NoteE;   //创建出的E键
    private Renderer ShowOrNot; //E键的显示
    public GameObject She;
    public GameObject Chai;

    private GameObject Player;
    private float Distance; //和玩家的距离

    bool told2;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        ShowOrNot = NoteE.GetComponent<Renderer>();
        told2 = false;
        Chai.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("EleParent").GetComponent<GameManager>().isInteraction)
        {
            if (She.GetComponent<Interaction>().DialogueIndex == 2)
            {
                CheckDistanceBetweenPlayer();
            }
        }
        if (!gameObject.GetComponent<Renderer>().enabled)
            ShowOrNot.enabled = false;

        if(!told2&& She.GetComponent<Interaction>().DialogueIndex == 1)
        {
            She.GetComponent<Interaction>().TextRegion.GetComponent<TalkSystem>().ChangeNPC("射", She.GetComponent<Interaction>().DialogueIndex);
            She.GetComponent<Interaction>().TextRegion.transform.position = new Vector3(She.transform.position.x, She.transform.position.y + 35f, She.transform.position.z);
            told2 = true;
        }

        if (told2 && !She.GetComponent<Interaction>().TextRegion.activeSelf && Chai != null)
        {
            Chai.SetActive(true);
        }
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
                Player.GetComponent<PlayerLogic>().enabled = false;
                GameObject.FindGameObjectWithTag("EleParent").GetComponent<GameManager>().switchPosition(
                    GameObject.FindGameObjectWithTag("EleParent").GetComponent<GameManager>().OutCavePot);
                NoteE.SetActive(false);
            }
        }
    }


    public void FixChuangHu()
    {
        gameObject.GetComponent<Renderer>().enabled = true;
        She.GetComponent<Interaction>().DialogueIndex += 1;
    }
}
