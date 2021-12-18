using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuoBaLogic : MonoBehaviour
{
    public GameObject HuoYan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightIt()
    {
        Debug.Log("点亮火把");
        HuoYan.GetComponent<SpriteRenderer>().enabled = true;
    }
}
