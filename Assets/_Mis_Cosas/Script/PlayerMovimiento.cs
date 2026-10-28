using UnityEngine;

public class PlayerMovimiento : MonoBehaviour
{
    [Header("Referencia")]
    [SerializeField] private Transform Camara;
    private CharacterController Controlador;
    private bool EstaEnSuelo;

    [Header("Movimiento")]
    [SerializeField] private bool UsarGetAxisRaw = true;
    [SerializeField] private float VelocidadMovimiento = 5f;
    [SerializeField] private float VelocidadCarrera = 8f;
    private float VelocidadActual;

    [Header("Salto")]
    [SerializeField] private float FuerzaSalto = 2f;

    [Header("Gravedad")]
    [SerializeField] private float Gravedad = -9f;
    private Vector3 VelocidadVertical;

    private void Awake()
    {
        // Busca el CharacterController del jugador
        Controlador = GetComponent<CharacterController>();

        // Obtiene la cámara principal si no se ha asignado
        if (Camara == null && Camera.main != null)
        {
            Camara = Camera.main.transform;
        }
    }

    private void Update()
    {
        // Guarda el estado del suelo antes de realizar movimientos
        if (Controlador == null)
        {
            return;
        }

        EstaEnSuelo = Controlador.isGrounded;

        MoverJugadorEnPlano();
        GestionarSalto();
        AplicarGravedad();
    }

    private void MoverJugadorEnPlano()
    {
        // Recoge los eventos del WASD
        float ValorHorizontal = UsarGetAxisRaw
            ? Input.GetAxisRaw("Horizontal")
            : Input.GetAxis("Horizontal");

        float ValorVertical = UsarGetAxisRaw
            ? Input.GetAxisRaw("Vertical")
            : Input.GetAxis("Vertical");

        // Comprueba que existen los componentes necesarios
        if (Controlador == null || Camara == null)
        {
            return;
        }

        // Obtiene las direcciones de la cámara
        Vector3 adelanteCamara = Camara.forward;
        Vector3 derechaCamara = Camara.right;

        // Elimina la componente vertical
        adelanteCamara.y = 0f;
        derechaCamara.y = 0f;

        // Normaliza las direcciones
        adelanteCamara.Normalize();
        derechaCamara.Normalize();

        // Calcula la dirección del movimiento
        Vector3 direccionPlano =
            (derechaCamara * ValorHorizontal) +
            (adelanteCamara * ValorVertical);

        // Evita moverse más rápido en diagonal
        if (direccionPlano.sqrMagnitude > 1f)
        {
            direccionPlano.Normalize();
        }

        // Comprueba si el jugador está corriendo
        VelocidadActual = Input.GetKey(KeyCode.LeftShift)
            ? VelocidadCarrera
            : VelocidadMovimiento;

        // Calcula el desplazamiento horizontal
        Vector3 desplazamientoXZ =
            direccionPlano * VelocidadActual * Time.deltaTime;

        // Mueve al jugador
        Controlador.Move(desplazamientoXZ);
    }

    private void GestionarSalto()
    {
        // Solo permite saltar si el jugador está en el suelo
        if (EstaEnSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            // Calcula la velocidad inicial del salto
            VelocidadVertical.y = Mathf.Sqrt(FuerzaSalto * -2f * Gravedad);
        }
    }

    private void AplicarGravedad()
    {
        // Mantiene al jugador pegado al suelo
        if (EstaEnSuelo && VelocidadVertical.y < 0)
        {
            VelocidadVertical.y = -2f;
        }

        // Aplica la gravedad
        VelocidadVertical.y += Gravedad * Time.deltaTime;

        // Aplica el movimiento vertical
        Controlador.Move(VelocidadVertical * Time.deltaTime);
    }
}