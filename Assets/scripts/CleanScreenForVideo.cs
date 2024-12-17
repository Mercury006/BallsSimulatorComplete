using UnityEngine;

public class CleanScreenForVideo : MonoBehaviour
{
    public BallCleaner Cleaner; // Refer�ncia p�blica que pode ser atribu�da no Editor

    void Start()
    {
        if (Cleaner == null)
        {
            Cleaner = FindFirstObjectByType<BallCleaner>();
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C) && Cleaner != null)
        {
            Cleaner.PushBallsOutOfScreen();
            Debug.Log("Tecla C acionada");
        }
    }
}
