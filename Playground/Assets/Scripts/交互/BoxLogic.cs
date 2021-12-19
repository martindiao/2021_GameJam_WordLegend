using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLogic : MonoBehaviour
{
    public Sprite OpenedBox;

    public GameObject She;

    bool Told;
    bool hasHanxuan;
    bool hasChased;
    bool hasLibie;
    // Start is called before the first frame update
    void Start()
    {
        Told = false;
        hasHanxuan = false;
        hasChased = false;
        hasLibie = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(She.GetComponent<Interaction>().DialogueIndex == 4 && !Told)
        {
            She.GetComponent<Interaction>().TextRegion.GetComponent<TalkSystem>().ChangeNPC("射", She.GetComponent<Interaction>().DialogueIndex);
            She.GetComponent<Interaction>().TextRegion.transform.position = new Vector3(She.transform.position.x, She.transform.position.y + 35f, She.transform.position.z);
            She.GetComponent<Interaction>().DialogueIndex += 1;
        }
        if (She.GetComponent<Interaction>().DialogueIndex == 5 && !She.GetComponent<Interaction>().TextRegion.activeSelf)
        {
            Told = true;
        }
        if (She.GetComponent<Interaction>().DialogueIndex == 5 && !hasHanxuan && Told)
        {
            She.GetComponent<Interaction>().TextRegion.GetComponent<TalkSystem>().ChangeNPC("射", She.GetComponent<Interaction>().DialogueIndex);
            She.GetComponent<Interaction>().TextRegion.transform.position = new Vector3(She.transform.position.x, She.transform.position.y + 35f, She.transform.position.z);
            She.GetComponent<Interaction>().DialogueIndex += 1;
        }
        if (She.GetComponent<Interaction>().DialogueIndex == 6 && !She.GetComponent<Interaction>().TextRegion.activeSelf)
        {
            hasHanxuan = true;
        }
        if (hasHanxuan && !hasChased)
        {
            GameObject.FindGameObjectWithTag("EleParent").GetComponent<GameManager>().ZhuiBing();
            hasChased = true;
        }
        if(hasChased && !hasLibie)
        {
            GameObject.FindGameObjectWithTag("Ai").GetComponent<Interaction>().TextRegion.GetComponent<TalkSystem>().
                ChangeNPC("矮", GameObject.FindGameObjectWithTag("Ai").GetComponent<Interaction>().DialogueIndex);
            GameObject.FindGameObjectWithTag("Ai").GetComponent<Interaction>().TextRegion.transform.position =
                new Vector3(GameObject.FindGameObjectWithTag("Ai").transform.position.x, GameObject.FindGameObjectWithTag("Ai").transform.position.y + 35f, GameObject.FindGameObjectWithTag("Ai").transform.position.z);
        }
    }

    public void OpenBox()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = OpenedBox;
        She.GetComponent<Interaction>().DialogueIndex += 1;
    }
}
