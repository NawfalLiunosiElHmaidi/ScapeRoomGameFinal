using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsWithLock : MonoBehaviour
{
    public Animator door;
    public GameObject openText;
    public GameObject KeyINV;

    public AudioSource doorSound;
    public AudioSource lockedSound;


    public bool inReach;
    public bool unlocked;
    public bool locked;
    public bool hasKey;

    public Outline outline; // Referencia al componente Outline



    void Start()
    {
        if (outline != null)
        {
            outline.enabled = false; // Desactivar el Outline al inicio
        }

        hasKey = false;
        unlocked = false;
        locked = true;
    }

    void OnTriggerEnter(Collider other)
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

    void OnTriggerExit(Collider other)
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





    void Update()
    {
        if(KeyINV.activeInHierarchy)
        {
            locked = false;
            hasKey = true;
        }  
        
        else
        {
            hasKey = false;
        }

        if (hasKey && inReach && Input.GetButtonDown("Interact"))
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
    void DoorOpens ()
    {
        if (unlocked)
        {
            door.SetBool("Open", true);
            door.SetBool("Closed", false);
            doorSound.Play();
        }

    }

    void DoorCloses()
    {
        if (unlocked)
        {
            door.SetBool("Open", false);
            door.SetBool("Closed", true);
        }

    }


}
