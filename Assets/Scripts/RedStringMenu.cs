using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RedStringMenu : MonoBehaviour
{
    [SerializeField] private MainMenu screenChange;

    [SerializeField] private GameObject connectionPointParent;
    [SerializeField] private GameObject stringConnectionParent;
    private Transform[] connectionPoints;
    private LineRenderer[] stringConnections;
    private int CPcount;

    [SerializeField] private TextMeshProUGUI questionMark;
    [SerializeField] private TextMeshProUGUI playTxt;
    [SerializeField] private GameObject overlay;
    [SerializeField] private AudioSource clickSFX;

    void Start()
    {
        connectionPoints = connectionPointParent.GetComponentsInChildren<Transform>();
        stringConnections = stringConnectionParent.GetComponentsInChildren<LineRenderer>();
        CPcount = connectionPointParent.transform.childCount;
    }

    private void OnMouseEnter()
    {
        ToggleHoverAdditions(true);

        for (int i = 0; i < stringConnections.Length; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                int randomNum = Random.Range(0, CPcount);
                var connectionPosition = new Vector3(connectionPoints[randomNum].position.x, connectionPoints[randomNum].position.y, 0);
                stringConnections[i].SetPosition(j, connectionPosition);
            }

        }
    }

    private void OnMouseExit()
    {
        ToggleHoverAdditions(false);
    }

    private void ToggleHoverAdditions(bool state)
    {
        stringConnectionParent.SetActive(state);
        overlay.SetActive(state);

        if (state)
        {
            questionMark.color = Color.red;
            playTxt.color = Color.red;
        }

        else
        {
            questionMark.color = Color.white;
            playTxt.color = Color.black;
        }
    }

    private void OnMouseDown()
    {
        clickSFX.Play();
        screenChange.PlayGame();
    }
}
