using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private float rotTime = 0.2f;

    private Transform Player;

    private bool isRot;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.position;

        Rotate();
    }

    void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isRot)
        {
            StartCoroutine(RotateProcess(-90, rotTime));
        }
        if (Input.GetKeyDown(KeyCode.E) && !isRot)
        {
            StartCoroutine(RotateProcess(90, rotTime));
        }
    }

    IEnumerator  RotateProcess(float angle, float time)
    {
        float number = 60 * time;
        float nextAngle = angle / number;
        isRot = true;
        for(int i = 0; i< number; i++)
        {
            transform.Rotate(new Vector3(0, 0, nextAngle));
            yield return new WaitForFixedUpdate();
        }

        isRot = false;
    }
}
