using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TalkSystem : MonoBehaviour
{

    public List<TextAsset> dialogs; //对话文件
    public Text textRegion; //对话框
    public List<string> talks;  //逐句对话
    public int index;   //对话索引
    public string NPC;  //正对话的NPC的tag

    private bool isTalking;
    // Start is called before the first frame update
    void Start()
    {
        isTalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && isTalking)
        {
            Debug.Log(talks.Count());
            if (index == talks.Count())
            {
                gameObject.SetActive(false);
                isTalking = false;
                return;
            }
            
            textRegion.text = talks[index];
            index++;
            Debug.Log(index);
        }
        
    }

    private void GetTalks(TextAsset textAsset)
    {
        index = 0;
        talks.Clear();

        //按行分割对话,存入列表
        var temp = textAsset.ToString().Split('\n');
        foreach (var line in temp)
        {
            talks.Add(line);
        }
    }

    public void ChangeNPC(string tag)
    {
        NPC = tag;
        isTalking = true;
        gameObject.SetActive(true);
        GetTalks(dialogs[0]);
    }
}
