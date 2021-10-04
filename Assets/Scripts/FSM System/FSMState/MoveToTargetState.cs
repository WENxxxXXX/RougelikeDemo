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
        Vector3 moveDirection = npc.GetComponent<AIController>().target.transform.position - npc.transform.position;
        moveDirection.Normalize();
        moveDirection.z = 0;
        npc.GetComponent<AIController>().moveDirectionList.Clear();
        npc.GetComponent<AIController>().moveDirectionList.Add(moveDirection);
        npc.GetComponent<EnemyStatus>().moveSpeed = npc.GetComponent<EnemyStatus>().maxMoveSpeed;
    }

    public override void Reason(GameObject npc)
    {
        if (npc.GetComponent<CharacterStatus>().currentHealth == 0)
        {
            fSMSystem.PerformTransition(Transition.NoHealth);
        }

        if (!npc.GetComponent<AIController>().target.activeSelf || Vector3.Distance(npc.GetComponent<AIController>().
            target.transform.position, npc.transform.position) >= npc.GetComponent<EnemyStatus>().spottingDistance)
        {
            fSMSystem.PerformTransition(Transition.NoTarget);
        }
    }
}
