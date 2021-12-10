using UnityEngine;

public class InsultState : IStateBase
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
                _battleManager.GetBattleChatBox().WriteMessage(character1.CharacterName + ": You are so damn weak!");
                _sequenceNumber++;
                break;
            case 1:
                if (!_battleManager.GetBattleChatBox().IsTyping())
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _battleManager.GetBattleChatBox().WriteMessage(character2.CharacterName+ ": Stop messing around and fight me!");
                        _sequenceNumber++;
                    }
                }

                break;
            case 2:
                if (!_battleManager.GetBattleChatBox().IsTyping())
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _battleManager.GetBattleStateMachine().ChangeStateByKey("MenuState");
                    }
                }

                break;
            default:
                break;
            
        }
    }
}
