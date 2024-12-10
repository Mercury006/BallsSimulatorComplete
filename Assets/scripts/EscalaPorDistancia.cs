using UnityEngine;

public class ScaleOnMouseProximity : MonoBehaviour
{
    public float maxDistance = 100f;  // Dist�ncia m�xima para influ�ncia (em pixels)
    public float maxScale = 1f;     // Escala m�xima (quando o objeto est� mais distante)
    public float minScale = 0.001f;     // Escala m�nima (quando o objeto est� mais perto)

    private Vector3 originalScale;

    void Start()
    {
        // Armazenar a escala original do objeto
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Obter a posi��o do mouse na tela
        Vector3 mouseScreenPos = Input.mousePosition;

        // Calcular a dist�ncia entre o mouse e o objeto (usando a posi��o do objeto na tela)
        Vector3 objectScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        float distance = Vector3.Distance(mouseScreenPos, objectScreenPos);

        // Se a dist�ncia for menor que o raio m�ximo, calcular a escala baseada na dist�ncia
        if (distance < maxDistance)
        {
            // Calcular a porcentagem de influencia com base na dist�ncia
            float scaleFactor = distance / maxDistance;  // Quanto maior a distancia, maior a escala

            // Interpolacaoo entre a escala minima e maxima com base na dist�ncia
            float targetScale = Mathf.Lerp(minScale, maxScale, scaleFactor);

            // Aplicar a nova escala ao objeto
            transform.localScale = originalScale * targetScale;
        }
        else
        {
            // Se a distancia for maior que o maximo, a escala e a maxima
            transform.localScale = originalScale * maxScale;
        }
    }
}
