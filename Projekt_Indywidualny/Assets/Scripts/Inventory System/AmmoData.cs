using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AmmoData
{
    public int ammoInClip { get; private set; }
    public int ammoLeft { get; private set; }
    public AmmoData(int ammoInClip, int ammoLeft)
    {
        this.ammoInClip = ammoInClip;
        this.ammoLeft = ammoLeft;
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
}
