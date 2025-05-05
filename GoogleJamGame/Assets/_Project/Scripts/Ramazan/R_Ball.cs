using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public float speed;
    public Transform explosion;
    public GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.arcadeGameOver)
        {
            Debug.Log("Ball Destroyed");
            Destroy(this.gameObject);
            //return;
        }

        if (!inPlay)
        {
            transform.position = paddle.position;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !inPlay)
        {
            inPlay = true;
            Debug.Log("Pressed Space Bar");
            rb.AddForce(Vector2.up * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("fallCollider"))
        {
            Debug.Log("Ball collided with " + collision.name);
            rb.velocity = Vector2.zero;
            inPlay = false;
            gameManager.UpdateLives(-1);
            Debug.Log("Lives: " + GameManager.lives);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("brick"))
        {
            Transform newExplosion = Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            Destroy(newExplosion.gameObject, 1f);

            gameManager.UpdateScore(collision.gameObject.GetComponent<R_Brick>().points);
            gameManager.BrickCounter();
            //Destroy(collision.gameObject);

        }
    }
}
