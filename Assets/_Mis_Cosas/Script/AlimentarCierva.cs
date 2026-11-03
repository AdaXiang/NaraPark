
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AlimentarCierva : MonoBehaviour
{
    [Header("Jugador")]
    public Transform player;
    public Animator playerAnimator;

    [Header("Interacción")]
    public float interactionDistance = 3f;
    public float stopDuration = 5f;

    [Header("Cierva")]
    public ParticleSystem Corazones;
    public MonoBehaviour DeerMovement;
    public Animator DeerController;

    [Header("Sonido")]
    public AudioClip sonidoComer;
    [Range(0f, 1f)]
    public float volumenSonido = 1f;

    private NavMeshAgent agent;
    private bool isEating = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player == null || isEating) return;

        float distance = Vector3.Distance(
            player.position,
            transform.position
        );

        if (distance <= interactionDistance &&
            Input.GetKeyDown(KeyCode.F))
        {
            if (PuestoGalletas.cookies > 0)
            {
                StartCoroutine(Feed());
            }
            else
            {
                Debug.Log("No tienes galletas.");
            }
        }
    }

    IEnumerator Feed()
    {
        isEating = true;
        PuestoGalletas.cookies--;

        // Animación de extender la mano del jugador
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("CogerGalletas");
        }

        // Detener movimiento de la cierva
        bool movementWasEnabled =
            DeerMovement != null && DeerMovement.enabled;

        if (DeerMovement != null)
            DeerMovement.enabled = false;

        // Detener NavMeshAgent
        if (agent != null && agent.enabled)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }

        // Animación Idle de la cierva
        if (DeerController != null)
        {
            DeerController.enabled = true;
            DeerController.Play("Idle", 0, 0f);
        }

        // Reproducir sonido de comer
        if (sonidoComer != null)
        {
            AudioSource.PlayClipAtPoint(
                sonidoComer,
                transform.position,
                volumenSonido
            );
        }

        // Mostrar corazones
        if (Corazones != null)
        {
            Corazones.gameObject.SetActive(true);

            Corazones.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );

            Corazones.Play();

            Debug.Log("Corazones activados");
        }
        else
        {
            Debug.LogWarning("No has asignado Corazones");
        }

        Debug.Log("¡La cierva está feliz!");

        // Esperar el tiempo de alimentación
        yield return new WaitForSeconds(stopDuration);

        // Reactivar movimiento
        if (agent != null && agent.enabled)
            agent.isStopped = false;

        if (DeerMovement != null)
            DeerMovement.enabled = movementWasEnabled;

        isEating = false;
    }

    void OnGUI()
    {
        if (player == null || isEating ||
            PuestoGalletas.cookies <= 0)
            return;

        float distance = Vector3.Distance(
            player.position,
            transform.position
        );

        if (distance <= interactionDistance)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 24;
            style.normal.textColor = Color.white;

            GUI.Box(
                new Rect(
                    Screen.width / 2 - 160,
                    Screen.height - 170,
                    320,
                    50
                ),
                ""
            );

            GUI.Label(
                new Rect(
                    Screen.width / 2 - 145,
                    Screen.height - 160,
                    300,
                    40
                ),
                "Pulsa F para dar una galleta",
                style
            );
        }
    }
}
