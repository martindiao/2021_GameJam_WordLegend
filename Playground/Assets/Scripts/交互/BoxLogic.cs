using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLogic : MonoBehaviour
{
    public Sprite OpenedBox;

    public GameObject She;

    bool Told;
    // Start is called before the first frame update
    void Start()
    {
        Told = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(She.GetComponent<Interaction>().DialogueIndex == 4 && !Told)
        {
            She.GetComponent<Interaction>().TextRegion.GetComponent<TalkSystem>().ChangeNPC("射", She.GetComponent<Interaction>().DialogueIndex);
            She.GetComponent<Interaction>().TextRegion.transform.position = new Vector3(She.transform.position.x, She.transform.position.y + 35f, She.transform.position.z);
            Told = true;
        }
    }

    public void OpenBox()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = OpenedBox;
        She.GetComponent<Interaction>().DialogueIndex += 1;
    }
}
