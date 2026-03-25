using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    
    Rigidbody2D rb;

    [SerializeField] int speed;
    float speedMultiplier;

    bool btnPressed;

    bool isWallTouch;
    public LayerMask wallLayer;
    public Transform wallCheckPoint;

    [Range(1,10)]
    [SerializeField] float acceleration;

    Vector2 relativeTransform;

    public bool isOnPlatform;
    public Rigidbody2D platformRb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        UpdateRelativeTransform();
    }

    private void FixedUpdate()
    {
        UpdateSpeedMultiplier();

        float targetSpeed = speed * speedMultiplier * relativeTransform.x;

        if (isOnPlatform)
        {
            rb.linearVelocity = new Vector2(targetSpeed + platformRb.linearVelocity.x, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
        }

        isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.06f, 0.7f), 0, wallLayer);

        if (isWallTouch)
        {
            Flip();
        }
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        UpdateRelativeTransform();
    }

    void UpdateRelativeTransform()
    {
        relativeTransform = transform.InverseTransformVector(Vector2.one);
    }

    public void Move(InputAction.CallbackContext Value)
    {
        if (Value.started)
        {
            btnPressed = true;
            speedMultiplier = 1f;
        }
        else if (Value.canceled)
        {
            btnPressed = false;
            speedMultiplier = 0;
        }
    }

    private void UpdateSpeedMultiplier()
    {
        if (btnPressed && speedMultiplier < 1)
        {
            speedMultiplier += Time.deltaTime * acceleration;
        }
        else if (!btnPressed && speedMultiplier > 0)
        {
            speedMultiplier -= Time.deltaTime * acceleration;
            if (speedMultiplier < 0) speedMultiplier = 0;
        }
    }
}
