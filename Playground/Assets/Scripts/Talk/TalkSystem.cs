using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TalkSystem : MonoBehaviour
{
    public List<TextAsset> PangBai;
    public List<TextAsset> Ai; //对话文件
    public List<TextAsset> She;
    public List<TextAsset> Mu;
    public List<TextAsset> XiaoBing;

    public Text textRegion; //对话框
    public List<string> talks;  //逐句对话
    public int index;   //对话索引
    public string person;  //正对话的person的Name

    private bool isTalking;
    // Start is called before the first frame update
    void Start()
    {
        isTalking = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Fire1")||Input.GetKeyDown(KeyCode.Space)) && isTalking)
        {
            CoutDialogs();
        }
        
    }

    //将剧本提取到列表talks
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

    //根据讲话NPC名字选择对话剧本
    public void ChangeNPC(string tag, int DialogueIndex)
    {
        person = tag;
        isTalking = true;
        gameObject.SetActive(true);
        switch (person)
        {
            case "主角旁白":
                {
                    GetTalks(PangBai[DialogueIndex]);
                    break;
                }
            case "矮":
                {
                    GetTalks(Ai[DialogueIndex]);
                    break;
                }
            case "射":
                {
                    GetTalks(She[DialogueIndex]);
                    break;
                }
                
            case "牧":
                {
                    GetTalks(Mu[DialogueIndex]);
                    break;
                }
                
            case "兵":
                {
                    GetTalks(XiaoBing[DialogueIndex]);
                    break;
                }
                
        }
        CoutDialogs();
    }

    //逐句输出对话
    private void CoutDialogs()
    {
        Debug.Log(talks.Count());
        if (index == talks.Count())
        {
            textRegion.text = "";
            gameObject.SetActive(false);
            isTalking = false;
            return;
        }

        textRegion.text = talks[index];
        index++;
        Debug.Log(index);
    }
}
