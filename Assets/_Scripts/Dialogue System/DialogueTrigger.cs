using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] sfx;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

    public void EndDialogue()
    {

		FindObjectOfType<DialogueManager>().EndDialogue();
    }

    void PlayRandomSFX()
    {
        audioSource.clip = sfx[UnityEngine.Random.Range(0, sfx.Length)];
        audioSource.Play();
        //    CallAudio();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
		{
			TriggerDialogue();
            PlayRandomSFX();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EndDialogue();
        }
    }

}
