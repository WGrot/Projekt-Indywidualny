using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : Explosive, Ihp
{
    [SerializeField] private float barrelHp;
    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void Heal(float healAmount)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
