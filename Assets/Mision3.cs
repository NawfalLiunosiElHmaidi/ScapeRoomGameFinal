using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mision3 : MonoBehaviour
{
    public TextMeshProUGUI TelText; // Referencia al texto de b�squeda de jarr�n
    public ManagerMision managerMision; // Referencia al ManagerMision

    public AudioSource audioSource; // Referencia al AudioSource para reproducir el sonido del tel�fono
    public AudioClip phonePickupSound; // Sonido del tel�fono al ser recogido
    public Mision4 mision4; // Referencia al script Mision3

    private bool canCollectTel = false; // Variable para controlar si se puede recolectar el objeto
    private Transform TelToCollect; // Referencia al objeto que se puede recolectar
    private bool missionCompleted = false; // Variable para controlar si la misi�n est� completada
    private bool soundPlayed = false; // Variable para controlar si se ha reproducido el sonido del tel�fono

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
        // Si la misi�n est� completada, desactivar el texto
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
        // Reproducir el sonido del tel�fono si no se ha reproducido a�n
        if (!soundPlayed)
        {
            audioSource.PlayOneShot(phonePickupSound);
            soundPlayed = true;
        }

        // Destruir el objeto recolectado
        Destroy(TelToCollect.gameObject);

        // Marcar la misi�n como completada
        missionCompleted = true;

        // Llamar al m�todo MissionCompleted del ManagerMision
        managerMision.MissionCompleted();
    }

    private void DisableJarText()
    {
        // Desactivar el texto de b�squeda del jarr�n
        TelText.gameObject.SetActive(false);
    }
}
