using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 20f;             // скорость поворота (радиан/с)

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
        // 1) Читаем ввод
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 2) Вектор движения + нормализация
        movement = new Vector3(horizontal, 0f, vertical).normalized;

        // 3) Анимация
        bool isWalking = movement.magnitude > 0f;
        animator.SetBool("IsWalking", isWalking);

        // 4) Поворот
        if (movement != Vector3.zero)
        {
            // Плавно поворачиваемся в сторону движения
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
            if (!m_AudioSource.isPlaying) m_AudioSource.Play(); // Запускаем звук шагов, если он не играет
        }
        else
        {
            m_AudioSource.Stop(); // Останавливаем звук шагов, если игрок не движется
        }
    }
    void OnAnimatorMove()
    {
        // 1) Движение через root motion magnitude и наш вектор
        rb.MovePosition(
            rb.position + movement * animator.deltaPosition.magnitude
        );

        // 2) Применяем рассчитанный поворот
        rb.MoveRotation(targetRotation);
    }

}