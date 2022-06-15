using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RevealAfterScenes : MonoBehaviour
{
    [SerializeField] private ChangeScenes loadScript;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void RecieveData(string sceneName)
    {
        RevealData(sceneName);
    }

    private void RevealData(string sceneName)
    {
        loadScript.LoadScene(sceneName);
    }

}
