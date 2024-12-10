using UnityEngine;
using UnityEngine.SceneManagement; // Para carregar cenas

public class SceneTransition : MonoBehaviour
{
    public BallCoverageVisualizer ballCoverageVisualizer; // Refer�ncia ao visualizador da cobertura
    public float coverageThreshold = 0f; // Quando a cobertura for igual ou menor que isso, a cena ser� carregada
    public float cooldownTime = 5f; // Tempo de cooldown em segundos
    public float sceneTransitionDelay = 2f; // Tempo de delay antes de carregar a cena

    private bool hasTransitioned = false; // Para garantir que a cena ser� carregada apenas uma vez
    private float cooldownTimer = 0f; // Timer para controlar o cooldown

    private void Update()
    {
        if (ballCoverageVisualizer == null)
        {
            Debug.LogError("BallCoverageVisualizer n�o est� atribu�do. Verifique o Inspector.");
            return;
        }

        // Se o cooldown ainda n�o acabou, decrementa o tempo
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            // Verifica se a cobertura atingiu o limite
            float coverage = ballCoverageVisualizer.CalculateCoveragePercentage();
            Debug.Log($"Cobertura atual: {coverage * 100}%"); // Log para depura��o

            if (coverage <= coverageThreshold && !hasTransitioned)
            {
                StartSceneTransition();
            }
        }
    }

    public void StartSceneTransition()
    {
        if (!hasTransitioned)
        {
            hasTransitioned = true;
            Debug.Log("Cobertura atingiu o limite, aguardando 2 segundos para carregar a cena...");

            // Inicia o delay de 2 segundos antes de trocar a cena
            Invoke(nameof(ChangeScene), sceneTransitionDelay);
        }
    }

    // Fun��o para trocar de cena
    void ChangeScene()
    {
        Debug.Log("Tentando carregar a cena 'Video Interacao'...");
        SceneManager.LoadScene("Video Interacao"); // Carrega a cena chamada "Video Interacao"
    }

    // Fun��o para iniciar o cooldown e come�ar a detec��o
    public void StartCooldown()
    {
        cooldownTimer = cooldownTime; // Reseta o timer de cooldown
        hasTransitioned = false; // Garante que a cena ser� carregada apenas uma vez
        Debug.Log("Cooldown iniciado. Aguardando 5 segundos para come�ar a detec��o...");
    }
}
