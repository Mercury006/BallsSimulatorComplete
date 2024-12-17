using UnityEngine;
using UnityEngine.UI;         // Para UI Text
using UnityEngine.Video;      // Para VideoPlayer

public class VideoTimeDisplay : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Refer�ncia ao VideoPlayer
    public Text timeText;            // Refer�ncia ao componente UI Text para exibir o tempo

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
}
