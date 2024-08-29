using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    List<Buff> buffs= new List<Buff>();
    List<Buff> buffsWithIcons = new List<Buff>();
    float lastCheckTime;
    [SerializeField] float timeBetweenChecks = 0.1f;

    public static BuffManager Instance { get; private set; }
    public List<Buff> BuffsWithIcons { get => buffsWithIcons; set => buffsWithIcons = value; }

    public delegate void BuffsUpdatedAction();
    public static event BuffsUpdatedAction OnBuffsUpdatedCallback;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one BuffManager instance found!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        lastCheckTime = Time.time;
    }


    public void Update()
    {
        if (lastCheckTime + timeBetweenChecks <= Time.time)
        {
            lastCheckTime = Time.time;
            CheckActiveBuffs();
        }
    }

    public void AddBuff(Buff buff)
    {
        int i = DoesListContainBuffOfSource(buff.Source);
        if(i != -1)
        {
            buffs[i].RemoveEffects();
            buffs.RemoveAt(i);
            buffsWithIcons.RemoveAt(i);
        }

        buffs.Add(buff);

        if (buff.BuffIcon is not null)
        {
            buffsWithIcons.Add(buff);
        }

        buff.ApplyEffects();
    }

    public void RemoveBuff(Buff buff)
    {
        buffs.Remove(buff);
        if (buff.BuffIcon != null)
        {
            buffsWithIcons.Remove(buff);
        }
        buff.RemoveEffects();
    }



    public void CheckActiveBuffs()
    {
        for (int i = buffs.Count - 1; i >= 0; i--)
        {
            Buff buff = buffs[i];
            if (buff.StartTime + buff.Duration < Time.time)
            {
                RemoveBuff(buff);
            }
        }

        if (OnBuffsUpdatedCallback != null)
        {
            OnBuffsUpdatedCallback();
        }
    }

    public int DoesListContainBuffOfSource(Object source)
    {
        for (int i = buffs.Count - 1; i >= 0; i--)
        {
            Buff buff = buffs[i];
            if (buff.Source == source)
            {
                return i;
            }
        }
        return -1;
    }


    public List<Buff> GetBuffList()
    {
        return buffs;
    }
}
