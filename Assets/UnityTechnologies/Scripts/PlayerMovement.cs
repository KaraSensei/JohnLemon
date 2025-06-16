using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 20f;             // �������� �������� (������/�)

    private Rigidbody rb;
    private Animator animator;
    private Vector3 movement;
    private Quaternion targetRotation = Quaternion.identity;
    AudioSource m_AudioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        // 1) ������ ����
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 2) ������ �������� + ������������
        movement = new Vector3(horizontal, 0f, vertical).normalized;

        // 3) ��������
        bool isWalking = movement.magnitude > 0f;
        animator.SetBool("IsWalking", isWalking);

        // 4) �������
        if (movement != Vector3.zero)
        {
            // ������ �������������� � ������� ��������
            Vector3 newDir = Vector3.RotateTowards(
                transform.forward,
                movement,
                turnSpeed * Time.deltaTime,
                0f
);
            targetRotation = Quaternion.LookRotation(newDir);
        }
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying) m_AudioSource.Play(); // ��������� ���� �����, ���� �� �� ������
        }
        else
        {
            m_AudioSource.Stop(); // ������������� ���� �����, ���� ����� �� ��������
        }
    }
    void OnAnimatorMove()
    {
        // 1) �������� ����� root motion magnitude � ��� ������
        rb.MovePosition(
            rb.position + movement * animator.deltaPosition.magnitude
        );

        // 2) ��������� ������������ �������
        rb.MoveRotation(targetRotation);
    }

}