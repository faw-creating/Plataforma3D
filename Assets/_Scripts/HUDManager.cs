using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI textoPontos;
    private int pontos = 0;

    public void AdicionarPonto()
    {
        pontos++;
        textoPontos.text = "Pontos: " + pontos;
    }
}