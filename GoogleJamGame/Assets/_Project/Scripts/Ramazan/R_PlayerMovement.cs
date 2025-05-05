using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace PlayerMovement {
    public class R_PlayerMovement : MonoBehaviour
    {
        private float horizontal;
        public float speed = 6f;
        public float jumpingPower = 6f;
        private bool isFacingRight = true;
        public int nextScene;
        public int previousScene;

        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;

        public R_TicketManager ticketManager;
        public R_CoinManager coinManager;

        private void Start()
        {
            nextScene = SceneManager.GetActiveScene().buildIndex + 1; // Loads next scene 
            previousScene = SceneManager.GetActiveScene().buildIndex - 1; // Loads previous scene
        }
        void Update()
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            Flip();
        }

        private void FixedUpdate()
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

        private void Flip()
        {
            if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ticket"))
            {
                Destroy(other.gameObject);
                ticketManager.ticketCount++;
                Debug.Log("Ticket: " + ticketManager.ticketCount);

            }
            if (other.gameObject.CompareTag("coin"))
            {
                Destroy(other.gameObject);
                coinManager.coinCount++;

            }
        }




    }

}