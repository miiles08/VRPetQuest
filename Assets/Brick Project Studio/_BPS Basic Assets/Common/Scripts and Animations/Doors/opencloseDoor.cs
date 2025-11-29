using System.Collections;
using UnityEngine;

namespace SojaExiles
{
    public class opencloseDoor : MonoBehaviour
    {
        public Animator openandclose;
        public bool open;
        private bool canInteract = true;

        void Start()
        {
            open = false;
        }

        void OpenDoor()
        {
            if (!open)
                StartCoroutine(opening());
            else
                StartCoroutine(closing());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Hand") && canInteract)
            {
                StartCoroutine(DoorInteraction());
            }
        }

        IEnumerator DoorInteraction()
        {
            canInteract = false;
            OpenDoor();
            yield return new WaitForSeconds(1f); // cooldown
            canInteract = true;
        }

        IEnumerator opening()
        {
            Debug.Log("You are opening the door");
            openandclose.Play("Opening");
            open = true;
            yield return new WaitForSeconds(0.5f);
        }

        IEnumerator closing()
        {
            Debug.Log("You are closing the door");
            openandclose.Play("Closing");
            open = false;
            yield return new WaitForSeconds(0.5f);
        }
    }
}