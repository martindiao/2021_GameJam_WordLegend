using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NPC 的互动脚本

public class Interaction : MonoBehaviour
{
    public GameObject PreNoteE; //接近NPC时显示的E键素材
    public GameObject TextRegion;   //对话框
    public string NPCName;  //NPC的名称
    public int DialogueIndex;
    public GameObject Jiaoxue;

    private GameObject NoteE;   //创建出的E键
    private Renderer ShowOrNot; //E键的显示

    private float Distance;

    public float TalkRange;

    private GameObject player;

    private GameObject EleParent;   //新生成的object需要是this的子object

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EleParent = GameObject.FindGameObjectWithTag("EleParent");
        NoteE = Instantiate(PreNoteE);
        NoteE.transform.SetParent(EleParent.transform);
        NoteE.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        ShowOrNot = NoteE.GetComponent<Renderer>();

        if (Jiaoxue != null)
            Jiaoxue.SetActive(false);

        DialogueIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceBetweenPlayer();

        if (NoteE.transform.position != new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z))
        {
            NoteE.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        }
    }

    private void CheckDistanceBetweenPlayer()
    {
        Distance = Vector2.Distance(transform.position, player.transform.position);
        if (Distance <= TalkRange && !ShowOrNot.enabled)
        {
            if (Jiaoxue != null)
                Jiaoxue.SetActive(true);
            ShowOrNot.enabled = true;
        }
        if (Distance > TalkRange && ShowOrNot.enabled)
        {
            if (Jiaoxue != null)
                Jiaoxue.SetActive(false);
            ShowOrNot.enabled = false;
        }

        //按E键对话
        if (Distance <= TalkRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TextRegion.GetComponent<TalkSystem>().ChangeNPC(NPCName, DialogueIndex);
                TextRegion.transform.position = new Vector3(transform.position.x, transform.position.y + 35f, transform.position.z);
            }
        }
    }
}
