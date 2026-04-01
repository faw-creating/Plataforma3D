using UnityEngine;

public class PlataformaMovel : MonoBehaviour
{
    public Transform pontoA;
    public Transform pontoB;
    public float velocidade = 3f;
    private Vector3 destinoAtual;

    void Start()
    {
        destinoAtual = pontoB.position;
    }

    void Update()
    {
        // Move a plataforma
        transform.position = Vector3.MoveTowards(transform.position, destinoAtual, velocidade * Time.deltaTime);

        // Se chegou no destino, inverte
        if (Vector3.Distance(transform.position, destinoAtual) < 0.1f)
        {
            destinoAtual = (destinoAtual == pontoA.position) ? pontoB.position : pontoA.position; // Tentando aprender
        }
        posicaoAnterior = transform.position; // !
    }

    private Vector3 posicaoAnterior; // Variável para guardar o deslocamento da plataforma no último frame

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Pegamos o CharacterController do Player
            CharacterController player = other.GetComponent<CharacterController>();
            
            if (player != null)
            {
                // Calculamos quanto a plataforma se moveu neste frame
                Vector3 deslocamento = transform.position - posicaoAnterior;
                
                // Movemos o player manualmente a mesma distância, sem interferir no WASD dele
                player.Move(deslocamento);
            }
        }
    }
} 