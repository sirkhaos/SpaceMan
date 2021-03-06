﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState=GameState.menu;
    //creando un singleton del Game Manager
    public static GameManager ShareInstans;
    public int collectedObject = 0;

    private PlayerController controller;

    private void Awake()
    {
        if (ShareInstans == null)
        {
            ShareInstans = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && currentGameState!=GameState.inGame)
        {
            StartGame();
        }
    }

    //metodo para comensar una partida
    public void StartGame()
    {
        SetGameState(GameState.inGame);

    }

    //metodo para cuando pierda
    public void EndGame()
    {
        SetGameState(GameState.gameOver);
    }

    //metodo para ir al menu
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    private void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            //TODO: hacer la logica de menu
            UIManager.sharedInstance.ShowMainMenu();
        }else if(newGameState== GameState.inGame)
        {
            //TODO: hacer a logica de el juego
            //creo que deveria ir en gameover
            LevelManager.sharedInstance.RemoveAllLevelBlock();
            LevelManager.sharedInstance.GenerateInitialBlocks();
            controller.StartGame();
            UIManager.sharedInstance.HideMainMenu();
        }
        else if (newGameState == GameState.gameOver)
        {
            //TODO: preparar el juego para el game over
            UIManager.sharedInstance.ShowMainMenu();
        }
        this.currentGameState = newGameState;
    }

    public void CollectObject(Collectable collectable)
    {
        collectedObject += collectable.value;
    }
}
