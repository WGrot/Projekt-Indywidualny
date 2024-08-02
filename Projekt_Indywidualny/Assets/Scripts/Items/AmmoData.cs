using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AmmoData
{

    public int ammoInClip { get; private set; }
    public int ammoLeft { get; private set; }

    public int ammoMax { get; private set; }
    public AmmoData(int ammoInClip, int ammoLeft, int ammoMax)
    {
        this.ammoInClip = ammoInClip;
        this.ammoLeft = ammoLeft;
        this.ammoMax = ammoMax;
    }

    public void DecreaseAmmo(int amount)
    {
        ammoInClip-= amount;
        if (ammoInClip < 0)
        {
            ammoInClip=0;
        }
        ammoLeft-= amount;
        if (ammoLeft < 0)
        {
            ammoLeft=0;
        }
    }
    
    public void ReloadClip(int clipSize)
    {
        if (ammoLeft <= clipSize)
        {
            ammoInClip=ammoLeft;
        }else
        {
            ammoInClip = clipSize;
        }
    }

    public void RefillByPercent(int percent)
    {
        Debug.Log((int)(percent / 100f) * ammoMax);
        ammoLeft += (int)((percent / 100f) * ammoMax);
        if (ammoLeft > ammoMax)
        {
            ammoLeft = ammoMax;
        }
    }
}
