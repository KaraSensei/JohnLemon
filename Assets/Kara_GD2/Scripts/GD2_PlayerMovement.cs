using UnityEngine;

public class GD2_PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the player movement
    public float turnSpeed = 20f; // Speed of the turning (radians per second)
    private Rigidbody rb; // Reference to the Rigidbody component
    private Vector3 movement; // Movement vector to store input direction
    private Animator animator; //Reference to the Animator component (if needed for animations)
    private Quaternion targetRotation = Quaternion.identity; // Target rotation for smooth turning

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to this GameObject
        animator = GetComponent<Animator>(); // Get the Animator component (if you have animations)
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get horizontal input (A/D or Left/Right arrows)
        float horizontal = Input.GetAxis("Horizontal");
        // Get vertical input (W/S or Up/Down arrows)
        float vertical = Input.GetAxis("Vertical");

        // 2) Вектор движения + нормализация
        movement = new Vector3(horizontal, 0f, vertical).normalized;

        Vector3 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;

        // Check if the player is moving
        bool isWalking = movement.magnitude > 0f;
        // Set walking animation state
        animator.SetBool("IsWalking", isWalking); 
    }

    
}
