using UnityEngine;
using DG.Tweening;

public class O_KayakPlayerController : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float lateralSpeed = 3f;
    private Rigidbody2D rb;
    [SerializeField] private Transform kayakTransform;
    private bool canMove = true;
    [SerializeField] private Vector3 jumpTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing!");
        }
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        if(!canMove) return;

        Vector3 movement = new Vector3(0, -forwardSpeed, 0);

        if (Input.GetKey(KeyCode.A) && canMove)
        {
            movement.x -= lateralSpeed;
            kayakTransform.DORotateQuaternion(Quaternion.Euler(0, 0, -45), 0.15f).SetEase(Ease.OutBack);
        }
        if (Input.GetKey(KeyCode.D) && canMove)
        {
            movement.x += lateralSpeed;
            kayakTransform.DORotateQuaternion(Quaternion.Euler(0, 0, 45), 0.15f).SetEase(Ease.OutBack);
        }
        if(((Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)) || (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))) && canMove)
        {
            movement.x = 0;
            kayakTransform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 0.15f).SetEase(Ease.OutBack);
        }

        rb.velocity = movement;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<O_Trigger>(out O_Trigger trigger))
        {
            kayakTransform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 0.15f).SetEase(Ease.OutBack);
            canMove = false;
            rb.velocity = Vector3.zero;

            rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);


            jumpTarget = new Vector3(transform.position.x, transform.position.y - 3.5f, transform.position.z);
            transform.DOJump(jumpTarget, 1f, 1, 3f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                rb.velocity = Vector3.zero;

                O_SceneManager.Instance.WinGame();
            });
        }
    }
}
