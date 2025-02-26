using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Buff
{
    protected float duration;
    protected float startTime;
    protected float power;
    protected bool isBlank = false;
    readonly StatModType modType = StatModType.Flat;
    readonly StatType statType;
    protected Sprite buffIcon;
    protected StatModifier statModifier;
    protected Object source;
    protected List<StatModifier> modifiers = new List<StatModifier>();

    public float StartTime { get => startTime; set => startTime = value; }
    public float Duration { get => duration; set => duration = value; }
    public Object Source { get => source; set => source = value; }
    public Sprite BuffIcon { get => buffIcon; set => buffIcon = value; }
    public bool IsBlank { get => isBlank; set => isBlank = value; }
    #region Constructors
    public Buff(float duration, StatModifier statModifier, Object source, float startTime, Sprite buffIcon)
    {
        this.duration = duration;
        this.source = source;
        this.modifiers.Add(statModifier);
        this.startTime = startTime;
        this.buffIcon = buffIcon;
    }

    public Buff(float duration, List<StatModifier> modifiers, Object source, float startTime, Sprite buffIcon)
    {
        this.duration = duration;
        this.source = source;
        this.modifiers = modifiers;
        this.startTime = startTime;
        this.buffIcon = buffIcon;
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
    #endregion
    public virtual bool AreConditionsMet()
    {
        if (startTime + duration < Time.time)
        {
            return false;
        }
        return true;
    }
    public void ApplyEffects()
    {
        if (isBlank)
        {
            return;
        }

        foreach (StatModifier modifier in modifiers)
        {
            PlayerStatus.Instance.stats[(int)modifier.statType].AddModifier(modifier);
        }
    }

    public void RemoveEffects()
    {
        if (isBlank)
        {
            return;
        }

        foreach (StatModifier modifier in modifiers)
        {
            PlayerStatus.Instance.stats[(int)modifier.statType].RemoveModifier(modifier);
        }
        //PlayerStatus.Instance.stats[(int)statType].RemoveModifier(statModifier);
    }

}
