using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 转换条件
/// </summary>
public enum Transition//条件只是一个代称，在每一个具体的状态类中Reason方法中确定不同的条件含义，再通过FSMSystem统一切换状态
{
    NullTransition=0,//空的转换条件
    BelowClick,
    AboveClick,
    FindTarget,
    TargetInAttackRange,
    NoHealth,
    NoTarget
}

/// <summary>
/// 当前状态
/// </summary>
public enum StateID
{
    NullState,//空的状态
    Idle,
    BeDragged,
    SeekTarget,
    MoveToTarget,
    Attack,
    Dead
}

public abstract class FSMState
{
    protected StateID stateID;
    public StateID StateID { get { return stateID; } }
    protected Dictionary<Transition, StateID> transitionStateDic = 
        new Dictionary<Transition, StateID>();//每一个子类状态中都有一个“条件-状态”字典以供切换
    public FSMSystem fSMSystem;
    // protected AIController aIController;
    // protected CharacterStatus characterStatus;

    public FSMState(FSMSystem fSMSystem)
    {
        this.fSMSystem = fSMSystem;
        // aIController = fSMSystem.npc.GetComponent<AIController>();
        // characterStatus = fSMSystem.npc.GetComponent<CharacterStatus>();
    }

    /// <summary>
    /// 添加转换条件
    /// </summary>
    /// <param name="trans">转换条件</param>
    /// <param name="id">转换条件满足时执行的状态</param>
    public void AddTransition(Transition trans,StateID id)
    {
        if(trans==Transition.NullTransition)
        {
            Debug.LogError("不允许NullTransition");
            return;
        }
        if(id==StateID.NullState)
        {
            Debug.LogError("不允许NullStateID");
            return;
        }
        if(transitionStateDic.ContainsKey(trans))
        {
            Debug.LogError("添加转换条件的时候" + trans + "已经存在于transitionStateDic中");
            return;
        }
        transitionStateDic.Add(trans, id);
    }
    /// <summary>
    /// 删除转换条件
    /// </summary>
    public void DeleteTransition(Transition trans)
    {
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("不允许NullTransition");
            return;
        }
        if(!transitionStateDic.ContainsKey(trans))
        {
            Debug.LogError("删除转换条件的时候" + trans + "不存在于transitionStateDic中");
            return;
        }
        transitionStateDic.Remove(trans);
    }

    /// <summary>
    /// 获取当前转换条件下的状态
    /// </summary>
    public StateID GetOutputState(Transition trans)
    {
        if(transitionStateDic.ContainsKey(trans))
        {
            return transitionStateDic[trans];
        }
        return StateID.NullState;
    }
    /// <summary>
    /// 进入新状态之前做的事
    /// </summary>
    public virtual void DoBeforeEnter(GameObject npc) { }
    /// <summary>
    /// 离开当前状态时做的事
    /// </summary>
    public virtual void DoAfterLeave(GameObject npc) { }
    /// <summary>
    /// 当前状态所做的事
    /// </summary>
    public abstract void Act(GameObject npc);
    /// <summary>
    /// 在某一状态执行过程中，新的转换条件满足时做的事
    /// </summary>
    public abstract void Reason(GameObject npc);//判断转换条件
}
