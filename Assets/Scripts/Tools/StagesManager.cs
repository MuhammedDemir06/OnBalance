using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StagesManager : MonoBehaviour
{
    public static StagesManager Instance;

    [SerializeField] private int currentStage;
    [SerializeField] private int currentChest;
    [SerializeField] private int maxChest;
    [SerializeField] private float stageBooster = 2f;
    [SerializeField] private Image stageImage;
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private float waitingTime = 15f;
    private bool stagesFinished;
    private bool isEntry;
    [Header("Chest")]
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private Transform[] chestSpawnPoints;
    [Header("Objects")]
    [SerializeField] private ObjectSpawner objectSpawner; 
    [SerializeField] private ObjectSpawner objectSpawner2; 
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        Entry();
    }
    private void Entry()
    {
        currentChest = 0;
        maxChest = 0;
        stageBooster = 0.1f;
        currentStage = 0;
        stageText.text = "Collecter is Starting...";
        isEntry = true;
    }
    private void SpawnChest()
    {
        var newPos = Random.Range(0, chestSpawnPoints.Length);
        Instantiate(chestPrefab, chestSpawnPoints[newPos].position, Quaternion.identity);
    }
    public void UpdateChestCount(bool canUpdate)
    {
        if (!isEntry || !stagesFinished)
        {
            if (canUpdate)
                currentChest += 1;

            stageText.text = $"Stage: {currentStage} || Collect Chests: {currentChest}/{maxChest}";

            if (stageImage.fillAmount > 0f && currentChest == maxChest)
            { 
                Invoke(nameof(NextStage),0.3f);
                Invoke(nameof(SpawnChest), 0.3f);
            }
            else if(stageImage.fillAmount == 0f)
            {
                StartCoroutine(Wait());
            }
            else
                Invoke(nameof(SpawnChest), 0.1f);
        }
    }
    private void NextStage()
    {
        currentStage += 1;
        currentChest = 0;
        maxChest += 1;
        stageImage.fillAmount = 1;
        waitingTime += 1;
        stagesFinished = false;
        stageBooster += 0.001f;

        stageText.text = $"Stage: {currentStage} || Collect Chests: {currentChest}/{maxChest}";

        Debug.Log("Next");
    }
    private void UpdateStageImageFill()
    {
        if (PlayerController.Instance.IsDead || GameManager.Instance.GamePaused)
            return;

        if (stageImage.fillAmount > 0)
            stageImage.fillAmount -= stageBooster * Time.deltaTime;

        if (!isEntry)
            return;

        if (stageImage.fillAmount == 0f && isEntry)
        {
            print("Not Entry");
            isEntry = false;
            stageBooster = 0.001f;
            //
            SpawnChest();

            NextStage();
        }
    }
    private IEnumerator Wait()
    {
        PlayerCameraShake.Instance.StartShake(0.3f, 0.5f);
        stagesFinished = true;
        stageText.text = $"For the Next Stages you need to survive for {waitingTime} seconds...";
        currentChest = 0;
        maxChest += 1;
        objectSpawner.SpawnTime = 2f;
        objectSpawner2.gameObject.SetActive(true);

        yield return new WaitForSeconds(waitingTime);

        objectSpawner.SpawnTime = 3f;
        objectSpawner2.gameObject.SetActive(false);

        NextStage();

        SpawnChest();
    }
    private void Update()
    {
        UpdateStageImageFill();
    }
}
