using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ConvictChecks : MonoBehaviour
{
    [SerializeField] private GeneralMovements moveScript;
    [SerializeField] private GameObject boardSuspectsParent;
    [SerializeField] private GameObject accuseSuspectsParent;
    [SerializeField] private TextMeshProUGUI suspectName;
    [SerializeField] private GameObject winWindow;
    [SerializeField] private GameObject loseWindow;
    [SerializeField] private GameObject loseSuspect;
    [SerializeField] private AudioSource[] convictSFX;
    [SerializeField] private AudioSource clickSFX;
    private string perpetrator = "Michael Anderson";

    public void ToggleWindow()
    {
        if (this.gameObject.activeInHierarchy)
            StartCoroutine(ToggleConstant(false, -10));

        else
        {
            this.gameObject.SetActive(true);

            StartCoroutine(ToggleConstant(true, 1));
        }
    }

    private IEnumerator ToggleConstant(bool isShown, int yValue)
    {
        moveScript.SetClickable(!isShown);

        transform.DOMoveY(yValue, 1).SetEase(Ease.InOutCubic);

        if (isShown)
        {
            moveScript.PlayPaperSFX();
            for (int i = 1; i < boardSuspectsParent.transform.childCount; i++)
                if (boardSuspectsParent.transform.GetChild(i).gameObject.activeInHierarchy)
                    accuseSuspectsParent.transform.GetChild(i - 1).gameObject.SetActive(true);
        }

        else
        {
            yield return new WaitForSeconds(1);
            this.gameObject.SetActive(false);
        }
    }

    public void SuspectChange(GameObject suspect)
    {
        clickSFX.Play();
        suspectName.text = suspect.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        loseSuspect.GetComponent<Image>().sprite = suspect.GetComponent<Image>().sprite;
        loseSuspect.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = suspectName.text;
    }

    public void SuspectCheck()
    {
        if(suspectName.text != "[ choose picture ]")
        {
            convictSFX[0].Play();
            convictSFX[1].Play();

            if (suspectName.text == perpetrator)
                winWindow.SetActive(true);

            else
                loseWindow.SetActive(true);

            this.gameObject.SetActive(false);
        }
    }
}