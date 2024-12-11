using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneTransition : MonoBehaviour
{
    public VideoPlayer videoPlayer;        // Refer�ncia ao VideoPlayer
    public string nextSceneName;           // Nome da cena para a qual deseja trocar
    public float delay = 0f;               // Delay adicional ap�s o fim do v�deo (para testes)

    void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer n�o est� atribu�do. Atribua um VideoPlayer no Inspector.");
            return;
        }

        // Adiciona um listener para quando o v�deo terminar
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // Fun��o chamada quando o v�deo termina
    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("V�deo terminou. Trocando de cena ap�s delay...");
        Invoke(nameof(ChangeScene), delay);
    }

    // Fun��o para trocar de cena
    void ChangeScene()
    {
        Debug.Log($"Carregando cena '{nextSceneName}'...");
        SceneManager.LoadScene(nextSceneName);
    }
}
