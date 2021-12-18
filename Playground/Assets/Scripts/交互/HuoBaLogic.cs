using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuoBaLogic : MonoBehaviour
{
    public GameObject HuoYan;
    public GameObject HeiWu;
    // Start is called before the first frame update
    void Start()
    {
        HeiWu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightIt()
    {
        Debug.Log("点亮火把");
        HuoYan.GetComponent<SpriteRenderer>().enabled = true;
        HeiWu.GetComponent<Animator>().Play("Fade");
        HeiWu.GetComponent<Collider2D>().enabled = false;
    }
}
