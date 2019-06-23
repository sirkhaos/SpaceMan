using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager sharedInstance;
    public Canvas menuCanvas;
    public Canvas inGameUI;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    public void ShowMainMenu()
    {
        menuCanvas.enabled = true;
        inGameUI.enabled = false;
    }

    public void HideMainMenu()
    {
        menuCanvas.enabled = false;
        ShowInGameUI();
    }

    public void ShowInGameUI()
    {
        inGameUI.enabled = true;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();  
        #endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
