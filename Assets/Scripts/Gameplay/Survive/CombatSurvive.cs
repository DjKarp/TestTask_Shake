using System;

public class CombatSurvive : Combat
{
    public event Action onDeath;

    protected override void Die(int _killerTeam)
    {
        onDeath?.Invoke();

        base.Die(_killerTeam);
    }

    public override void ResetCombat()
    {
        base.ResetCombat();
        dead = false;
    }
}
