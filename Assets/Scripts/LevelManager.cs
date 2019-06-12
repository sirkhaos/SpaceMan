using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //creando el singleton
    public static LevelManager sharedInstance;

    private void Awake()
    {
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
    public Transform levelStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLevelBlock()
    {
        //agrega un nuevo level block al final .
        //TODO: crear logica de pocicionar el nuevo levelbock
    }

    public void RemoveLevelBlock()
    {
        //quita el level block que deja de ser visto
        //TODO: crear la logica de eliminar los level block en desuso
    }

    public void RemoveAllLevelBlock()
    {
        //Quita todos los level block al resetiar la partida
        //TODO: crear la logica de limpiar el nivel.
    }

    public void GenerateInitialBlocks()
    {
        //genera los bloques iniciales del juego 
        for(int i = 0; i < 2; i++)
        {
            AddLevelBlock();
        }
    }
}
