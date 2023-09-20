using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConcreteActionBlock : MonoBehaviour, IActionBlock
{
    public int hits = 1;
    public Sprite destroyedConcreteImg;
    public AudioClip concreteHitSfx;
    public float audioVolume = 0.5f;

    private bool destroyed = false;
    private TriggerMachine triggerMachine;
    private TextMeshPro hitNumberText;
    private PuzzleManager puzzleManager;

    // Start is called before the first frame update
    void Start()
    {
        puzzleManager = FindFirstObjectByType<PuzzleManager>();
        puzzleManager.RegisterConcreteBlock();
        triggerMachine = GetComponent<TriggerMachine>();
        hitNumberText = gameObject.GetComponentInChildren<TextMeshPro>();
        UpdateHitText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateHitText()
    {
        hitNumberText.text = hits.ToString();
        if (hits <= 0)
        {
            hitNumberText.enabled = false;
        }
    }

    public void PerformAction(string sourceTag)
    {
        if (sourceTag != "Bomb")
        {
            return; // do not advance if interaction is not from correct source
        }

        if (!destroyed)
        {
            hits--;
            triggerMachine.PlaySoundFX(concreteHitSfx, audioVolume);
            UpdateHitText();
            if (hits <= 0)
            {
                destroyed = true;
                triggerMachine.ChangeBlockImage(destroyedConcreteImg);
                triggerMachine.ActivateSenderTriggers();
                puzzleManager.RegisterConcreteBlockDestroyed();
            }
        }
    }
}
