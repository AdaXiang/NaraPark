
using UnityEngine;

public class PlayerMovimiento : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform Camara;
    [SerializeField] private Animator Animador;
    [SerializeField] private Transform Modelo;

    private CharacterController Controlador;
    private bool EstaEnSuelo;
    private bool EstaSaltando;

    [Header("Movimiento")]
    [SerializeField] private float VelocidadMovimiento = 5f;
    [SerializeField] private float VelocidadCarrera = 8f;
    [SerializeField] private float VelocidadRotacion = 12f;

    private float VelocidadActual;

    [Header("Salto")]
    [SerializeField] private float FuerzaSalto = 2f;

    [Header("Gravedad")]
    [SerializeField] private float Gravedad = -9f;
    private Vector3 VelocidadVertical;

    private void Awake()
    {
        Controlador = GetComponent<CharacterController>();

        if (Animador == null)
            Animador = GetComponentInChildren<Animator>();

        if (Modelo == null && transform.childCount > 0)
            Modelo = transform.GetChild(0);

        if (Camara == null && Camera.main != null)
            Camara = Camera.main.transform;
    }

    private void Update()
    {
        if (Controlador == null || Camara == null)
            return;

        EstaEnSuelo = Controlador.isGrounded;

        MoverJugador();
        GestionarSalto();
        AplicarGravedad();
        ActualizarAnimaciones();
    }

    private void MoverJugador()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        Vector3 Adelante = Camara.forward;
        Vector3 Derecha = Camara.right;

        Adelante.y = 0f;
        Derecha.y = 0f;

        Adelante.Normalize();
        Derecha.Normalize();

        Vector3 Direccion =
            (Derecha * Horizontal) + (Adelante * Vertical);

        if (Direccion.sqrMagnitude > 1f)
            Direccion.Normalize();

        bool EstaMoviendose = Direccion.sqrMagnitude > 0.01f;
        bool EstaCorriendo = Input.GetKey(KeyCode.LeftShift);

        VelocidadActual = EstaCorriendo
            ? VelocidadCarrera
            : VelocidadMovimiento;

        Vector3 Desplazamiento =
            Direccion * VelocidadActual * Time.deltaTime;

        Controlador.Move(Desplazamiento);

        if (EstaMoviendose && Modelo != null)
        {
            Quaternion RotacionObjetivo =
                Quaternion.LookRotation(Direccion);

            Modelo.rotation = Quaternion.Slerp(
                Modelo.rotation,
                RotacionObjetivo,
                VelocidadRotacion * Time.deltaTime
            );
        }
    }

    private void GestionarSalto()
    {
        if (EstaEnSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            VelocidadVertical.y =
                Mathf.Sqrt(FuerzaSalto * -2f * Gravedad);

            EstaSaltando = true;
        }
    }


    private void AplicarGravedad()
    {
        bool EstabaEnSuelo = EstaEnSuelo;

        VelocidadVertical.y += Gravedad * Time.deltaTime;

        Controlador.Move(
            VelocidadVertical * Time.deltaTime
        );

        EstaEnSuelo = Controlador.isGrounded;

        if (EstaEnSuelo && VelocidadVertical.y < 0)
        {
            VelocidadVertical.y = -2f;

            if (EstaSaltando)
            {
                EstaSaltando = false;
            }
        }
    }

    private void ActualizarAnimaciones()
    {
        if (Animador == null)
            return;

        Animador.SetBool("Saltando", EstaSaltando);

        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        bool EstaMoviendose =
            Mathf.Abs(Horizontal) > 0.01f ||
            Mathf.Abs(Vertical) > 0.01f;

        bool EstaCorriendo = Input.GetKey(KeyCode.LeftShift);

        float VelocidadAnimacion = 0f;

        if (!EstaSaltando && EstaMoviendose)
        {
            VelocidadAnimacion = EstaCorriendo ? 1f : 0.5f;
        }

        Animador.SetFloat("Speed", VelocidadAnimacion);
    }
}
