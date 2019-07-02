using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //propiedades prublicas
    public float jumpFoce = 8f;
    public float speed = 2f;
    public LayerMask groundMask;
    public int healthPoints, manaPoints;

    //propiedades internas o privadas
    private Rigidbody2D rBPlayer;
    private Animator aniPlayer;
    [SerializeField]
    private float rangeColl = 2f;
    private Vector3 startPos;

    //constantes
    private const string INGROUND = "isOnTheGround";
    private const string ISAlIVE = "isALive";
    private const string DIALOG = "Dialogo";

    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 15, MAX_HEALTH = 200, MAX_MANA = 30, MIN_HEALTH = 10, MIN_MANA = 0;
    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE = 1.5f;

    //la primera llamada
    private void Awake()
    {
        rBPlayer = GetComponent<Rigidbody2D>();
        aniPlayer = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
    }

    public void StartGame()
    {
        aniPlayer.SetBool(ISAlIVE, true);
        aniPlayer.SetBool(INGROUND, true);

        healthPoints = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;

        Invoke("ResetPosition", 0.28f);//ejecuta despues de un tiempo el metodo
    }

    private void ResetPosition()
    {
        this.transform.position = startPos;
        rBPlayer.velocity = Vector2.zero;
        Camera.main.GetComponent<CameraFollow>().ResetCameraPosition();
    }

    //update a rango fijo no por frame
    private void FixedUpdate()
    {
        if (GameManager.ShareInstans.currentGameState == GameState.inGame)
        {
            if (rBPlayer.velocity.x < speed)
            {
                rBPlayer.velocity = new Vector2(speed, rBPlayer.velocity.y);
            }
        }
        else
        {
            rBPlayer.velocity = new Vector2(0, rBPlayer.velocity.y);
        }
    }
    // Update is called once per frame
    void Update()
    {
        aniPlayer.SetBool(INGROUND, IsTouchingTheGround());
        /*if (Input.GetAxis(horizontal) != 0)
        {
            aniPlayer.SetFloat(horizontal, Mathf.Abs(Input.GetAxis(horizontal)));
        }*/
        if (Input.GetButtonDown("Jump"))
        {
            jump(false);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            jump(true);
        }

        Debug.DrawRay(this.transform.position, Vector2.down * rangeColl, Color.red);
    }

    void jump(bool super)
    {
        float jumpForceFactor = jumpFoce;
        if (super && manaPoints >= SUPERJUMP_COST) 
        {
            manaPoints -= SUPERJUMP_COST;
            jumpForceFactor *= SUPERJUMP_FORCE;
        }
        if (IsTouchingTheGround())
        {
            if (GameManager.ShareInstans.currentGameState == GameState.inGame)
            {
                GetComponent<AudioSource>().Play();
                rBPlayer.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
            }
        }
    }

    private bool IsTouchingTheGround()
    {
        //parametros son relevantes 1° pocicion actual, 2° hacia que direccion, 3° que distancia maxima, contra que capa
        if(Physics2D.Raycast(this.transform.position,Vector2.down, rangeColl, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Die()
    {
        float Score = GetTravelledDistance();
        float MaxScore = PlayerPrefs.GetFloat("MaxScore", 0f);

        if(Score> MaxScore)
        {
            PlayerPrefs.SetFloat("MaxScore", Score);
        }
        aniPlayer.SetBool("isALive", false);
        GameManager.ShareInstans.EndGame();
    }

    public void CollectHealth(int points)
    {
        this.healthPoints += points;
        if (this.healthPoints >= MAX_HEALTH)
        {
            this.healthPoints = MAX_HEALTH;
        }
        if (this.healthPoints <= 0)
        {
            Die();
        }
    }

    public void CollectMana(int points)
    {
        this.manaPoints += points;
        if (this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHealth()
    {
        return healthPoints;
    }
    
    public int GetMana()
    {
        return manaPoints;
    }
    public float GetTravelledDistance()
    {
        return this.transform.position.x - startPos.x;
    }
}
