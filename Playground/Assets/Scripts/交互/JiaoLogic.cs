using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiaoLogic : MonoBehaviour
{
    public GameObject gamemanager;
    private bool picked;

    // Start is called before the first frame update
    void Start()
    {
        picked = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!gameObject.GetComponent<Renderer>().enabled && !picked)
        {
            picked = true;
        }
    }
}
