using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed=1.5f;
    public bool facingRight=false;
    public int damage = 10;


    private Rigidbody2D rb;
    private Vector3 startPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called PARA ejecutarlo a intervalos fijos de tiempo(cambio en fisica)
    private void FixedUpdate()
    {
        float currentSpeed = speed;

        if (facingRight)
        {
            currentSpeed = speed;
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            currentSpeed = -speed;
            this.transform.eulerAngles = Vector3.zero;
        }

        if (GameManager.ShareInstans.currentGameState == GameState.inGame)
        {
            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Ground"))
        {
            facingRight = !facingRight;
            return;
        }
        if (collision.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().CollectHealth(-damage);
            return;
        }
    }
}
