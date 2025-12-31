using UnityEngine;

public class ScreenUtility : MonoBehaviour
{
    [SerializeField]Camera mainCamera;
    float screenWidth;
    float screenHeight;

    public float ScreenWidth 
    { 
        get 
        { 
            screenWidth = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
            return screenWidth; 
        } 
    }
    public float ScreenHeight 
    { 
        get 
        { 
            screenHeight = mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;
            return screenHeight; 
        } 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
