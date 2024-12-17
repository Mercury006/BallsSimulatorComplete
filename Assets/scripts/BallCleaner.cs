using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BallCleaner : MonoBehaviour
{
    public BallCoverageVisualizer ballCoverageVisualizer; // Refer�ncia direta no Inspector
    public float pushForce = 10f; // For�a com a qual as bolinhas ser�o empurradas para fora da tela
    public float coverageThreshold = 0.4f; // Percentual de cobertura para ativar o empurr�o

    private bool isPushing = false; // Flag para verificar se as bolinhas est�o sendo empurradas
    private bool hasRemovedBalls = false; // Flag para garantir que as bolinhas sejam removidas apenas uma vez
    private bool hasStartedChecking = false; // Para controlar quando iniciar a verifica��o

    void Start()
    {
        // Iniciar o atraso para come�ar a verificar a cobertura
        Invoke(nameof(StartChecking), 10f); // Espera 10 segundos antes de come�ar a verificar
    }

    void StartChecking()
    {
        hasStartedChecking = true;
        Debug.Log("Iniciando verifica��o de cobertura.");
    }

    void Update()
    {
        // Somente come�a a verificar se j� passou o tempo de espera
        if (hasStartedChecking)
        {
            // Verifica a porcentagem de cobertura atingiu o limite
            float coverage = ballCoverageVisualizer.CalculateCoveragePercentage();

if (coverage < coverageThreshold && !isPushing)
{
    isPushing = true;
    Debug.Log($"Cobertura atingiu {coverage * 100}%, empurrando as bolinhas para fora.");
}

if (coverage <= 0.02f && !hasRemovedBalls && ballCoverageVisualizer.GetBalls().Length > 0)
{
                Invoke(nameof(RemoveAllBalls), 4f);
}
if (coverage <= 0.0001f && !hasRemovedBalls && ballCoverageVisualizer.GetBalls().Length > 0)
{
    RemoveAllBalls();
    hasRemovedBalls = true;
}


            if (isPushing)
            {
                PushBallsOutOfScreen();
            }
        }
    }

    // Empurra todas as bolinhas para fora da tela
    public void PushBallsOutOfScreen()
    {
        Camera mainCamera = ballCoverageVisualizer.mainCamera; // Usa a c�mera principal referenciada no BallCoverageVisualizer

        foreach (GameObject ball in ballCoverageVisualizer.GetBalls())
        {
            if (ball == null) continue;

            // Obt�m a posi��o da bolinha na tela
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(ball.transform.position);

            // Calcula a dire��o para empurrar a bolinha para fora da tela
            Vector3 directionToCenter = (screenPosition - new Vector3(Screen.width / 2, Screen.height / 2, 0)).normalized;

            // Aplica a for�a para empurrar a bolinha
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.AddForce(directionToCenter * pushForce, ForceMode.Impulse);
            }
        }

    }


    // Fun��o para remover todas as bolinhas da tela e chamar a transi��o de cena
    void RemoveAllBalls()
    {
        foreach (GameObject ball in ballCoverageVisualizer.GetBalls())
        {
            if (ball != null)
            {
                ball.SetActive(false); // Desativa a bolinha para remov�-la da tela
            }
        }
        Debug.Log("Todas as bolinhas foram removidas.");

        // For�a a transi��o de cena ap�s a remo��o das bolinhas
        //FindFirstObjectByType<SceneTransition>()?.ChangeScene();
        Invoke(nameof(ChangeScene), 1f);
    }

    public void ChangeScene()
    {
        Debug.Log("Tentando carregar a cena 'Video Interacao'...");
        SceneManager.LoadScene("Video Interacao");
    }
}
