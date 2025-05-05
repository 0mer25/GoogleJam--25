using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Brick : MonoBehaviour
{
    public int points;
    public int brickPoint;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            Destroy(gameObject);
        }
    }
}
