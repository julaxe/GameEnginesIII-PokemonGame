using UnityEngine;
public class FinalState : IStateBase
{
    private int _sequenceNumber = 0;
    public override void OnEnterState(BattleManager battleManager)
    {
        base.OnEnterState(battleManager);
        _battleManager.ShowBattleMessage();
        _sequenceNumber = 0;
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        // show characters
        switch (_sequenceNumber)
        {
            case 0:
                _battleManager.GetBattleAnimator().StartQueueAnimation(new string[]{
                    "SlideInEnemyFinal"
                });
                _sequenceNumber++;
                break;
            case 1:
                if (!_battleManager.GetBattleAnimator().IsQueuePlaying())
                {
                    if (character2.ActivePokemon.hp > 0)
                    {
                        _battleManager.GetBattleChatBox().WriteMessage(character2.winPhrase);
                    }
                    else
                    {
                        _battleManager.GetBattleChatBox().WriteMessage(character2.losePhrase);
                    }

                    _sequenceNumber++;
                }

                break;
            case 2:
                if (!_battleManager.GetBattleChatBox().IsTyping())
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _battleManager.EndBattle();
                    }
                }

                break;
            default:
                break;
            
        }
        //slide in character 1 and 2.
        
        //show message saying defeat message from loser.
        //resume game.
    }
}

