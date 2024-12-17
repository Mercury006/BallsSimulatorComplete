using UnityEngine;
using UnityEngine.UI;         // Para UI Text
using UnityEngine.Video;      // Para VideoPlayer
using System.Collections;    // Para usar Coroutine

public class VideoTimeDisplay : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Refer�ncia ao VideoPlayer
    public Text timeText;            // Refer�ncia ao componente UI Text para exibir o tempo
    public VideoClip[] videoClips;   // Array de VideoClips para selecionar aleatoriamente
    public CanvasGroup canvasGroup;  // Refer�ncia ao CanvasGroup para controle de fade (se usar UI)
    public float fadeDuration = 0f;  // Dura��o do fade in

    void Start()
    {
        // Verifica se o VideoPlayer e os outros componentes est�o atribu�dos
        if (videoPlayer == null || timeText == null || videoClips.Length == 0)
        {
            Debug.LogError("Refer�ncias n�o atribu�das corretamente. Verifique o Inspector.");
            return;
        }
         
        // Seleciona um v�deo aleat�rio da lista de VideoClips
        VideoClip randomVideoClip = videoClips[Random.Range(0, videoClips.Length)];

        // Define o VideoClip no VideoPlayer
        videoPlayer.clip = randomVideoClip;

        // Inicia o fade in do v�deo
        //StartCoroutine(FadeInVideo());

        // Inicia a reprodu��o do v�deo
        videoPlayer.Play();
    }

    void Update()
    {
        if (videoPlayer == null || timeText == null)
        {
            Debug.LogError("VideoPlayer ou timeText n�o est�o atribu�dos. Verifique o Inspector.");
            return;
        }

        // Verifica se o v�deo est� preparado para reproduzir
        if (videoPlayer.isPlaying)
        {
            // Calcula o tempo atual e a dura��o do v�deo em segundos
            double currentTime = videoPlayer.time;
            double duration = videoPlayer.length;

            // Atualiza o texto com o tempo formatado
            timeText.text = FormatTime(currentTime) + " / " + FormatTime(duration);
        }
    }

    // Fun��o para formatar o tempo em minutos e segundos (MM:SS)
    string FormatTime(double timeInSeconds)
    {
        int minutes = Mathf.FloorToInt((float)timeInSeconds / 60);
        int seconds = Mathf.FloorToInt((float)timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Coroutine para realizar o fade in
   /* IEnumerator FadeInVideo()
    {
        float elapsedTime = 0f;

        // Come�a com o canvasGroup invis�vel (alpha 0)
        canvasGroup.alpha = 0f;

        // Executa o fade in durante o tempo especificado
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assegura que a opacidade final ser� 1
        canvasGroup.alpha = 1f;
    }*/
}
