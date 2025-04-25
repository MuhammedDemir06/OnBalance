using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour,Damageable
{
    [Header("Health")]
    [SerializeField] private int healthAmount = 10;
    [SerializeField] private Image healthImage;
    [SerializeField] private GameObject effectPrefab;
    public void Damage(int damageAmount)
    {
        healthAmount = healthAmount - damageAmount;
        healthImage.fillAmount -= (float)healthAmount / 10;
        if (healthAmount<=0)
        {
            Invoke(nameof(TakeDamage), 0.6f);
        }
    }    
    private void TakeDamage()
    {
        Debug.Log("Destroyed Chest");
        Instantiate(effectPrefab, transform.position, Quaternion.identity);
        StagesManager.Instance.UpdateChestCount(true);
        Destroy(gameObject);
    }
}
