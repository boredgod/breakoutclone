using UnityEngine;
using UnityEngine.Events;

public class BottomWallDeath : MonoBehaviour
{
    BallEvents ballEvents = new BallEvents();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.AddBWDInvoker(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ballEvents.Invoke();
    }

    public void AddBottomWallDeathListener(UnityAction listener)
    {
        ballEvents.AddListener(listener);
    }
}
