using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSurvive : Combat
{
    public event Action onDeath;

    protected override void Die(int _killerTeam)
    {
        onDeath?.Invoke();

        base.Die(_killerTeam);
    }

    public void ResetCombat()
    {
        dead = false;
        health = maxHealth;
    }
}
