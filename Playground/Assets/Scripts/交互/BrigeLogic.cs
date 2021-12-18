using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigeLogic : MonoBehaviour
{
    public GameObject Qiao;
    // Start is called before the first frame update
    void Start()
    {
        Qiao.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixBrige()
    {
        Debug.Log("修桥");
        gameObject.GetComponent<BoxCollider>().enabled = false;
        Qiao.GetComponent<BoxCollider2D>().enabled = false;
        Qiao.GetComponent<Renderer>().enabled = true;
    }
}
