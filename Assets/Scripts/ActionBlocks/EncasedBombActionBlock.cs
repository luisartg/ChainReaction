using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncasedBombActionBlock : MonoBehaviour, IActionBlock
{
    public float timeToWaitForExplosion = 0.2f;
    public Sprite bombImg;
    public Sprite activatedBombImg;
    public Sprite explosionImg;

    public AudioClip explosionSfx;
    public AudioClip caseOpennedSfx;
    public AudioClip activateBombSfx;
    public float audioVolume = 0.5f;

    private TriggerMachine triggers;
    private bool blocked = true;
    private bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        triggers = GetComponent<TriggerMachine>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void TryToExplode()
    {
        if (!exploded)
        {
            triggers.PlaySoundFX(activateBombSfx, audioVolume);
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
        if (sourceTag == "Bomb" && !blocked)
        {
            if (!exploded)
            {
                TryToExplode();
            }
        }

        if (sourceTag == "Concrete" && blocked)
        {
            blocked = false;
            triggers.PlaySoundFX(caseOpennedSfx, audioVolume);
            triggers.ChangeBlockImage(bombImg);
        }

    }
}
