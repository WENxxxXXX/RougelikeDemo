using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : AIController
{
    public override void Start()
    {
        aiConfigFile = "EnemyRangedAI.txt";
        base.Start();
    }
}
