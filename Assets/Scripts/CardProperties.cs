using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardProperties : MonoBehaviour
{
    [SerializeField] public FinalGameManager gmScript;
    [SerializeField] private TextMeshProUGUI[] unknownTxt;
    [SerializeField] private List<string> knownTxt;
    [SerializeField] private GameObject[] completeReveal;
    private Color unknownColor;
    private bool allRevealed = false;

    private void Start()
    {
        ColorUtility.TryParseHtmlString("#A81C07", out unknownColor);

        for (int i = 0; i < unknownTxt.Length; i++)
        {
            knownTxt.Add(unknownTxt[i].text);
            unknownTxt[i].text = "Unknown";
            unknownTxt[i].color = unknownColor;
            
        }
    }

    public  void SendInfo()
    {
        if (gmScript.LabCheck(unknownTxt, knownTxt) && !allRevealed)
        {
            completeReveal[0].SetActive(false);

            for (int i = 1; i < completeReveal.Length; i++)
                completeReveal[i].SetActive(true);

            allRevealed = true;
        }
    }
}
