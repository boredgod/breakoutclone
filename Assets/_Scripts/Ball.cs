using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float initialSpeed = 5f;
    [SerializeField] float minSpeed = 4f;
    [SerializeField] float maxSpeed = 8f;
    
    [SerializeField] float maxBounceAngle = 75;
    Rigidbody2D rb;

    ScreenUtility mainCamera;
    bool isLaunched = false;
    GameObject parentPaddle;
    Vector3 initialLocalPostion;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScreenUtility>();
        rb = GetComponent<Rigidbody2D>();
        initialLocalPostion = transform.localPosition;
        parentPaddle = transform.parent.gameObject;
        BallEventManager.AddBWDListener(ResetBall);
        BallEventManager.AddBallFiredListener(BallFired);
    }

    // Update is called once per frame
    void Update()
    {
        if(isLaunched)
        {
            transform.SetParent(null);   
        }
        if(isLaunched && rb.linearVelocity.magnitude < minSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * minSpeed;
        }
        else if(isLaunched && rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }


    void BallFired()
    {
        isLaunched = true;
        rb.linearVelocity = Vector2.up * initialSpeed;
    }

    void ResetBall()
    {
        isLaunched = false;
        rb.linearVelocity = Vector2.zero;
        transform.SetParent(parentPaddle.transform);
        transform.localPosition = initialLocalPostion;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        BallReflect(collision);
    }
    private void BallReflect(Collision2D collision)
    {        
        if (collision.gameObject.CompareTag("Player") && isLaunched)
        {
            Quaternion rotation = CalculateReflectAngle(collision);

            rb.linearVelocity = rotation * Vector2.up * rb.linearVelocity.magnitude;
        }
        else if(collision.gameObject.CompareTag("Wall") && isLaunched)
        {
            Quaternion rotation = CalculateReflectAngle(collision);
            float screenWidth = mainCamera.ScreenWidth;
            if(gameObject.transform.position.x > screenWidth/2f)
            {
                rb.linearVelocity = rotation * Vector2.left * rb.linearVelocity.magnitude;
            }
            else if(gameObject.transform.position.x < screenWidth/2f)
            {
                rb.linearVelocity = rotation * Vector2.right * rb.linearVelocity.magnitude;
            }
            else
            {
                rb.linearVelocity = rotation * Vector2.down * rb.linearVelocity.magnitude;
            }
        }
    }

    private Quaternion CalculateReflectAngle(Collision2D collision)
    {
        Vector3 colliderPosition = collision.transform.position;
        Vector2 contactPoint = collision.GetContact(0).point;
        float offset = colliderPosition.x - contactPoint.x;
        float width = collision.collider.bounds.size.x / 2f;
        float currentAngle = Vector2.SignedAngle(Vector2.up, rb.linearVelocity);
        float bounceAngle = (offset / width) * maxBounceAngle;
        float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);
        Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
        return rotation;
    }
}
