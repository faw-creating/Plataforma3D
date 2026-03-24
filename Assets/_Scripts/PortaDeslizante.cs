using UnityEngine;

public class PortaDeslizante : MonoBehaviour
{
    public Vector3 posicaoAberta;
    public Vector3 posicaoFechada;
    public float velocidade = 3f;
    private bool jogadorPerto = false;

    void Update()
    {
        Vector3 alvo = jogadorPerto ? posicaoAberta : posicaoFechada;
        transform.localPosition = Vector3.Lerp(transform.localPosition, alvo, Time.deltaTime * velocidade);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) jogadorPerto = true;
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player")) jogadorPerto = false;
    }
}