using UnityEngine;

public class ToggleCursorVisibility : MonoBehaviour
{
    private bool cursorVisible = true; // Estado inicial do cursor

    void Update()
    {
        // Detecta a tecla H para alternar a visibilidade do cursor
        if (Input.GetKeyDown(KeyCode.H))
        {
            cursorVisible = !cursorVisible; // Inverte o estado do cursor

            // Atualiza apenas a visibilidade do cursor
            Cursor.visible = cursorVisible;

            // Log no console indicando o estado atual do cursor
            if (cursorVisible)
            {
                Debug.Log("Cursor est� VIS�VEL. Pode ser movimentado e clicado normalmente.");
            }
            else
            {
                Debug.Log("Cursor est� INVIS�VEL. Ainda funcional, mas n�o pode ser visto.");
            }
        }
    }
}
