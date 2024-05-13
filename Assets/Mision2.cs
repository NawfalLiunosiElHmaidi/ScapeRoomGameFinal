using UnityEngine;
using TMPro;

public class Mision2 : MonoBehaviour
{
    public TextMeshProUGUI jarText; // Referencia al texto de b�squeda de jarr�n
    public ManagerMision managerMision; // Referencia al ManagerMision
    public Mision3 mision3; // Referencia al script Mision3

    private bool canCollectJar = false; // Variable para controlar si se puede recolectar el objeto
    private Transform jarToCollect; // Referencia al objeto que se puede recolectar
    private bool missionCompleted = false; // Variable para controlar si la misi�n est� completada

    private void Start()
    {
        jarText.gameObject.SetActive(false);
    }

    public void ActivateJarText()
    {
        jarText.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (missionCompleted)
        {
            DisableJarText();
            mision3.ActivateTelText();
        }
        else
        {
            // Raycast para detectar objetos
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2))
            {
                // Verificar si el objeto detectado es el que queremos recolectar
                if (hit.collider.gameObject.name == "FloralGoldJar")
                {
                    jarToCollect = hit.transform;
                    canCollectJar = true;
                }
            }

            // Recolectar el objeto al presionar la tecla E
            if (canCollectJar && Input.GetKeyDown(KeyCode.E))
            {
                CollectJar();
            }
        }
    }

    private void CollectJar()
    {
        // Destruir el objeto recolectado
        Destroy(jarToCollect.gameObject);

        // Marcar la misi�n como completada
        missionCompleted = true;

        // Llamar al m�todo MissionCompleted del ManagerMision
        managerMision.MissionCompleted();
    }

    private void DisableJarText()
    {
        // Desactivar el texto de b�squeda del jarr�n
        jarText.gameObject.SetActive(false);
    }
}
