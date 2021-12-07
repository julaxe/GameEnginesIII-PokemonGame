
using UnityEngine;

public class IStateBase
{
    protected BattleManager _battleManager;
    public virtual void OnEnterState(BattleManager battleManager)
    {
        _battleManager = battleManager;
    }

    public virtual void OnUpdateState()
    {
        
    }

    public virtual void OnExitState()
    {
        
    }
    
}
