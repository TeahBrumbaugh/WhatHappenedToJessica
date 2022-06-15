using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class FinalGameManager : MonoBehaviour
{
    private GeneralMovements moveScript;
    [SerializeField] private GameObject suspectParent;
    [SerializeField] private List<GameObject> suspects;
    [SerializeField] private GameObject evidenceCardParent;

    private GameObject activeCard;
    private bool intShown = false;

    void Start()
    {
        moveScript = GetComponent<GeneralMovements>();

        foreach (Transform child in suspectParent.transform)
            suspects.Add(child.gameObject);
    }

    public void SendToLab()
    {
        activeCard.GetComponent<CardProperties>().SendInfo();
    }

    public bool LabCheck(TextMeshProUGUI[] unknownTxt, List<string> knownTxt)
    {
        int lastOfArray = unknownTxt.Length - 1;

        for (int i = 0; i < lastOfArray; i++)
            RevealText(unknownTxt[i], knownTxt[i]);

        string tagName = unknownTxt[lastOfArray].tag;

        if(tagName == "Untagged")
        {
            RevealText(unknownTxt[lastOfArray], knownTxt[lastOfArray]);
            return true;
        }

        else
        {
            for (int i = 0; i < suspects.Count; i++)
            {
                if (suspects[i].tag == tagName)
                {
                    if (suspects[i].activeInHierarchy)
                    {
                        RevealText(unknownTxt[lastOfArray], knownTxt[lastOfArray]);
                        return true;
                    }

                    else
                    {
                        unknownTxt[lastOfArray].text = "Unidentified";
                        return false;
                    }
                }
            }
        }

        Debug.Log("Something went wrong");
        return false;
    }

    private void RevealText(TextMeshProUGUI txt, string replaceTxt)
    {
        txt.color = Color.black;
        txt.text = replaceTxt;
    }

    public void ShowReport(GameObject report)
    {
        if (intShown)
        {
            var container = activeCard.transform.Find("Scroll/Viewport/Content");

            for (int i = 0; i < container.transform.childCount; i++)
                container.transform.GetChild(i).gameObject.SetActive(true);

            intShown = false;
        }

        if(report.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text != "?")
            report.transform.GetChild(0).gameObject.SetActive(false);

        if (!evidenceCardParent.activeInHierarchy)
            moveScript.MoveTransfer();

        for (int i = 1; i < evidenceCardParent.transform.childCount; i++)
        {
            var childCard = evidenceCardParent.transform.GetChild(i).gameObject;

            if (childCard.name == report.name)
            {
                activeCard = childCard;
                activeCard.SetActive(true);
            }

            else
                childCard.SetActive(false);

        }
    }

    public void ShowInterview(GameObject tagSearch)
    {
        intShown = true;

        string tagName = tagSearch.tag;
        var container = activeCard.transform.Find("Scroll/Viewport/Content");

        for (int i = 0; i < container.transform.childCount; i++)
            if(container.transform.GetChild(i).tag != tagName)
                container.transform.GetChild(i).gameObject.SetActive(false);
    }
}
