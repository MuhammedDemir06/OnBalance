using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggger : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioSource triggerSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var interact = collision.gameObject.GetComponent<Interactable>();
        if (interact != null)
            interact.Interact();

        if(collision.gameObject.tag=="Coin")
        {
            if (triggerSound.enabled)
                triggerSound.Play();

            GameManager.Instance.UpdateCoin();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interact = collision.GetComponent<Interactable>();
        if (interact != null)
            interact.Interact();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var notInteract = collision.gameObject.GetComponent<NotInteractable>();
        if (notInteract != null)
            notInteract.NotInteract();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        var notInteract = collision.gameObject.GetComponent<NotInteractable>();
        if (notInteract != null)
            notInteract.NotInteract();
    }
}
