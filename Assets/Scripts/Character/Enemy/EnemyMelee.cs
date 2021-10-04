using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : AIController
{
    protected override void OnEnable()
    {
        aiConfigFile = "EnemyMeleeAI.txt";
        base.OnEnable();
    }
}
