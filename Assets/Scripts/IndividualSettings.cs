using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IndividualSettings : MonoBehaviour
{
    [SerializeField] private GameObject reportPage;
    [SerializeField] private GeneralMovements transferScript;
    [SerializeField] private GameObject[] revealedObjects;
    private float waitTime = 1;
    private Vector3 origPos;
    private Vector3 origScl;
    private Button btnSettings;
    private Canvas layerOverride;

    private void Start()
    {
        btnSettings = GetComponent<Button>();
        origPos = transform.position;
        origScl = transform.localScale;
    }

    public void TransferInfo()
    {
        if (btnSettings.interactable)
        {
            btnSettings.interactable = false;

            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            reportPage.SetActive(true);

            layerOverride = this.gameObject.AddComponent<Canvas>();
            layerOverride.overrideSorting = true;
            layerOverride.sortingOrder = 2;

            transferScript.Clicked(this.gameObject, origPos, origScl);
        }
        
    }

    public void MakeAvaliable()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        Destroy(this.GetComponent<Canvas>());
        reportPage.SetActive(false);
    }

    public void RevealEvidence()
    {
        StartCoroutine(WaitToReveal());
    }

    private IEnumerator WaitToReveal()
    {
        yield return new WaitForSeconds(waitTime);

        if(reportPage.name != "JA Bio")
        {
            reportPage.transform.GetChild(1).gameObject.SetActive(false);
            reportPage.transform.GetChild(2).gameObject.SetActive(true);
        }

        for (int i = 0; i < revealedObjects.Length; i++)
            revealedObjects[i].SetActive(true);
    }

}
