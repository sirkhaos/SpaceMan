﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //obgetivo a segir
    public Transform target;
    //distancia de la camara
    public Vector3 offset = new Vector3(0.2f, 0.0f, -10f);
    // duresa de reacchion del movimiento de la camara
    public float dampingTime = 0.3f;
    //velosidad de la camara
    public Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera(true);
    }

    public void ResetCameraPosition()
    {
        MoveCamera(false);
    }

    void MoveCamera(bool Smooth)
    {
        //posision a donde deve ir la camara
        Vector3 destination = new Vector3(target.transform.position.x - offset.x, offset.y, offset.z);
        if (Smooth)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampingTime);
        }
        else
        {
            this.transform.position = destination;
        }
    }
}
