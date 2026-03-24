using UnityEngine;

public class PortaGiratoria : MonoBehaviour
{
    [Header("Configurações")]
    public float velocidadeAbrir = 3f;
    public float anguloAbertura = 90f; // Quanto a porta vai girar
    
    private bool aberta = false;
    private bool jogadorPerto = false;
    private Quaternion rotacaoInicial;
    private Quaternion rotacaoAlvo;

    void Start()
    {
        // Salva a rotação de quando o jogo começa (fechada)
        rotacaoInicial = transform.localRotation;
    }

    void Update()
    {
        // Se o jogador estiver perto e apertar 'E'
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            aberta = !aberta;
        }

        // Define para qual rotação a porta deve ir
        if (aberta)
        {
            rotacaoAlvo = rotacaoInicial * Quaternion.Euler(0, anguloAbertura, 0);
        }
        else
        {
            rotacaoAlvo = rotacaoInicial;
        }

        // Gira suavemente até o alvo
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotacaoAlvo, Time.deltaTime * velocidadeAbrir);
    }

    // Detecta o Player entrando na área da porta
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
            Debug.Log("Pressione E para interagir");
        }
    }

    // Detecta o Player saindo da área
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
        }
    }
}