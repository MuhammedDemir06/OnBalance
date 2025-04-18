using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHealth : MonoBehaviour
{
    public int Health = 50;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var takeHealth = collision.GetComponent<Healthable>();
        if (takeHealth != null)
            takeHealth.TakeHealth(Health);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var takeHealth = collision.gameObject.GetComponent<Healthable>();
        if (takeHealth != null)
            takeHealth.TakeHealth(Health);
    }
}
