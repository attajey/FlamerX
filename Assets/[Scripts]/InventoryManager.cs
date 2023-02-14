using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [Header("SFX")]
    [SerializeField] private AudioClip[] coinSFX;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Coin.OnCoinCollected += PlayRandomCoinSFX;
        }
    }

    public void PlayRandomCoinSFX()
    {
        audioSource.clip = coinSFX[UnityEngine.Random.Range(0, coinSFX.Length)];
        audioSource.Play();
        //    CallAudio();
    }
}
