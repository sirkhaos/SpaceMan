﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlock : MonoBehaviour
{
    public Transform startPoint, exitPoint;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.Find("StartPoint").transform;
        exitPoint = transform.Find("EndPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
