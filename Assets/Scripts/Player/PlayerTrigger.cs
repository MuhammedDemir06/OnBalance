using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var interact = collision.gameObject.GetComponent<Interactable>();
        if (interact != null)
            interact.Interact();

        if(collision.gameObject.tag=="Coin")
        {
            GameManager.Instance.UpdateCoin();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interact = collision.GetComponent<Interactable>();
        if (interact != null)
            interact.Interact();
    }
}
