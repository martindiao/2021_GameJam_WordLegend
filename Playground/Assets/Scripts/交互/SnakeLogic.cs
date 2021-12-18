using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeLogic : MonoBehaviour
{
    public GameObject gamemanager;
    public GameObject TextRegion;

    private GameObject Bag;

    // Start is called before the first frame update
    void Start()
    {
        Bag = GameObject.FindGameObjectWithTag("Bag");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CanPick()
    {
        foreach (var item in Bag.GetComponent<InventoryManager>().Bag.items)
        {
            if (item != null && item.name == "叉")
                return true;
        }

        TextRegion.GetComponent<TalkSystem>().ChangeNPC("主角旁白", TextRegion.GetComponent<TalkSystem>().PangBai.FindIndex(item => item.name.Equals("Snake")));
        TextRegion.transform.position = new Vector3(transform.position.x, transform.position.y + 35f, transform.position.z);
        return false;
    }
}
