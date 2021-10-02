using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : FSMState
{
    public DeadState(FSMSystem fSMSystem) : base(fSMSystem)
    {
        stateID = StateID.Dead;
    }

    public override void Act(GameObject npc)
    {
        
    }

    public override void Reason(GameObject npc)
    {
        
    }
}
