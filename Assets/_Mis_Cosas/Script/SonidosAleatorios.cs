
using UnityEngine;
using System.Collections;

public class SonidosAleatorios : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform Jugador;
    [SerializeField] private AudioSource FuenteAudio;
    [SerializeField] private AudioClip[] Sonidos;

    [Header("Frecuencia")]
    [SerializeField] private float TiempoMinimo = 8f;
    [SerializeField] private float TiempoMaximo = 25f;

    [Header("Distancia")]
    [SerializeField] private float DistanciaMinima = 20f;
    [SerializeField] private float DistanciaMaxima = 100f;

    [Header("Volumen")]
    [SerializeField] private float VolumenMinimo = 0.4f;
    [SerializeField] private float VolumenMaximo = 1f;

    private void Start()
    {
        StartCoroutine(ReproducirSonidos());
    }

    private IEnumerator ReproducirSonidos()
    {
        while (true)
        {
            // Espera un intervalo aleatorio
            yield return new WaitForSeconds(
                Random.Range(TiempoMinimo, TiempoMaximo)
            );

            // Comprueba las referencias
            if (Jugador == null || FuenteAudio == null ||
                Sonidos == null || Sonidos.Length == 0)
            {
                continue;
            }

            // Elige un sonido aleatorio
            AudioClip sonido = Sonidos[
                Random.Range(0, Sonidos.Length)
            ];

            if (sonido == null)
            {
                continue;
            }

            // Calcula una posición aleatoria alrededor del jugador
            Vector2 direccion = Random.insideUnitCircle.normalized;

            float distancia = Random.Range(
                DistanciaMinima,
                DistanciaMaxima
            );

            Vector3 posicion = Jugador.position + new Vector3(
                direccion.x * distancia,
                0f,
                direccion.y * distancia
            );

            // Coloca el origen del sonido
            FuenteAudio.transform.position = posicion;

            // Asigna el sonido y su volumen
            FuenteAudio.clip = sonido;
            FuenteAudio.volume = Random.Range(
                VolumenMinimo,
                VolumenMaximo
            );

            // Reproduce el sonido
            FuenteAudio.Play();
        }
    }
}
