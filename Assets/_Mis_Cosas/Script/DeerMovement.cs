
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class DeerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float runSpeed = 3f;
    public float roamRadius = 12f;

    [Header("Descanso")]
    public float minIdleTime = 3f;
    public float maxIdleTime = 7f;

    private NavMeshAgent agent;
    private Animator animator;
    private Vector3 origin;

    private const int IDLE = 0;
    private const int RUN = 1;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        if (animator == null || !agent.isOnNavMesh)
        {
            Debug.LogError(
                "Falta Animator o la cierva no está en NavMesh");
            enabled = false;
            return;
        }

        animator.applyRootMotion = false;
        origin = transform.position;

        StartCoroutine(BehaviourLoop());
    }

    void SetIdle()
    {
        // Detener completamente el movimiento
        agent.isStopped = true;
        agent.ResetPath();
        agent.velocity = Vector3.zero;
        agent.speed = 0f;

        // Cambiar animación
        animator.SetInteger("state", IDLE);

        Debug.Log("Cierva: IDLE");
    }

    void SetRun()
    {
        // Preparar movimiento
        agent.speed = runSpeed;
        agent.isStopped = false;

        // Cambiar animación
        animator.SetInteger("state", RUN);

        Debug.Log("Cierva: RUN");
    }

    IEnumerator BehaviourLoop()
    {
        while (true)
        {
            // 1. Descansar
            SetIdle();

            yield return new WaitForSeconds(
                Random.Range(minIdleTime, maxIdleTime));

            // 2. Buscar un destino
            Vector3 destination = transform.position;
            bool found = false;

            for (int i = 0; i < 20; i++)
            {
                Vector2 randomPoint =
                    Random.insideUnitCircle * roamRadius;

                Vector3 candidate = origin +
                    new Vector3(randomPoint.x, 0,
                                randomPoint.y);

                if (NavMesh.SamplePosition(
                    candidate,
                    out NavMeshHit hit,
                    3f,
                    NavMesh.AllAreas))
                {
                    NavMeshPath path = new NavMeshPath();

                    if (agent.CalculatePath(
                        hit.position, path) &&
                        path.status ==
                            NavMeshPathStatus.PathComplete &&
                        Vector3.Distance(
                            transform.position,
                            hit.position) > 2f)
                    {
                        destination = hit.position;
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
                continue;

            // 3. Preparar la ruta antes de correr
            agent.speed = runSpeed;

            if (!agent.SetDestination(destination))
            {
                SetIdle();
                continue;
            }

            SetRun();

            // 4. Esperar a llegar
            float elapsed = 0f;

            while (elapsed < 20f)
            {
                if (!agent.pathPending)
                {
                    if (agent.pathStatus ==
                        NavMeshPathStatus.PathInvalid)
                        break;

                    if (agent.remainingDistance <=
                        agent.stoppingDistance &&
                        agent.velocity.sqrMagnitude < 0.01f)
                        break;
                }

                elapsed += Time.deltaTime;
                yield return null;
            }

            // 5. Parar antes del siguiente ciclo
            SetIdle();
        }
    }

    void OnDisable()
    {
        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.ResetPath();
            agent.velocity = Vector3.zero;
        }
    }
}
