using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class ChangeScenes : MonoBehaviour
{
    [SerializeField] private GameObject revealAfterScenesObj;
    [SerializeField] private GameObject crimeSceneBoard;
    private float tweenDuration = 0.5f;
    private float waitTime = 3;
    private bool isAtScene = false;
    private string currentSceneName;
    [SerializeField] private AudioSource clickSFX;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
        Destroy(revealAfterScenesObj);
    }

    public void LoadScene(string sceneName)
    {
        this.gameObject.SetActive(true);

        if (isAtScene)
            sceneName = "Crime Board";
        else
        {
            clickSFX.Play();
            currentSceneName = sceneName;
        }

        transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = sceneName;

        StartCoroutine(WaitingLoad());
    }

    private IEnumerator WaitingLoad()
    {
        Fading(1);

        yield return new WaitForSeconds(waitTime);

        if(isAtScene)
        {
            isAtScene = false;
            crimeSceneBoard.SetActive(true);
            SceneManager.UnloadSceneAsync(currentSceneName);
        }

        else
        {
            isAtScene = true;
            crimeSceneBoard.SetActive(false);
            SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive); // else try additive
        }

        Fading(0);

        yield return new WaitForSeconds(waitTime / 2);
        this.gameObject.SetActive(false);
    }

    private void Fading(int aNum)
    {
        transform.GetChild(0).gameObject.GetComponent<Image>().DOFade(aNum, tweenDuration).SetEase(Ease.InOutQuad);
        transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().DOFade(aNum, tweenDuration).SetEase(Ease.InOutQuad);
        transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().DOFade(aNum, tweenDuration).SetEase(Ease.InOutQuad);
    }
}
