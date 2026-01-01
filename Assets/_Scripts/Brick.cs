using UnityEngine;

public class Brick : MonoBehaviour
{
    float destroyTimer = 0.05f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject,destroyTimer);
        }
    }
}
