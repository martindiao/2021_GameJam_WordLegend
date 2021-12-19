using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeiDianBug : MonoBehaviour
{
    bool actived = false;
    // Start is called before the first frame update
    void Start()
    {
        //if (gameObject.name == "矢")
        //    gameObject.GetComponent<InteractiveItem>().NoteE.SetActive(false);
        //if (gameObject.name == "牧")
        //    gameObject.GetComponent<Interaction>().NoteE.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (!actived && gameObject.activeSelf)
        {
            if (gameObject.name == "矢")
                gameObject.GetComponent<InteractiveItem>().NoteE.SetActive(true);
            if (gameObject.name == "牧")
                gameObject.GetComponent<Interaction>().NoteE.SetActive(true);
            actived = true;
        }

    }
}
