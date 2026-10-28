using UnityEngine;

public class ApplyBridgeMaterial : MonoBehaviour
{
    [SerializeField] private Material materialPuente;

    [ContextMenu("Aplicar material a todo el puente")]
    private void AplicarMaterial()
    {
        if (materialPuente == null)
        {
            Debug.LogWarning("Asigna primero el material.");
            return;
        }

        Renderer[] renderers =
            GetComponentsInChildren<Renderer>(true);

        foreach (Renderer renderer in renderers)
        {
            renderer.sharedMaterial = materialPuente;
        }

        Debug.Log($"Material aplicado a {renderers.Length} renderers.");
    }
}