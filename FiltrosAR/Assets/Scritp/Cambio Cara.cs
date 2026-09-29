using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CambioCara : MonoBehaviour
{
    [SerializeField] private ARFaceManager faceManager;
    private int indexModelo = 0;
    private int cantidadModelos = 0;

    private void Awake()
    {
        if (faceManager == null)
            faceManager = FindAnyObjectByType<ARFaceManager>();
    }

    public void CambiarObjeto()
    {
        // Recorremos todos los rostros detectados
        foreach (var face in faceManager.trackables)
        {
            // Buscamos el contenedor que creamos llamado "Modelos"
            Transform contenedorModelos = face.transform.Find("Modelos");

            if (contenedorModelos != null)
            {
                cantidadModelos = contenedorModelos.childCount;
                if (cantidadModelos == 0) return;

                // 1. Apagamos todos los modelos 3D
                for (int i = 0; i < cantidadModelos; i++)
                {
                    contenedorModelos.GetChild(i).gameObject.SetActive(false);
                }

                // 2. Prendemos solo el modelo que corresponde al índice actual
                if (indexModelo < cantidadModelos)
                {
                    contenedorModelos.GetChild(indexModelo).gameObject.SetActive(true);
                }
            }
        }

        // 3. Aumentamos el índice para el próximo clic
        indexModelo++;

        // Si llegamos al final de la lista de modelos, volvemos al inicio (o podemos poner un estado "vacío")
        if (indexModelo >= cantidadModelos)
        {
            indexModelo = 0;
        }
    }
}