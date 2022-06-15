using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BkgMusic : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    private static BkgMusic currentMusic;

    private void Awake()
    {
        if (currentMusic == null)
        {
            currentMusic = this;
            DontDestroyOnLoad(music);
        }
        else
            Destroy(music);
    }

}
