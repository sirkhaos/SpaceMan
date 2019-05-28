using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpFoce = 6f;
    public LayerMask groundMask;

    private Rigidbody2D rBPlayer;
    private Animator aniPlayer;
    private const string horizontal = "Horizontal";

    //la primera llamada
    private void Awake()
    {
        rBPlayer = GetComponent<Rigidbody2D>();
        aniPlayer = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis(horizontal) != 0)
        {
            aniPlayer.SetFloat(horizontal, Mathf.Abs(Input.GetAxis(horizontal)));
        }
        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            jump();
        }
        if (false)
        {
            aniPlayer.SetBool("isOnTheGround", false);
        }
    }

    void jump()
    {
        if (IsTouchingTheGround())
        {
            rBPlayer.AddForce(Vector2.up * jumpFoce, ForceMode2D.Impulse);
        }
    }

    private bool IsTouchingTheGround()
    {
        //parametros son relevantes 1° pocicion actual, 2° hacia que direccion, 3° que distancia maxima, contra que capa
        if(Physics2D.Raycast(this.transform.position,Vector2.down, 2.5f, groundMask))
        {
            //TODO: programar logica del suelo
            return true;
        }
        else
        {
            //TODO: programar logiaca de no contacto
            return false;
        }
    }
}
