using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    ScreenUtility mainCamera;
    [SerializeField]BoxCollider2D topWall;
    [SerializeField]BoxCollider2D bottomWall;
    [SerializeField]BoxCollider2D leftWall;
    [SerializeField]BoxCollider2D rightWall;

    float screenWidth;
    float screenHeight;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = GetComponent<ScreenUtility>();
    }
    // Update is called once per frame
    void Update()
    {
        ScreenSizeCalculation();
        ProcessCameraBounds();
    }

    private void ProcessCameraBounds()
    {
        topWall.size = new Vector2(screenWidth * 2f, 1f);
        topWall.offset = new Vector2(0f, screenHeight + 0.5f);
        bottomWall.size = new Vector2(screenWidth * 2f, 1f);
        bottomWall.offset = new Vector2(0f, -screenHeight - 0.5f);
        leftWall.size = new Vector2(1f, screenHeight * 2f);
        leftWall.offset = new Vector2(-screenWidth - 0.5f, 0f);
        rightWall.size = new Vector2(1f, screenHeight * 2f);
        rightWall.offset = new Vector2(screenWidth + 0.5f, 0f);
    }

    private void ScreenSizeCalculation()
    {
        screenWidth = mainCamera.ScreenWidth;
        screenHeight = mainCamera.ScreenHeight;
        // Debug.Log("Width: " + screenWidth + " Height: " + screenHeight);
    }
    }
