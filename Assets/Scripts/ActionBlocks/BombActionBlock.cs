using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombActionBlock : MonoBehaviour, IActionBlock
{
    public float timeToWaitForExplosion = 0.2f;
    public Sprite activatedBombImg;
    public Sprite explosionImg;
    public AudioClip activateSfx;
    public AudioClip explosionSfx;
    public float audioVolume = 0.5f;

    private TriggerMachine triggers;
    private PuzzleManager puzzleManager;
    private bool exploded = false;
    // Start is called before the first frame update
    void Start()
    {
        triggers = GetComponent<TriggerMachine>();
        puzzleManager = FindFirstObjectByType<PuzzleManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void TryToExplode(bool clickExplosion)
    {
        if (!exploded)
        {   if (clickExplosion)
            {
                triggers.PlaySoundFX(activateSfx, audioVolume);
            }
            triggers.ChangeBlockImage(activatedBombImg);
            exploded = true;
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode() 
    {
        yield return new WaitForSeconds(timeToWaitForExplosion);
        triggers.PlaySoundFX(explosionSfx, audioVolume);
        triggers.ChangeBlockImage(explosionImg);
        triggers.ActivateSenderTriggers();
        StartCoroutine(EndExplosion());
    }

    private IEnumerator EndExplosion()
    {
        yield return new WaitForSeconds(timeToWaitForExplosion);
        triggers.TurnOffBlockImage();
    }

    public void PerformAction(string sourceTag)
    {
        if (exploded) { return; }

        if (sourceTag == "Click")
        {
            //Only first click is allowed to start, the rest has to explode by reaction
            if (puzzleManager.AllowedToIgnite())
            {
                TryToExplode(true);
            }
            else
            {
                Debug.Log("Only one click allowed!");
            }
        }

        if (sourceTag == "Bomb")
        {
            TryToExplode(false);
        }

    }
}
