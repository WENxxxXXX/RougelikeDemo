using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetState : FSMState
{
    public MoveToTargetState(FSMSystem fSMSystem) : base(fSMSystem)
    {
        stateID = StateID.MoveToTarget;
    }

    public override void Act(GameObject npc)
    {
        if (npc.GetComponent<AIController>().target.activeSelf)
        {
            Vector3 moveDirection = npc.GetComponent<AIController>().target.transform.position - npc.transform.position;
            moveDirection.z = 0;
            npc.transform.Translate(moveDirection.normalized * npc.GetComponent<CharacterStatus>().moveSpeed * Time.deltaTime);
        }
    }

    public override void Reason(GameObject npc)
    {
        if (npc.GetComponent<CharacterStatus>().currentHealth == 0)
        {
            fSMSystem.PerformTransition(Transition.NoHealth);
        }

        if (!npc.GetComponent<AIController>().target.activeSelf || Vector3.Distance(npc.GetComponent<AIController>().
            target.transform.position, npc.transform.position) >= npc.GetComponent<CharacterStatus>().spottingDistance)
        {
            fSMSystem.PerformTransition(Transition.NoTarget);
        }

        if (Vector3.Distance(npc.GetComponent<AIController>().target.transform.position, npc.transform.position)
            <= npc.GetComponent<CharacterStatus>().attackRange)
        {
            fSMSystem.PerformTransition(Transition.TargetInAttackRange);
        }
    }
}
