public class IdleState : State
{
    public ChaseState chaseState;
    public bool canSeePlayer;
    public override State RunCurrentState()
    {
        if (canSeePlayer)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }
}
