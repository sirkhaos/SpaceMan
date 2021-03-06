﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //creando el singleton
    public static LevelManager sharedInstance;

    private void Awake()
    {
        levelStartPosition = GameObject.Find("LevelStart").transform;
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    //lista de todos los posibles niveles
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    //lista de los niveles cargados
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();
    //posision de el primer levels
    private Transform levelStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //agrega un nuevo level block al final.
    public void AddLevelBlock()
    {
        int randomIdx = Random.Range(1, allTheLevelBlocks.Count);

        LevelBlock block;

        Vector3 spawnPosition = Vector3.zero;

        if (currentLevelBlocks.Count == 0)
        {
            block = Instantiate(allTheLevelBlocks[0]);
            spawnPosition = levelStartPosition.position;
        }
        else
        {
            block = Instantiate(allTheLevelBlocks[randomIdx]);
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position;
        }
        block.transform.SetParent(this.transform, false);
        Vector3 correction = new Vector3(   spawnPosition.x - block.startPoint.position.x,
                                            spawnPosition.y - block.startPoint.position.y, 0);
        block.transform.position += correction;
        currentLevelBlocks.Add(block);
    }

    //quita el level block que deja de ser visto
    public void RemoveLevelBlock()
    {
        LevelBlock oldblock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldblock);
        Destroy(oldblock.gameObject);
    }

    //Quita todos los level block al resetiar la partida
    public void RemoveAllLevelBlock()
    {
        while (currentLevelBlocks.Count > 0)
        {
            RemoveLevelBlock();
        }
    }

    //genera los bloques iniciales del juego
    public void GenerateInitialBlocks()
    {
        for(int i = 0; i < 2; i++)
        {
            AddLevelBlock();
        }
    }
}
