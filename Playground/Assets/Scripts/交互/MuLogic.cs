using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuLogic : MonoBehaviour
{
    public GameObject TextRegion;

    bool plued = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.GetComponent<PlayerLogic>().enabled = false;
            TextRegion.GetComponent<TalkSystem>().ChangeNPC("牧", 0);

            TextRegion.transform.position = new Vector3(transform.position.x, transform.position.y + 35f, transform.position.z);
            if(!plued)
                GameObject.FindGameObjectWithTag("EleParent").GetComponent<GameManager>().GameStep = 5;
            plued = true;
        }
    }
}
