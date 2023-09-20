using System.Collections;
using UnityEngine;

public class SyncBombActionBlock : MonoBehaviour, IActionBlock
{
    public float timeToWaitForExplosion = 0.2f;
    public Sprite activatedBombImg;
    public Sprite explosionImg;

    private TriggerMachine triggers;
    private SynchroCounter sCounter;
    private int bombId = -1;

    // Start is called before the first frame update
    void Start()
    {
        triggers = GetComponent<TriggerMachine>();
        sCounter = FindFirstObjectByType<SynchroCounter>();
        if (sCounter == null)
        {
            Debug.LogError("SyncBombs need a SynchroCounter to work!!!");
        }
        else
        {
            bombId = sCounter.RegisterSyncBomb(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerformAction(string sourceTag)
    {
        if (sourceTag == "Bomb")
        {
            sCounter.RegisterSignal(bombId);
        }
    }

    public void ActivateBomb()
    {
        TryToExplode();
    }

    private void TryToExplode()
    {
        triggers.ChangeBlockImage(activatedBombImg);
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(timeToWaitForExplosion);
        triggers.ChangeBlockImage(explosionImg);
        triggers.ActivateSenderTriggers();
        StartCoroutine(EndExplosion());
    }

    private IEnumerator EndExplosion()
    {
        yield return new WaitForSeconds(timeToWaitForExplosion);
        triggers.TurnOffBlockImage();
    }
}
