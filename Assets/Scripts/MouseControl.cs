using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 mousePosition;
	public float moveSpeed = 0.1f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
		transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        //transform.position = Vector3.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);
        
    }
}