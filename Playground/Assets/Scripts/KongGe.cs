using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KongGe : MonoBehaviour
{
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 2.0f)
            timer = 0f;
        if (timer <= 1.0f)
        {
            gameObject.GetComponent<Text>().color =
               new Color(gameObject.GetComponent<Text>().color.r, gameObject.GetComponent<Text>().color.g, gameObject.GetComponent<Text>().color.b, 1 * timer);
            timer += Time.deltaTime;
        }
        if (1.0f < timer && timer < 2.0f)
        {
            gameObject.GetComponent<Text>().color =
               new Color(gameObject.GetComponent<Text>().color.r, gameObject.GetComponent<Text>().color.g, gameObject.GetComponent<Text>().color.b, 1 * (2 - timer));
            timer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
        }
    }
}
