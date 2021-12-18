using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingLogic : MonoBehaviour
{
    public GameObject TextRegion;
    private bool beenTold1;
    private bool beenTold2;

    // Start is called before the first frame update
    void Start()
    {
        beenTold1 = false;
        beenTold2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (beenTold1 && !TextRegion.activeSelf)
        {
            GameObject.FindGameObjectWithTag("EleParent").GetComponent<GameManager>().switchPosition(
                GameObject.FindGameObjectWithTag("EleParent").GetComponent<GameManager>().CavePot);
            Destroy(this);
        }

        if (GameObject.FindGameObjectWithTag("EleParent").GetComponent<GameManager>().GameStep == 2 && !beenTold2)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            TextRegion.GetComponent<TalkSystem>().ChangeNPC("兵",
                TextRegion.GetComponent<TalkSystem>().XiaoBing.FindIndex(item => item.name.Equals("JinDong")));

            TextRegion.transform.position = new Vector3(transform.position.x, transform.position.y + 35f, transform.position.z);
            beenTold2 = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            TextRegion.GetComponent<TalkSystem>().ChangeNPC("兵",
                TextRegion.GetComponent<TalkSystem>().XiaoBing.FindIndex(item => item.name.Equals("Zhua")));

            TextRegion.transform.position = new Vector3(transform.position.x, transform.position.y + 35f, transform.position.z);
            beenTold1 = true;
        }
    }


}
