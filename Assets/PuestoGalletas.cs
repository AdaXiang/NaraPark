
using UnityEngine;

public class PuestoGalletas : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 3f;

    public static int cookies = 0;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(
            player.position, transform.position
        );

        if (distance <= interactionDistance &&
            Input.GetKeyDown(KeyCode.E))
        {
            cookies++;
            Debug.Log("Has recogido una galleta. Total: " + cookies);
        }
    }

    void OnGUI()
    {
        if (player == null) return;

        // Estilo del texto
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 26;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.white;

        // Fondo oscuro del contador
        GUI.Box(new Rect(80, 50, 200, 55), "");

        // Contador de galletas (siempre visible)
        GUI.Label(
            new Rect(105, 58, 200, 40),
            "Galletas: " + cookies,
            style
        );

        // Mensaje al acercarse al puesto
        float distance = Vector3.Distance(
            player.position,
            transform.position
        );

        if (distance <= interactionDistance)
        {
            GUI.Box(
                new Rect(Screen.width / 2 - 160,
                        Screen.height - 110, 320, 50),
                ""
            );

            GUI.Label(
                new Rect(Screen.width / 2 - 120,
                        Screen.height - 102, 300, 40),
                "Pulsa E para coger galletas",
                style
            );
        }
    }
}
