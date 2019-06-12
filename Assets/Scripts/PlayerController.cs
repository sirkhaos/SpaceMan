using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //propiedades prublicas
    public float jumpFoce = 8f;
    public float speed = 2f;
    public LayerMask groundMask;

    //propiedades internas o privadas
    private Rigidbody2D rBPlayer;
    private Animator aniPlayer;
    [SerializeField]
    private float rangeColl = 1.3f;
    private Vector3 startPos;

    //constantes
    private const string INGROUND = "isOnTheGround";
    private const string ISAlIVE = "isALive";
    private const string DIALOG = "Dialogo";

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
        
        Invoke("ResetPosition", 0.28f);//ejecuta despues de un tiempo el metodo
    }

    private void ResetPosition()
    {
        this.transform.position = startPos;
        rBPlayer.velocity = Vector2.zero;
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
            jump();
        }

        Debug.DrawRay(this.transform.position, Vector2.down * rangeColl, Color.red);
    }

    void jump()
    {
        if (IsTouchingTheGround())
        {
            if (GameManager.ShareInstans.currentGameState == GameState.inGame)
            {
                rBPlayer.AddForce(Vector2.up * jumpFoce, ForceMode2D.Impulse);
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
    public void die()
    {
        aniPlayer.SetBool("isALive", false);
        GameManager.ShareInstans.EndGame();
    }
}
