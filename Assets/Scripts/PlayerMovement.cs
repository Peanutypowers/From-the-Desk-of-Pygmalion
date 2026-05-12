using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    Vector3 velocity;

    private CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Movement vector
        Vector3 move = transform.right * x + transform.forward * z;
        move.y = 0;

        controller.Move(move * speed * Time.deltaTime);
    }
}
