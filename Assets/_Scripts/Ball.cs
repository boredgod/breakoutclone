using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float initialSpeed = 5f;
    [SerializeField] float minSpeed = 4f;
    [SerializeField] float maxSpeed = 8f;
    Rigidbody2D rb;
    bool isLaunched = false;
    GameObject parentPaddle;
    Vector3 initialLocalPostion;
    int ballCount = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialLocalPostion = transform.localPosition;
        parentPaddle = transform.parent.gameObject;
        EventManager.AddBWDListener(ResetBall);
        EventManager.AddBallFiredListener(BallFired);
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

}
