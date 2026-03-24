using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller; 
    private Vector3 moveDirection; 
    
    [Header("Configurações")]
    public float speed = 5.0f;
    public float gravity = 20.0f;
    public float jumpHeight = 2.0f;

    [Header("Dash")]
    public float dashSpeed = 20f; 
    public float dashTime = 0.2f;  
    private float dashCounter;     
    private float dashCoolDown;    

    void Start() { controller = GetComponent<CharacterController>(); }

    void Update()
    {
        if (dashCounter > 0) { DashLogic(); }
        else { NormalMovement(); CheckDashInput(); }
    }

    void NormalMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            // Pega a rotação da Câmera
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;

            forward.y = 0; 
            right.y = 0;

            forward.Normalize();
            right.Normalize();
            moveDirection = (forward * moveZ + right * moveX).normalized * speed;
            if (Input.GetButtonDown("Jump")) 
            {
                moveDirection.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void CheckDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCoolDown <= 0)
        {
            dashCounter = dashTime;
            dashCoolDown = 1.0f; 
        }
        if (dashCoolDown > 0) dashCoolDown -= Time.deltaTime;
    }

    void DashLogic()
    {
        Vector3 dashMove = transform.forward * dashSpeed;
        controller.Move(dashMove * Time.deltaTime);
        dashCounter -= Time.deltaTime;
    }
}