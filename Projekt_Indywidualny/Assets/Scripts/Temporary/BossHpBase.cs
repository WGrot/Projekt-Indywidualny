using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpBase : EnemyHpBase
{
    public delegate void BossfightStartedAction(Sprite bossIcon);
    public static event BossfightStartedAction OnBossfightStartedCallback;

    public delegate void BossfightEndedAction();
    public static event BossfightEndedAction OnBossfighEndedCallback;

    public delegate void BossHpChanged(float remainingHp, float maxHp);
    public static event BossHpChanged OnBossHpChangedCallback;

    public void CallOnBossFightStarted(Sprite bossIcon)
    {
        if (OnBossfightStartedCallback != null)
        {
            OnBossfightStartedCallback(bossIcon);
        }
    }

    public void CallOnBossFightEnded()
    {
        if (OnBossfighEndedCallback != null)
        {
            OnBossfighEndedCallback();
        }
    }

    public void CallOnBossHpChanged(float remainingHp, float maxHp)
    {
        if (OnBossHpChangedCallback != null)
        {
            OnBossHpChangedCallback(remainingHp, maxHp);
        }
    }
}
