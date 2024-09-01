using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Buff
{
    float duration;
    float startTime;
    float power;
    private bool isBlank = false;
    readonly StatModType modType = StatModType.Flat;
    readonly StatType statType;
    Sprite buffIcon;
    StatModifier statModifier;
    Object source;

    public float StartTime { get => startTime; set => startTime = value; }
    public float Duration { get => duration; set => duration = value; }
    public Object Source { get => source; set => source = value; }
    public Sprite BuffIcon { get => buffIcon; set => buffIcon = value; }
    public bool IsBlank { get => isBlank; set => isBlank = value; }

    public Buff(float duration, float power, StatType statType, Object source)
    {
        this.duration = duration;
        this.power = power;
        this.statType = statType;
        this.source = source;
        startTime = Time.time;
    }

    public Buff(float duration, float power, StatType statType, Object source, StatModType modType)
    {
        this.duration = duration;
        this.power = power;
        this.statType = statType;
        this.modType = modType;
        this.source = source;
        startTime = Time.time;
    }

    public Buff(float duration, float power, StatType statType, Object source, StatModType modType, float startTime)
    {
        this.duration = duration;
        this.power = power;
        this.statType = statType;
        this.source = source;
        this.modType = modType;
        this.startTime = startTime;
    }

    public Buff(float duration, float power, StatType statType, Object source, StatModType modType, float startTime, Sprite buffIcon)
    {
        this.duration = duration;
        this.power = power;
        this.statType = statType;
        this.source = source;
        this.modType = modType;
        this.startTime = startTime;
        this.buffIcon= buffIcon;
    }

    // Konstruktor dla pustego buffa. Pusty buff ma tylko i wy³¹cznie pokazywaæ ikonê w Hud'zie, bez wp³ywania na statystyki
    public Buff(float duration, Object source, float startTime, Sprite buffIcon) 
    {
        this.duration = duration;
        this.source = source;
        this.startTime = startTime;
        this.buffIcon = buffIcon;
        isBlank= true;
    }

    public void ApplyEffects()
    {
        if (isBlank)
        {
            return;
        }

        statModifier = new StatModifier(power, modType, 100, this);
        PlayerStatus.Instance.stats[(int)statType].AddModifier(statModifier);
    }

    public void RemoveEffects()
    {
        if (isBlank)
        {
            return;
        }
        PlayerStatus.Instance.stats[(int)statType].RemoveModifier(statModifier);
    }

}
