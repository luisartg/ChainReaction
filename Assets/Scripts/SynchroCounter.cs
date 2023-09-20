using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchroCounter : MonoBehaviour
{
    private List<SyncBombActionBlock> syncBombs;
    private List<bool> signals;

    [SerializeField]
    private float signalFallOffTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeList()
    {
        if (syncBombs == null)
        {
            syncBombs = new List<SyncBombActionBlock>();
            signals = new List<bool>();
        }
    }

    private IEnumerator WaitForSignalFallOff()
    {
        yield return new WaitForSeconds(signalFallOffTime);
        ClearSignals();
    }

    private void ClearSignals()
    {
        for (int i = 0; i < signals.Count; i++)
        {
            signals[i] = false;
        }
    }

    private void SynchronizedExplosion()
    {
        foreach (var sbomb in syncBombs)
        {
            sbomb.ActivateBomb();
        }
    }

    public int RegisterSyncBomb(SyncBombActionBlock syncBombRef)
    {
        InitializeList();
        syncBombs.Add(syncBombRef);
        signals.Add(false);
        return syncBombs.Count - 1;
    }

    public void RegisterSignal(int bombId)
    {
        StopCoroutine(WaitForSignalFallOff());
        signals[bombId] = true;
        if (IsCompleteSignal())
        {
            SynchronizedExplosion();
        }
        else
        {
            StartCoroutine(WaitForSignalFallOff());
        }
    }

    private bool IsCompleteSignal()
    {
        foreach (var signal in signals)
        {
            if (!signal)
            {
                return false;
            }
        }
        return true;
    }
}
