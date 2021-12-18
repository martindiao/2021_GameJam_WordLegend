using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiLogic : MonoBehaviour
{
    public GameObject gamemanager;
    public GameObject TextRegion;

    private GameObject Bag;

    private bool picked;
    // Start is called before the first frame update
    void Start()
    {
        Bag = GameObject.FindGameObjectWithTag("Bag");
        picked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<Renderer>().enabled && !picked)
        {
            picked = true;
            TextRegion.GetComponent<TalkSystem>().ChangeNPC("主角旁白", TextRegion.GetComponent<TalkSystem>().PangBai.FindIndex(item => item.name.Equals("mi2")));
            TextRegion.transform.position = new Vector3(transform.position.x, transform.position.y + 35f, transform.position.z);
        }
    }

    public bool CanPick()
    {
        foreach (var item in Bag.GetComponent<InventoryManager>().Bag.items)
        {
            if (item != null && item.name == "斗")
                return true;
        }

        TextRegion.GetComponent<TalkSystem>().ChangeNPC("主角旁白", TextRegion.GetComponent<TalkSystem>().PangBai.FindIndex(item => item.name.Equals("mi1")));
        TextRegion.transform.position = new Vector3(transform.position.x, transform.position.y + 35f, transform.position.z);
        return false;
    }
}
