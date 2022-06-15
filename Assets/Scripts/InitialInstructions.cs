using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class InitialInstructions : MonoBehaviour
{
    private bool usedLeft = false, usedRight = false, usedUp = false, usedDown = false, isWaiting = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            usedUp = true;

        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            usedLeft = true;

        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            usedDown = true;

        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            usedRight = true;

        if (usedUp && usedLeft && usedDown && usedRight && !isWaiting)
        {
            isWaiting = true;
            StartCoroutine(FadeAway());
        }
    }

    private IEnumerator FadeAway()
    {
        this.GetComponent<TextMeshProUGUI>().DOFade(0, 2);
        yield return new WaitForSeconds(2);
        Destroy(transform.parent.gameObject);
    }
}
