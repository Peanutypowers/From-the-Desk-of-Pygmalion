using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 mousePosition;
	public float moveSpeed = 0.1f;
    public float leftBound = 14f;
    public float rightBound = 20f;
    public float lowerBound = 10f;
    public float upperBound = 20f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Bind mouse position
        if(mousePosition.x < leftBound) {
            mousePosition.x = leftBound;
        }
        if(mousePosition.x > rightBound) {
            mousePosition.x = rightBound;
        }
        if(mousePosition.y < lowerBound) {
            mousePosition.y = lowerBound;
        }
        if(mousePosition.y > upperBound) {
            mousePosition.y = upperBound;
        }

        mousePosition.z = transform.position.z; // will be 0
		transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        //transform.position = Vector3.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);
        
    }
}