using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("Exit Scene")]
    [SerializeField] private GameObject transitionEnd;
    [SerializeField] private float transitionTimeEnd = 2f;
    [Header("Entry Scene")]
    [SerializeField] private GameObject transitionEntry;
    [SerializeField] private float transitionTimeEntry = 1f;
    private void Start()
    {
        transitionEntry.SetActive(true);
        Invoke(nameof(TransitionEntry), transitionTimeEntry);
    }
    private void TransitionEntry()
    {
        transitionEntry.SetActive(false);
    }
    public void LoadSceneWithTransition(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }
    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        transitionEnd.SetActive(true);

        yield return new WaitForSeconds(transitionTimeEnd);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
    }

    public void Exit()
    {
        transitionEnd.SetActive(true);
        Invoke(nameof(ExitGame), 1f);
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
