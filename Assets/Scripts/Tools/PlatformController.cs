using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private float speed = 0.3f;
    [SerializeField] private Transform platform;
    [SerializeField] private Transform nextDirection;
    [SerializeField] private Transform firstDirection;
    [HideInInspector] public bool CanUpgrade;
    [SerializeField] private float waitingTime = 35f;

    private void Start()
    {
        CanUpgrade = false;
        StartCoroutine(DirectionChanger());
    }
    private IEnumerator DirectionChanger()
    {
        while(!PlayerController.Instance.IsDead)
        {
            yield return new WaitForSeconds(waitingTime);
            CanUpgrade = !CanUpgrade;
        }
    }
    private void Next()
    {
        if (GameManager.Instance.GamePaused)
            return;

        if(CanUpgrade)
        {
            platform.position = Vector2.Lerp(platform.position, nextDirection.position, speed * Time.deltaTime);
        }
        else
        {
            platform.position = Vector2.Lerp(platform.position, firstDirection.position, speed * Time.deltaTime);
        }
    }
    private void Update()
    {
        Next();
    }
}
