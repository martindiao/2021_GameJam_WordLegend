using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jiewei : MonoBehaviour
{
    public bool jiewei = false;
    public bool Wanjie = false;
    public GameObject Up;

    public List<Sprite> sprites;
    public Sprite BG;
    public Sprite WG;

    float timer = 0f;
    int index = 0;

    bool showed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame 12
    void Update()
    {
        if (jiewei && !showed)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);

            timer += Time.deltaTime;

            if (index < 12)
            {
                if (timer < 1.0f)
                {
                    Up.GetComponent<Image>().sprite = sprites[index];
                    Up.GetComponent<Image>().color = new Color(0, 0, 0, timer);
                }
                else if (timer >= 1.0f && timer <= 2.0f)
                {
                    Up.GetComponent<Image>().color = new Color(0, 0, 0, 2.0f - timer);
                }
                else
                {
                    timer = 0;
                    index++;
                    
                }
            }

            if (index == sprites.Count - 1)
            {
                showed = true;
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                Up.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }

            if (index >= 12 && index < sprites.Count - 1)
            {
                Up.GetComponent<Image>().sprite = sprites[index];
                Up.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                index++;
            }
            

            
        }

        if (Wanjie)
        {

            
            if(gameObject.GetComponent<Image>().sprite != WG)
            {
                gameObject.GetComponent<Image>().sprite = WG;
                gameObject.transform.localScale = new Vector3(4, 4, 1);
            }
            
            timer += Time.deltaTime;

            if (timer < 3.0f)
            {
                gameObject.GetComponent<Image>().color = new Color(1, 1, 1, timer/3);
            }
            if (timer > 3.0f && timer <= 6.0f)
            {
                Debug.Log(gameObject.transform.localScale);
                gameObject.transform.localScale = new Vector3(4 - (timer - 3.0f), 4 - (timer - 3.0f), 1);
            }
            if (timer >= 6.0f)
            {
                Application.Quit();
            }

        }

    }
}
