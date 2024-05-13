using UnityEngine;
using TMPro;
using System.Media;
using UnityEngine.Audio;

public class Mision4 : MonoBehaviour
{
    public TextMeshProUGUI GenText; // Referencia al texto de búsqueda de jarrón
    public ManagerMision managerMision; // Referencia al ManagerMision
    //public Mision5 mision5; // Referencia al script Mision5

    private bool canCollectGen = false; // Variable para controlar si se puede recolectar el objeto
    private Transform GenToCollect; // Referencia al objeto que se puede recolectar
    private bool missionCompleted = false; // Variable para controlar si la misión está completada


    public AudioSource audioSource; // Referencia al AudioSource para reproducir el sonido del teléfono
    public AudioClip GenSound; // Sonido del teléfono al ser recogido
    private bool soundPlayed = false; // Variable para controlar si se ha reproducido el sonido del teléfono

    private void Start()
    {
        GenText.gameObject.SetActive(false);
    }

    public void ActivateGenText()
    {
        GenText.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (missionCompleted)
        {
            DisableJarText();
            //mision5.ActivateTelText();
        }
        else
        {
            // Raycast para detectar objetos
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2))
            {
                // Verificar si el objeto detectado es el que queremos recolectar
                if (hit.collider.gameObject.name == "Generador")
                {
                    GenToCollect = hit.transform;
                    canCollectGen = true;
                }
            }

            // Recolectar el objeto al presionar la tecla E
            if (canCollectGen && Input.GetKeyDown(KeyCode.E))
            {
                CollectGen();
            }
        }
    }

    private void CollectGen()
    {
        if (!soundPlayed)
        {
            audioSource.PlayOneShot(GenSound);
            soundPlayed = true;
        }

        // Marcar la misión como completada
        missionCompleted = true;

        // Llamar al método MissionCompleted del ManagerMision
        managerMision.MissionCompleted();
    }

    private void DisableJarText()
    {
        // Desactivar el texto de búsqueda del jarrón
        GenText.gameObject.SetActive(false);
    }
}
