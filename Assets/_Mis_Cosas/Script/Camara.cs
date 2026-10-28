
using UnityEngine;

public class Camara : MonoBehaviour
{
    public float Sensibilidad = 100;
    public Transform Player;

    float RotacionHorizontal = 0;
    float RotacionVertical = 0;

    void Start()
    {
        // Quitamos cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Recoge eventos
        float ValorX = Input.GetAxis("Mouse X") * Sensibilidad * Time.deltaTime;
        float ValorY = Input.GetAxis("Mouse Y") * Sensibilidad * Time.deltaTime;

        // Acumula y limita eventos
        RotacionHorizontal += ValorX;
        RotacionVertical -= ValorY;
        RotacionVertical = Mathf.Clamp(RotacionVertical, -80f, 80f);

        // Realiza el movimiento
        transform.localRotation = Quaternion.Euler(RotacionVertical, 0f, 0f);

        if (Player != null)
        {
            Player.Rotate(Vector3.up * ValorX);
        }
        else
        {
            Debug.LogWarning("Camara: asigna (Player) en el Inspector.");
        }
    }
}
