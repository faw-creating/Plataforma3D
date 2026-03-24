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
            destinoAtual = (destinoAtual == pontoA.position) ? pontoB.position : pontoA.position;
        }
    }

    // Faz o Player "virar filho" da plataforma para ele se mover junto com ela
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    // Quando o Player pula ou sai, ele deixa de ser filho
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}