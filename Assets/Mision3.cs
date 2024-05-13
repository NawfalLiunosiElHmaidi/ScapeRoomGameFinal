using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mision3 : MonoBehaviour
{
    public TextMeshProUGUI TelText; // Referencia al texto de búsqueda de jarrón
    public ManagerMision managerMision; // Referencia al ManagerMision

    public AudioSource audioSource; // Referencia al AudioSource para reproducir el sonido del teléfono
    public AudioClip phonePickupSound; // Sonido del teléfono al ser recogido
    public Mision4 mision4; // Referencia al script Mision3

    private bool canCollectTel = false; // Variable para controlar si se puede recolectar el objeto
    private Transform TelToCollect; // Referencia al objeto que se puede recolectar
    private bool missionCompleted = false; // Variable para controlar si la misión está completada
    private bool soundPlayed = false; // Variable para controlar si se ha reproducido el sonido del teléfono

    private void Start()
    {
        TelText.gameObject.SetActive(false);
    }

    public void ActivateTelText()
    {
        TelText.gameObject.SetActive(true);
    }

    private void Update()
    {
        // Si la misión está completada, desactivar el texto
        if (missionCompleted)
        {
            DisableJarText();
            mision4.ActivateGenText();
        }
        else
        {
            // Raycast para detectar objetos
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2))
            {
                // Verificar si el objeto detectado es el que queremos recolectar
                if (hit.collider.gameObject.name == "Telefono")
                {
                    TelToCollect = hit.transform;
                    canCollectTel = true;
                }
            }

            // Recolectar el objeto al presionar la tecla E
            if (canCollectTel && Input.GetKeyDown(KeyCode.E))
            {
                CollectTel();
            }
        }
    }

    private void CollectTel()
    {
        // Reproducir el sonido del teléfono si no se ha reproducido aún
        if (!soundPlayed)
        {
            audioSource.PlayOneShot(phonePickupSound);
            soundPlayed = true;
        }

        // Destruir el objeto recolectado
        Destroy(TelToCollect.gameObject);

        // Marcar la misión como completada
        missionCompleted = true;

        // Llamar al método MissionCompleted del ManagerMision
        managerMision.MissionCompleted();
    }

    private void DisableJarText()
    {
        // Desactivar el texto de búsqueda del jarrón
        TelText.gameObject.SetActive(false);
    }
}
