using UnityEngine;
using TMPro;
using UnityEngine.Events;
using NavKeypad;

public class DoorsWithKeypad : MonoBehaviour
{
    public Animator door;
    public GameObject openText;
    public AudioSource doorSound;
    public AudioSource lockedSound;
    public Outline outline; // Referencia al componente Outline

    public bool inReach;
    public bool unlocked;
    public bool locked;
    public bool accessGranted;

    private Keypad keypad; // Referencia al Keypad

    private void Start()
    {
        if (outline != null)
        {
            outline.enabled = false; // Desactivar el Outline al inicio
        }

        unlocked = false;
        locked = true;
        accessGranted = false;

        // Buscar el componente Keypad en cualquier objeto del escenario
        keypad = FindObjectOfType<Keypad>();
        if (keypad != null)
        {
            // Suscribirse a los eventos del Keypad
            keypad.OnAccessGranted.AddListener(GrantAccess);
            keypad.OnAccessDenied.AddListener(DenyAccess);
        }
        else
        {
            Debug.LogWarning("No se encontró el componente Keypad en el escenario.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
            if (outline != null)
            {
                outline.enabled = true; // Activar el Outline al entrar en el rango de alcance
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
            if (outline != null)
            {
                outline.enabled = false; // Desactivar el Outline al salir del rango de alcance
            }
        }
    }

    private void Update()
    {
        if (accessGranted && inReach && Input.GetButtonDown("Interact"))
        {
            unlocked = true;
            DoorOpens();
        }
        else
        {
            DoorCloses();
        }

        if (locked && inReach && Input.GetButtonDown("Interact"))
        {
            lockedSound.Play();
        }
    }

    private void GrantAccess()
    {
        accessGranted = true;
    }

    private void DenyAccess()
    {
        // Manejar el acceso denegado
        Debug.Log("Acceso denegado.");
        // Aquí puedes agregar cualquier acción adicional que desees cuando se deniegue el acceso, como reproducir un sonido, mostrar un mensaje, etc.
    }

    private void DoorOpens()
    {
        if (unlocked)
        {
            door.SetBool("Open", true);
            door.SetBool("Closed", false);
            doorSound.Play();
        }
    }

    private void DoorCloses()
    {
        if (unlocked)
        {
            door.SetBool("Open", false);
            door.SetBool("Closed", true);
        }
    }
}


