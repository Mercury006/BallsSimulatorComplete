using UnityEngine;
using UnityEngine.SceneManagement; // Para carregar cenas

public class SceneTransitionOnCoverage : MonoBehaviour
{
    public BallCoverageVisualizer ballCoverageVisualizer; // Refer�ncia ao visualizador da cobertura
    public GameObject cubePrefab; // Prefab do cubo a ser instanciado
    public float coverageThreshold = 0f; // Quando a cobertura for igual ou menor que isso, o cubo ser� spawnado
    public float cooldownTime = 5f; // Tempo de cooldown em segundos

    private bool hasSpawned = false; // Para garantir que apenas um cubo seja spawnado
    private float cooldownTimer = 0f; // Timer para controlar o cooldown

    void Start()
    {
        // Inicia o cooldown quando o script come�ar
        StartCooldown();
    }

    void Update()
    {
        // Se o cooldown ainda n�o acabou, decrementa o tempo
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            // Se o cooldown acabou, come�amos a verificar a cobertura
            if (ballCoverageVisualizer != null && !hasSpawned)
            {
                float coverage = ballCoverageVisualizer.CalculateCoveragePercentage();

                // Verifica se a cobertura atingiu 0% ou menos
                if (coverage <= coverageThreshold)
                {
                    SpawnCubeAtCenter();
                }
            }
        }
    }

    // Fun��o para spawnar um cubo no centro da tela
    void SpawnCubeAtCenter()
    {
        if (cubePrefab != null)
        {
            // Calcula a posi��o no centro da tela
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 10f); // Dist�ncia da c�mera (ajuste conforme necess�rio)
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenCenter);

            // Cria o cubo na posi��o central
            Instantiate(cubePrefab, worldPosition, Quaternion.identity);
            Debug.Log("Cubo spawnado no centro da tela.");

            // Marca que o cubo foi spawnado e n�o deve ser mais spawnado
            hasSpawned = true;
        }
        else
        {
            Debug.LogError("Prefab do cubo n�o atribu�do. Atribua um prefab de cubo no Inspector.");
        }
    }

    // Fun��o para iniciar o cooldown e come�ar a detec��o
    public void StartCooldown()
    {
        cooldownTimer = cooldownTime; // Reseta o timer de cooldown
        hasSpawned = false; // Garante que o cubo ser� spawnado
        Debug.Log("Cooldown iniciado. Aguardando 5 segundos para come�ar a detec��o...");
    }

    // Fun��o para trocar de cena (a ser chamada mais tarde)
    void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Carrega a cena com o nome especificado
    }
}
