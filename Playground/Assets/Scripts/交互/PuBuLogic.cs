using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuBuLogic : MonoBehaviour
{
    public GameObject Shi;

    // Start is called before the first frame update
    void Start()
    {
        Shi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.FindGameObjectWithTag("She").GetComponent<Interaction>().DialogueIndex == 6)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<PlayerLogic>().enabled = false;
                GameObject.FindGameObjectWithTag("EleParent").GetComponent<GameManager>().switchPosition(
                    GameObject.FindGameObjectWithTag("EleParent").GetComponent<GameManager>().WaterPot);
                Shi.SetActive(true);
            }
        }
        
    }
}
