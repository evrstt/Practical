using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private int maxHealth = 10;

    private Rigidbody rb;
    private Vector3 moveDireciton;
    private bool jumpPressed;
    private bool isGrounded = true;
    private int currentHealth;

    public bool isAlive
    {
        get { return currentHealth > 0; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    
    void Update()
    {
        HandleInput();
        HandleShooting();
    }

    void FixedUpdate()
    {
        MovePlayer();
        HandleJump();
    }

    private void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDireciton = transform.right * horizontalInput + transform.forward * verticalInput;
        moveDireciton = moveDireciton.normalized;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpPressed = true;
        }
    }

    private void MovePlayer()
    {
        Vector3 newPosition = rb.position + moveDireciton * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    private void HandleJump()
    {
        if (jumpPressed)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpPressed = false;
        }
    }

    private void HandleShooting()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (projectilePrefab == null || projectileSpawnPoint == null)
        {
            return;
        }

        Instantiate(
            projectilePrefab,
            projectileSpawnPoint.position,
            projectileSpawnPoint.rotation
        );
    }

    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        Debug.Log("Player Health " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player has died");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
