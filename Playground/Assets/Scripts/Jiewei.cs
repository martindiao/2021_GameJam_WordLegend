using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jiewei : MonoBehaviour
{
    public bool jiewei = false;
    public GameObject Up;

    public List<Sprite> sprites;
    public Sprite BG;

    float timer = 0f;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (jiewei)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);

            timer += Time.deltaTime;

            if (timer < 1.0f)
            {
                Up.GetComponent<Image>().sprite = sprites[index];
                Up.GetComponent<Image>().color = new Color(0, 0, 0, timer);
            }
            else if (timer >= 1.0f && timer <= 2.0f)
            {
                Up.GetComponent<Image>().color = new Color(0, 0, 0, 2.0f-timer);
            }
            else
            {
                timer = 0;
                index++;
            }

            if (index == sprites.Count - 1)
            {
                gameObject.SetActive(false);
            }

        }
    }
}
