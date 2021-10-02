using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekTargetState : FSMState
{
    public SeekTargetState(FSMSystem fSMSystem) : base(fSMSystem)
    {
        stateID = StateID.SeekTarget;
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

        var colliders = Physics2D.OverlapCircleAll(npc.transform.position, npc.GetComponent<CharacterStatus>().spottingDistance);
        if (colliders.Length != 0)
        {
            foreach (var collider in colliders)
            {
                if (collider.CompareTag(npc.GetComponent<CharacterStatus>().attackTargetTag) && collider.gameObject.activeSelf)
                    {
                        npc.GetComponent<AIController>().target = collider.gameObject;
                        fSMSystem.PerformTransition(Transition.FindTarget);
                        break;
                    }
            }
        }
    }
}
