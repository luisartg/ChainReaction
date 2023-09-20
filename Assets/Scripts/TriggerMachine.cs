using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMachine : MonoBehaviour
{
    private SpriteRenderer blockImage;
    private GameObject triggerSender;
    private BoxCollider2D triggerReceiver;
    private IActionBlock actionBlock;
    private AudioSource soundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        blockImage = GetComponent<SpriteRenderer>();
        triggerReceiver = GetComponent<BoxCollider2D>();
        triggerSender = gameObject.transform.GetChild(0).gameObject;
        actionBlock = GetComponent<IActionBlock>();
        soundPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartChain(string collisionTag)
    {
        ExecuteActionBlock(collisionTag);
    }

    private void ExecuteActionBlock(string sourceTag)
    {
        // this executes whatever action the variant prefabs will have
        if (actionBlock != null)
        {
            actionBlock.PerformAction(sourceTag);
        }
        else
        {
            Debug.Log("WARNING: No action block was found");
            ActivateSenderTriggers();
        }
    }

    private void OnMouseUpAsButton()
    {
        StartChain("Click");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartChain(collision.tag);
    }


    public void ActivateSenderTriggers()
    {
        // This must be activated after all actions desired by action block have finished
        triggerReceiver.enabled = false;
        triggerSender.SetActive(true);
    }

    public void ChangeBlockImage(Sprite newImage)
    {
        blockImage.sprite = newImage;
    }
    public void TurnOffBlockImage()
    {
        blockImage.enabled = false;
    }

    public void PlaySoundFX(AudioClip audioClip, float volume)
    {
        soundPlayer.PlayOneShot(audioClip, volume);
    }
}
