using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraShake : MonoBehaviour
{
    public static PlayerCameraShake Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void StartShake(float duration,float magnitude)
    {
        //Recommend values =0.3,0.5
        StartCoroutine(Shake(duration, magnitude));
    }
    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
