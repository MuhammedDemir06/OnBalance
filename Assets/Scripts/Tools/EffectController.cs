using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] private float deleteTime = 0.8f;
    private void Start()
    {
        Destroy(gameObject, deleteTime);
    }
}
