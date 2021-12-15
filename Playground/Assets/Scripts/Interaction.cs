using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NPC 的互动脚本

public class Interaction : MonoBehaviour
{
    public GameObject PreNoteE; //接近NPC时显示的E键素材
    public GameObject TextRegion;   //对话框
    public string NPCName;  //NPC的名称

    private GameObject NoteE;   //创建出的E键
    private Renderer ShowOrNot; //E键的显示

    private float Distance;

    private GameObject player;

    private GameObject EleParent;   //新生成的object需要是this的子object

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EleParent = GameObject.FindGameObjectWithTag("EleParent");
        NoteE = Instantiate(PreNoteE);
        NoteE.transform.SetParent(EleParent.transform);
        NoteE.transform.position = new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z);
        ShowOrNot = NoteE.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector2.Distance(transform.position, player.transform.position);
        if(Distance <= 1.0f && !ShowOrNot.enabled)
        {
            ShowOrNot.enabled = true;
        }
        if(Distance > 1.0f && ShowOrNot.enabled)
        {
            ShowOrNot.enabled = false;
        }

        //按R键对话
        if (Distance <= 1.0f)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                TextRegion.GetComponent<TalkSystem>().ChangeNPC(NPCName);
                TextRegion.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
            }
        }
    }
}
