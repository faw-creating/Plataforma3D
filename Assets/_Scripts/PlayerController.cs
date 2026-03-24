using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller; 
    private Vector3 moveDirection; 

    [Header("Configurações de Movimento")]
    public float speed = 5.0f;
    public float gravity = 20.0f;
    public float jumpHeight = 2.0f;

    [Header("Configurações de Dash")]
    public float dashSpeed = 20f; // Velocidade do Dash
    public float dashTime = 0.2f; // Tempo de Dash
    private float dashCounter; // Quantos Dashes podemos fazer
    private float dashCoolDown; // Tempop para novamente dar Dash
    private Vector3 dashStoredDirection; // VARIÁVEL NOVA: Guarda para onde vamos dar o Dash

    void Start() 
    { 
        // Pega o componente que cuida da física de movimento
        controller = GetComponent<CharacterController>(); 
    }

    void Update()
    {
        // SISTEMA DE ESTADOS:
        
        if (dashCounter > 0) // Se o contador do Dash for maior que zero, ele executa a lógica de Dash.
        { 
            DashLogic();// Metodo da Logica de Dash
        }
        else // Se não, ele executa o movimento normal.
        { 
            NormalMovement(); // Metodo de Movimento Normal
            CheckDashInput(); // Verificador de entrada para realizar um Dash
        }
    }

    void NormalMovement()
    {
        // 1. Pega os inputs (AWSD)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // 2. Só permite controle total se estiver no chão
        if (controller.isGrounded)
        {
            // Pega a direção que a Câmera principal está olhando
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;

            // Zera o Y para o player não tentar voar ou afundar no chão ao olhar pra cima/baixo
            forward.y = 0; 
            right.y = 0;
            // back.y = 0; // !
            forward.Normalize();
            right.Normalize();

            // 3. Calcula a direção baseada na Câmera e nos Teclados
            // moveDirection é um vetor que aponta exatamente para onde você quer ir
            moveDirection = (forward * moveZ + right * moveX).normalized * speed; // !

            // 4. Lógica do Pulo
            if (Input.GetButtonDown("Jump")) 
            {
                // Fórmula física: Velocidade = Raiz Quadrada(Altura * 2 * Gravidade)
                moveDirection.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
            }
        }

        // 5. Aplica a gravidade constantemente
        moveDirection.y -= gravity * Time.deltaTime;

        // 6. Move o jogador de fato
        controller.Move(moveDirection * Time.deltaTime);
    }

    void CheckDashInput()
    {
        // Verifica se apertou Shift e se o "tempo de descanso" (cooldown) acabou
        if (Input.GetKeyDown(KeyCode.Q) && dashCoolDown <= 0)
        {
            // CORREÇÃO AQUI: 
            // Antes de começar o Dash, salvamos para onde a câmera está olhando agora.
            // Se o player estiver parado, damos o Dash para frente da câmera.
            Vector3 direcaoDoOlhar = Camera.main.transform.forward;
            direcaoDoOlhar.y = 0;
            dashStoredDirection = direcaoDoOlhar.normalized;

            dashCounter = dashTime; // Inicia o tempo do Dash
            dashCoolDown = 1.0f;    // Define 1 segundo de espera
        }

        // Diminui o tempo de espera do Dash a cada segundo
        if (dashCoolDown > 0) dashCoolDown -= Time.deltaTime;
    }

    void DashLogic()
    {
        // Durante o Dash, usamos a direção que salvamos no momento do clique.
        // Isso ignora a gravidade por um breve momento (efeito de impulso).
        controller.Move(dashStoredDirection * dashSpeed * Time.deltaTime);
        
        // Diminui o tempo de duração do Dash
        dashCounter -= Time.deltaTime;
    }
}