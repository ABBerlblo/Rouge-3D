using UnityEngine;

public class AttackState : State
{
    public override State RunCurrentState()
    {
        Debug.Log("Attack");
        return this;
    }
}
