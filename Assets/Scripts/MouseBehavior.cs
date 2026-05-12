using UnityEngine;

public class MouseBehavior : MonoBehaviour
{
    public float mouseSensitivity = 500f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float upperClamp = -90f;
    public float lowerClamp = 90f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate around the x axis to look up and down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, upperClamp, lowerClamp); // Limit the vertical rotation so it doesn't reverse/move weird
        // Rotate around the y axis to look left and right
        yRotation += mouseX;
        
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        
    }
}
