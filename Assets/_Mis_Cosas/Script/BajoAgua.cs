using UnityEngine;

public class UnderwaterEffect : MonoBehaviour
{
    [Header("Agua")]
    public Transform waterCube;

    [Header("Niebla submarina")]
    public float fogStart = 0.5f;
    public float fogEnd = 4f;

    private bool underwater = false;

    void Update()
    {
        if (waterCube == null)
            return;

        // Calcular la altura superior del cubo
        float waterLevel = waterCube.position.y
                         + waterCube.lossyScale.y / 2f;

        bool isUnderwater = transform.position.y < waterLevel;

        if (isUnderwater != underwater)
        {
            underwater = isUnderwater;

            if (underwater)
            {
                // Activar niebla submarina
                RenderSettings.fog = true;
                RenderSettings.fogMode = FogMode.Linear;

                // Color gris turbio
                RenderSettings.fogColor =
                    new Color(0.40f, 0.42f, 0.41f);

                // Distancia de la niebla
                RenderSettings.fogStartDistance = fogStart;
                RenderSettings.fogEndDistance = fogEnd;
            }
            else
            {
                // Desactivar niebla al salir del agua
                RenderSettings.fog = false;
            }
        }
    }
}