using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField]InputActionReference moveAction;
    [SerializeField]InputActionReference fireAction;

    [SerializeField]float moveFactor = 5f;

    ScreenUtility mainCamera;
    float maxMove = 1f;
    float minMove = -1f;

    BallEvents ballEvents = new BallEvents();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScreenUtility>();
        BallEventManager.AddBallFiredInvoker(this);
    }

    // Update is called once per frame
    void Update()
    {
        
        ProcessTranslate();
    }

    void FixedUpdate()
    {
        ProcessFire();
    }

    private void ProcessTranslate()
    {
        float moveAxis = moveAction.action.ReadValue<float>();
        moveAxis = Mathf.Clamp(moveAxis, minMove, maxMove);
        // Vector3 moveVector = new Vector3(moveAxis * Time.deltaTime * moveFactor, 0, 0);
        transform.position += Vector3.right * moveAxis * moveFactor * Time.deltaTime;
        ProcessPaddleBounds();
    }

    void ProcessPaddleBounds()
    {
        Vector2 spriteSize = GetComponent<SpriteRenderer>().bounds.size;
        float screenWidth = mainCamera.ScreenWidth;
        float screenHeight = mainCamera.ScreenHeight;
        Vector3 objectTransform = transform.position;
        objectTransform.x = Mathf.Clamp(objectTransform.x, -screenWidth + spriteSize.x / 2f, screenWidth - spriteSize.x / 2f);
        transform.position = objectTransform;
    }
    void ProcessFire()
    {
        if(fireAction.action.WasPressedThisFrame())
        {
            // Debug.Log("Fire");
            //Invoke Event from Ball
            ballEvents.Invoke();
        }
    }
    public void AddBallFiredListener(UnityAction listener)
    {
        ballEvents.AddListener(listener);
    }
}
