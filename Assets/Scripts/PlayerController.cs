using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Speeds & Values")]
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float slowerSpeed = 5;
    [SerializeField] private float sidewaysSpeed = 10;
    [SerializeField] private float jumpForce = 10;

    [Header("Controls")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject controlButtons;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance;
    
    private Rigidbody _rb;
    private string _controls;
    private float _horizontal;
    private bool _wastedExtraLife;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _controls = PlayerPrefs.GetString("Controls");

        if (_controls == "JOYSTICK")
        {
            controlButtons.SetActive(false);
            joystick.gameObject.SetActive(true);
        }
        else if (_controls == "BUTTONS")
        {
            controlButtons.SetActive(true);
            joystick.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        MoveForward();

        if (_controls == "JOYSTICK") JoystickControls();
        else if (_controls == "BUTTONS") Move(_horizontal);

        if (transform.position.y < -1) SceneManager.LoadScene(0);
    }
    
    // Movement functions
    void MoveForward()
    {
        float speed = moveSpeed;
        if (DailyReward.Reward == DailyReward.RewardType.SlowerSpeed) speed = slowerSpeed;
        
        transform.Translate(Vector3.forward * (Time.deltaTime * speed), Space.World);
    }
    
    void Move(float horizontal)
    {
        float moveX = horizontal * sidewaysSpeed;
        
        _rb.velocity = new Vector3(moveX * Time.deltaTime, _rb.velocity.y, _rb.velocity.z);
    }

    public void Move(int horizontal)
    {
        _horizontal = horizontal;
    }

    public void Jump()
    {
        if (DailyReward.Reward == DailyReward.RewardType.UnlimitedJumps) _rb.velocity = Vector3.up * jumpForce;
        else if (IsGrounded()) _rb.velocity = Vector3.up * jumpForce;
    }
    
    // Controls management
    private void JoystickControls()
    {
        _horizontal = joystick.Horizontal;
        Move(_horizontal);

        if (joystick.Vertical > 0.5f)
        {
            Jump();
        }
    }
    
    // Other
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (GodMod.inGodMod)
            {
                Destroy(other.gameObject);
                return;
            }

            if (DailyReward.Reward == DailyReward.RewardType.ExtraLife && !_wastedExtraLife)
            {
                Destroy(other.gameObject);
                _wastedExtraLife = true;
                return;
            }
            
            SceneManager.LoadScene(0);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -transform.up, groundDistance, groundLayer);
    }
}
