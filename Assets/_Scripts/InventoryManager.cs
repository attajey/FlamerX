using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [Header("SFX")]
    [SerializeField] private AudioClip[] coinSFX;
    [SerializeField] private AudioClip fuelSFX;
    [SerializeField] private AudioClip potionSFX;
    [SerializeField] private AudioClip buffAbilitySFX;
    [SerializeField] private AudioClip healAbilitySFX;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Coin.OnCoinCollected += PlayRandomCoinSFX;
        Fuel.OnFuelCollected += PlayFuelSFX;
        Potion.OnPotionCollected += PlayPotionSFX;
        BuffAbility.OnBuffAbilityCollected += PlayBuffAbilitySFX;
        HealAbility.OnHealAbilityCollected += PlayHealAbilitySFX;


    }

    private void PlayBuffAbilitySFX()
    {
        audioSource.clip = buffAbilitySFX;
        audioSource.Play();
    }

    private void PlayHealAbilitySFX()
    {
        audioSource.clip = healAbilitySFX;
        audioSource.Play();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Coin"))
    //    {
    //    }
    //    if (collision.CompareTag("Fuel"))
    //    {
    //    }
    //}

    public void PlayRandomCoinSFX()
    {
        audioSource.clip = coinSFX[UnityEngine.Random.Range(0, coinSFX.Length)];
        audioSource.Play();
        //    CallAudio();
    }

    public void PlayFuelSFX()
    {
        audioSource.clip = fuelSFX;
        audioSource.Play();
    }

    public void PlayPotionSFX()
    {
        audioSource.clip = potionSFX;
        audioSource.Play();
    }
}
