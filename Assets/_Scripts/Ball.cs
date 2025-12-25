using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float initialSpeed = 5f;
    [SerializeField] float minSpeed = 4f;
    [SerializeField] float maxSpeed = 8f;
    [SerializeField]Transform paddleTransform;
    Rigidbody2D rb;
    Vector3 initialPaddleOffset;

    bool isLaunched = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(paddleTransform != null)
        {
            initialPaddleOffset = transform.position - paddleTransform.position;
        }
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
}
