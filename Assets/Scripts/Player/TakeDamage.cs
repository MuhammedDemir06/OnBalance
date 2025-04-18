using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public int Damage = 50;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var takeDamage = collision.gameObject.GetComponent<Damageable>();
        if (takeDamage != null)
            takeDamage.Damage(Damage);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var takeDamage = collision.GetComponent<Damageable>();
        if (takeDamage != null)
            takeDamage.Damage(Damage);
    }
}
