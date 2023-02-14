using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Coin : MonoBehaviour, ICollectible
{
    [Header("SFX")]
    [SerializeField] private AudioClip[] coinSFX;
    private AudioSource audioSource;


    public static event Action OnCoinCollected;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    public void Collect()
    {
        OnCoinCollected += PlayRandomSFX;
        Debug.Log("Collected a coin!");
        Destroy(gameObject);
        OnCoinCollected?.Invoke();
    }

    void PlayRandomSFX()
    {
        audioSource.clip = coinSFX[UnityEngine.Random.Range(0, coinSFX.Length)];
        audioSource.Play();
        //    CallAudio();
    }


}
