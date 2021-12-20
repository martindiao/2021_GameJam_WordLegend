using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    //private float rotTime = 0.2f;

    private Transform Player;

    public Vector3 offset;

    public bool ending;
    public bool ended = false;
    public float MoveTime;

    Vector3 nextdistance;
    Vector3 direction;

    float Timer =0;

    //private bool isRot;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        ending = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!ended)
            transform.position = Player.position + offset;

        //Rotate();
        if (ended)
        {
            if (!ending)
            {
                transform.Rotate(new Vector3(45, 0, 0));
                Debug.Log(transform.localPosition);
                Debug.Log(transform.position);
                //nextdistance = Vector3.Distance(new Vector3(0, 0, -400), transform.position) / 120;
                direction = new Vector3(100, -80, -600) - transform.position;
                nextdistance = direction / 180;
                
            }
            Timer += 1/60f;
            ending = true;
            if (Timer <= 3.0f)
            {
                Debug.Log(nextdistance);
                transform.position += nextdistance;
                Debug.Log(transform.position);
            }

        }
    }

    public void End()
    {
        if (!ending)
        {
            Debug.Log(transform.localPosition);
            StartCoroutine(EndProcess(transform.localPosition, MoveTime));
        }
            
    }

    IEnumerator EndProcess(Vector3 a, float time)
    {
        float number = 60 * time;
        float nextAngle = 45 / number;
        float nextdistance = Vector3.Distance(new Vector3(0, 0, -400), a) / number;
        Debug.Log(nextAngle);
        Vector3 direction = (new Vector3(0, 0, -400) - a);
        Debug.Log(nextdistance);
        for (int i = 0; i < number; i++)
        {
            //transform.RotateAroundLocal(new Vector3(1, 0, 0),nextAngle);
            transform.localPosition += nextdistance * direction;
            yield return new WaitForFixedUpdate();
        }
        ending = true;
    }
}
