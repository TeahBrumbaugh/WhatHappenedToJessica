using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class PlayerBehavior : MonoBehaviour
{
    private RevealAfterScenes transferScript;
    private CharacterController playerController;
    private int movementSpeed = 6;
    private bool canMove = true;

    [SerializeField] private GameObject evidenceList;
    private int collectedEvidence = 0;
    private bool isInterview = false;

    private Slider currentSlider;
    [SerializeField] private Light cameraFlash;
    [SerializeField] private AudioSource cameraSFX;
    [SerializeField] private AudioSource writingSFX;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
        transferScript = GameObject.Find("RevealAfterScenes").GetComponent<RevealAfterScenes>();
    }

    private void FixedUpdate()
    {
        if (isInterview)
            currentSlider.value += Time.deltaTime;

        if (canMove == false)
            return;

        float zMove = Input.GetAxis("Vertical");
        float xMove = Input.GetAxis("Horizontal");

        Vector3 playerMovement = transform.forward * zMove + transform.right * xMove;
        playerController.Move(playerMovement * Time.deltaTime * movementSpeed);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Evidence")
        {
            collision.gameObject.SetActive(false);
            collectedEvidence++;

            if (collectedEvidence == evidenceList.transform.childCount)
                transferScript.RecieveData(" ");
        }

        else if (collision.tag == "Photo")
            StartCoroutine(InterviewWait(collision, 1));

        else if (collision.tag == "Interview" && isInterview == false)
        {
            isInterview = true;
            StartCoroutine(InterviewWait(collision, 2));
        }
    }

    private IEnumerator InterviewWait(Collider collidedObj, float waitTime)
    {
        collectedEvidence++;
        canMove = false;

        if (isInterview)
        {
            writingSFX.Play();
            currentSlider = collidedObj.transform.GetChild(0).GetChild(0).GetComponentInChildren<Slider>();
            currentSlider.gameObject.SetActive(true);
            currentSlider.maxValue = waitTime;
            yield return new WaitForSeconds(waitTime);
            writingSFX.Stop();
            isInterview = false;
        }

        else
        {
            cameraSFX.Play();
            yield return new WaitForSeconds(waitTime / 2);
            cameraFlash.DOIntensity(2, waitTime / 2);
            yield return new WaitForSeconds(waitTime / 4);
            cameraFlash.DOIntensity(0, waitTime / 4);
            yield return new WaitForSeconds(waitTime / 4);
        }

        collidedObj.transform.GetChild(0).gameObject.SetActive(false);
        Destroy(collidedObj);

        canMove = true;

        if (collectedEvidence == evidenceList.transform.childCount)
            transferScript.RecieveData(" ");
    }
}
