using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : AIController
{
    protected override void OnEnable()
    {
        aiConfigFile = "EnemyRangedAI.txt";
        base.OnEnable();
    }
}
