using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Object Spawner")]
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private float spawnRangeX;
    [SerializeField] private float spawnTime = 3f;
    private void Start()
    {
        StartCoroutine(Spawner());
    }
    private IEnumerator Spawner()
    {
        while(!PlayerController.Instance.IsDead)
        {
            Instantiate(objectPrefab, new Vector2(Random.Range(spawnRangeX, -spawnRangeX), transform.position.y), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
