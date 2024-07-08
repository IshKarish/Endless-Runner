using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float sidewaysSpeed = 10;
    [SerializeField] private float jumpForce = 10;

    [SerializeField] private Joystick joystick;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance;
    
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveForward();

        float horizontal;
        
        if (Application.platform == RuntimePlatform.Android)
            horizontal = joystick.Horizontal;
        else
            horizontal = Input.GetAxis("Horizontal");
        
        Move(horizontal);
        
        if ((Input.GetButtonDown("Jump") || joystick.Vertical > 0.5f) && IsGrounded())
        {
            Jump();
        }
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * moveSpeed), Space.World);
    }

    void Move(float horizontal)
    {
        float moveX = horizontal * sidewaysSpeed;
        
        _rb.velocity = new Vector3(moveX * Time.deltaTime, _rb.velocity.y, _rb.velocity.z);
    }

    void Jump()
    {
        _rb.velocity = Vector3.up * jumpForce;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            enabled = false;
            SceneManager.LoadScene(0);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -transform.up, groundDistance, groundLayer);
    }
}
