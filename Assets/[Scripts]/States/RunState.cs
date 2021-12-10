using UnityEngine;

public class RunState : IStateBase
{
    private int _sequenceNumber;
    public override void OnEnterState(BattleManager battleManager)
    {
        base.OnEnterState(battleManager);
        _battleManager.ShowBattleMessage();
        _sequenceNumber = 0;
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        switch (_sequenceNumber)
        {
            case 0:
                _battleManager.GetBattleChatBox().WriteMessage("Run away safely... like a pussy");
                _sequenceNumber++;
                break;
            case 1:
                if (!_battleManager.GetBattleChatBox().IsTyping())
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _battleManager.EndBattle();
                        _sequenceNumber++;
                    }
                }
                break;
            default:
                break;
            
        }
    }
}
