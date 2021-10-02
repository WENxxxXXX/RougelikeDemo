using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : FSMState
{
    public AttackState(FSMSystem fSMSystem) : base(fSMSystem)
    {
        stateID = StateID.Attack;
    }

    public override void DoBeforeEnter(GameObject npc)
    {
        npc.GetComponent<AIController>().StartFireCoroutine();
    }

    public override void Act(GameObject npc)
    {
        
    }

    public override void Reason(GameObject npc)
    {
        if (npc.GetComponent<CharacterStatus>().currentHealth == 0)
        {
            fSMSystem.PerformTransition(Transition.NoHealth);
        }
        
        if (!npc.GetComponent<AIController>().target.activeSelf)
        {
            fSMSystem.PerformTransition(Transition.NoTarget);
        }
    }

    public override void DoAfterLeave(GameObject npc)
    {
        npc.GetComponent<AIController>().StopFireCoroutine();
    }
}
