
using UnityEngine;

public class PuestoGalletas : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    public Animator playerAnimator;

    [Header("Interacción")]
    public float interactionDistance = 3f;
    public float collectionCooldown = 1.5f;

    public static int cookies = 0;

    private float nextCollectionTime = 0f;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(
            player.position,
            transform.position
        );

        if (distance <= interactionDistance &&
            Input.GetKeyDown(KeyCode.E) &&
            Time.time >= nextCollectionTime)
        {
            // Establecer el tiempo de espera
            nextCollectionTime = Time.time + collectionCooldown;

            // Añadir una galleta
            cookies++;

            // Reproducir la animación de recoger
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("CogerGalletas");
            }

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

        // Contador de galletas
        GUI.Label(
            new Rect(105, 58, 200, 40),
            "Galletas: " + cookies,
            style
        );

        // Comprobar distancia al puesto
        float distance = Vector3.Distance(
            player.position,
            transform.position
        );

        // Mostrar mensaje de interacción
        if (distance <= interactionDistance)
        {
            GUI.Box(
                new Rect(
                    Screen.width / 2 - 160,
                    Screen.height - 110,
                    320,
                    50
                ),
                ""
            );

            GUI.Label(
                new Rect(
                    Screen.width / 2 - 120,
                    Screen.height - 102,
                    300,
                    40
                ),
                "Pulsa E para coger galletas",
                style
            );
        }
    }
}
