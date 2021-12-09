
using UnityEngine;

public class IStateBase
{
    protected BattleManager _battleManager;
    protected Character character1;
    protected Character character2;
    public virtual void OnEnterState(BattleManager battleManager)
    {
        _battleManager = battleManager;
        character1 = _battleManager.GetCharacter1();
        character2 = _battleManager.GetCharacter2();
    }

    public virtual void OnUpdateState()
    {
        
    }

    public virtual void OnExitState()
    {
        
    }
    
}
