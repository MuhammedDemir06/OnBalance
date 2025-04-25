using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractStore : MonoBehaviour,Interactable,NotInteractable
{
    [SerializeField] private AnimatedPanel animatedPanel;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private GameManager managerGame;
    [Header("UI")]
    [SerializeField] private List<UpgradeableItemBooster> upgradeableItems;
    [SerializeField] private AnimatedPanel warningPanel;

    public void Interact()
    {
        animatedPanel.Show();
    }
    public void NotInteract()
    {
        animatedPanel.Hide();
    }
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        for (int i = 0; i < upgradeableItems.Count; i++)
        {
            upgradeableItems[i].AmountOfGoldText.text = upgradeableItems[i].AmountOfGold.ToString();
            upgradeableItems[i].CurrentNumText.text = upgradeableItems[i].ClickNum.ToString();
        }
    }
    private void Upgrade(int index)
    {
        if (managerGame.Coin >= upgradeableItems[index].AmountOfGold && upgradeableItems[index].ClickNum <= upgradeableItems[index].MaxClickNum)
        {
            switch (index)
            {
                case 0:
                    playerAttack.DamageAmount += upgradeableItems[index].IncreaseAmount;
                    break;
                case 1:
                    uIManager.PowerBooster += upgradeableItems[index].IncreaseAmount;
                    break;
                case 2:
                    playerController.JumpForce += upgradeableItems[index].IncreaseAmount;
                    break;
                case 3:
                    managerGame.CoinBooster += upgradeableItems[index].IncreaseAmount;
                    break;
            }     
            upgradeableItems[index].ClickNum += 1;
            managerGame.StoreCoin(upgradeableItems[index].AmountOfGold);
            upgradeableItems[index].AmountOfGold += 3;
            upgradeableItems[index].AmountOfGoldText.text = upgradeableItems[index].AmountOfGold.ToString();

            if (upgradeableItems[index].ClickNum < upgradeableItems[index].MaxClickNum)
                upgradeableItems[index].CurrentNumText.text = upgradeableItems[index].ClickNum.ToString();
            else if (upgradeableItems[index].ClickNum == upgradeableItems[index].MaxClickNum)
                upgradeableItems[index].CurrentNumText.text = "Max";

            Debug.Log("Upgraded");
        }
        else
        {
            Debug.Log("Not Enough Coin");
            warningPanel.TextSetting("Not Enough Coin");
        }
    }
    public void UpgradeButton(int index)
    {
        Upgrade(index);
    }
}
[System.Serializable]
public class UpgradeableItemBooster
{
    public string ItemName;
    public int IncreaseAmount;
    public int MaxClickNum;
    [HideInInspector] public int ClickNum;
    public TextMeshProUGUI CurrentNumText;
    public TextMeshProUGUI AmountOfGoldText;
    public int AmountOfGold;
}
