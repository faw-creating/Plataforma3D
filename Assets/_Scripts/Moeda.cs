using UnityEngine;

public class Moeda : MonoBehaviour
{
    void Update() { transform.Rotate(0, 100 * Time.deltaTime, 0); } // Gira a moeda

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Avisa o HUD para somar ponto (vamos criar o HUD abaixo)
            FindAnyObjectByType<HUDManager>().AdicionarPonto();
            Destroy(gameObject);
        }
    }
}