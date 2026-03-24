using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform alvo;
    public Vector3 offset = new Vector3(0, 2f, -4f);
    public float sensibilidade = 150f;
    public float suavizacao = 10f;
    public LayerMask obstaculos;

    [Header("Configurações de Zoom")]
    public float distanciaMin = 2f; // Limite de quanto pode chegar perto
    public float distanciaMax = 10f; // Limite de quanto pode afastar

    float yaw, pitch = 10f, distancia;

    void Start() 
    { 
        distancia = offset.magnitude; 
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void LateUpdate()
    {
        if (!alvo) return;

        // Movimento do Mouse
        yaw += Input.GetAxis("Mouse X") * sensibilidade * Time.unscaledDeltaTime;
        pitch = Mathf.Clamp(pitch - Input.GetAxis("Mouse Y") * sensibilidade * Time.unscaledDeltaTime, -30f, 70f);

        // --- LÓGICA DO ZOOM (Implementada aqui) ---
        float scroll = Input.GetAxis("Mouse ScrollWheel"); 
        if (Mathf.Abs(scroll) > 0.01f) 
        {
            distancia -= scroll * 5f; 
            distancia = Mathf.Clamp(distancia, distanciaMin, distanciaMax); 
        }

        // Cálculo da rotação e posição
        Quaternion rot = Quaternion.Euler(pitch, yaw, 0);
        Vector3 destino = alvo.position + rot * (Vector3.back * distancia) + Vector3.up * 1.2f;

        // Anti-Clipping (Não atravessar parede)
        Vector3 dir = destino - alvo.position;
        if (Physics.SphereCast(alvo.position, 0.2f, dir.normalized, out RaycastHit hit, distancia, obstaculos))
            destino = alvo.position + dir.normalized * (hit.distance - 0.1f);

        // Aplicação final
        transform.position = Vector3.Lerp(transform.position, destino, Time.deltaTime * suavizacao);
        transform.LookAt(alvo.position + Vector3.up * 1.2f);
    }
}