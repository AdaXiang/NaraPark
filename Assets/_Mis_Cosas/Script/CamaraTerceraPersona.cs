
using UnityEngine;

public class CamaraTerceraPersona : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform Objetivo;

    [Header("Distancia y altura")]
    [SerializeField] private float Distancia = 5f;

    [Header("Control del raton")]
    [SerializeField] private float Sensibilidad = 150f;
    [SerializeField] private float RotacionMinima = -30f;
    [SerializeField] private float RotacionMaxima = 65f;

    private float RotacionHorizontal;
    private float RotacionVertical = 15f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (Objetivo != null)
            RotacionHorizontal = Objetivo.root.eulerAngles.y;
    }

    private void LateUpdate()
    {
        if (Objetivo == null)
            return;

        // Control del raton
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        RotacionHorizontal += MouseX * Sensibilidad * Time.deltaTime;
        RotacionVertical -= MouseY * Sensibilidad * Time.deltaTime;

        RotacionVertical = Mathf.Clamp(
            RotacionVertical,
            RotacionMinima,
            RotacionMaxima
        );

        // Calcula la rotacion de la camara
        Quaternion Rotacion = Quaternion.Euler(
            RotacionVertical,
            RotacionHorizontal,
            0f
        );

        // Coloca la camara detras del objetivo
        Vector3 Posicion = Objetivo.position -
            (Rotacion * Vector3.forward * Distancia);

        transform.position = Posicion;
        transform.rotation = Rotacion;
    }
}
