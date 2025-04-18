using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private GameObject objectEffect;
    [SerializeField] private float deleteTime = 0.8f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (objectEffect != null)
            Instantiate(objectEffect, transform.position, Quaternion.identity);
        Destroy(gameObject,deleteTime);
    }
}
