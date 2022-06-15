using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GeneralMovements : MonoBehaviour
{
    private enum transformProp {oldPos, newPos, oldScl, newScl};

    [SerializeField] private GameObject suspectParent;
    [SerializeField] private GameObject picParent;
    [SerializeField] private List<Button> picSettings;

    [SerializeField] private Image blackOveray;

    [SerializeField] private GameObject paperReport;
    [SerializeField] private GameObject sidePapers;
    private Vector3[] reportPos = new Vector3[2];
    [SerializeField] private Transform reportShownPos;

    private GameObject viewedPicture;
    private Vector3[] picPos = new Vector3[4];
    [SerializeField] private Transform picShownPos;
    [SerializeField] private AudioSource[] paperSFX;

    private float tweenDuration = 0.5f;

    private bool canExit = false;

    private void Start()
    {
        foreach (Transform child in picParent.transform)
            picSettings.Add(child.GetComponent<Button>());

        foreach (Transform child in suspectParent.transform)
            picSettings.Add(child.GetComponent<Button>());

        reportPos[(int)transformProp.oldPos] = paperReport.GetComponent<RectTransform>().position;
        reportPos[(int)transformProp.newPos] = reportShownPos.position;
        picPos[(int)transformProp.newPos] = picShownPos.position;
        picPos[(int)transformProp.newScl] = picShownPos.localScale;
    }

    public void Clicked(GameObject clickedPicture, Vector3 origPos, Vector3 origScl)
    {
        PlayPaperSFX();

        SetClickable(false);
        viewedPicture = clickedPicture;

        picPos[(int)transformProp.oldPos] = origPos;
        picPos[(int)transformProp.oldScl] = origScl;

        paperReport.SetActive(true);
        setPosition(picPos[(int)transformProp.newPos], picPos[(int)transformProp.newScl], Random.Range(1f, 4f), reportPos[(int)transformProp.newPos], 0.75f);
        StartCoroutine(EnableInteraction());
    }

    public void SetClickable(bool state)
    {
        for (int i = 0; i < picSettings.Count; i++)
            picSettings[i].interactable = state;
    }

    private IEnumerator EnableInteraction()
    {
        yield return new WaitForSeconds(tweenDuration);
        canExit = true;
    }

    public void Exit()
    {
        if (canExit)
        {
            canExit = false;
            setPosition(picPos[(int)transformProp.oldPos], picPos[(int)transformProp.oldScl], Random.Range(-1.5f, 1.5f), reportPos[(int)transformProp.oldPos], 0);
            StartCoroutine(MoveEC(false));
            StartCoroutine(ReturnStates());
        }
    }

    public void MoveTransfer()
    {
        PlayPaperSFX();
        StartCoroutine(MoveEC(true));
    }

    private IEnumerator MoveEC(bool isShown)
    {
        float xValue = -5.12f * 3;

        if (isShown)
        {
            sidePapers.SetActive(true);
            xValue = -5.12f;
        }

        sidePapers.transform.DOMoveX(xValue, 1, false).SetEase(Ease.InOutCubic);

        if (isShown == false)
        {
            yield return new WaitForSeconds(1);
            sidePapers.SetActive(false);
        }
    }

    public IEnumerator ReturnStates()
    {
        yield return new WaitForSeconds(tweenDuration * 2);

        viewedPicture.SetActive(false);
        viewedPicture.SetActive(true);
        paperReport.SetActive(false);
        SetClickable(true);
        viewedPicture.GetComponent<IndividualSettings>().MakeAvaliable();
    }
     
    private void setPosition(Vector3 imgPos, Vector3 imgScl, float newRotation, Vector3 reportPos, float overlayFade)
    {
        SetBlackOverlay(overlayFade);
        viewedPicture.transform.GetChild(1).GetComponent<TextMeshProUGUI>().DOFade(overlayFade, tweenDuration / 2).SetEase(Ease.InOutQuad);

        viewedPicture.transform.DOMove(imgPos, tweenDuration).SetEase(Ease.InOutQuad);
        viewedPicture.transform.DORotate(new Vector3(0, 0, newRotation), tweenDuration, RotateMode.Fast).SetEase(Ease.InOutQuad);
        viewedPicture.transform.DOScale(imgScl, tweenDuration).SetEase(Ease.InOutQuad);

        paperReport.transform.DOMove(reportPos, tweenDuration).SetEase(Ease.InOutQuad);
    }

    public void SetBlackOverlay(float overlayFade)
    {
        blackOveray.DOFade(overlayFade, tweenDuration / 2).SetEase(Ease.InOutQuad);
    }

    public void PlayPaperSFX()
    {
        paperSFX[Random.Range(0, paperSFX.Length)].Play();
    }
}
