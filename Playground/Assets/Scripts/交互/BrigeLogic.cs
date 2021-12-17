using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigeLogic : MonoBehaviour
{
    public GameObject Qiao;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixBrige()
    {
        Debug.Log("修桥");
        gameObject.GetComponent<BoxCollider>().enabled = false;
        Qiao.GetComponentInChildren<BoxCollider>().enabled = false;
        Qiao.GetComponentInChildren<Renderer>().enabled = true;
    }
}
