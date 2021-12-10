
using UnityEngine;

public class StartState : IStateBase
{
    private int AnimationCounter = 0;
    public override void OnEnterState(BattleManager battleManager)
    {

        SoundManager.soundManagerInstace.StopSound("Steps");
        SoundManager.soundManagerInstace.StopSound("Background");
        SoundManager.soundManagerInstace.PlaySound("Battle");

        base.OnEnterState(battleManager);
        Debug.Log("Start State enter");
        AnimationCounter = 0;
        _battleManager.GetBattleChatBox().WriteMessage("");
        _battleManager.GetBattleAnimator().StartQueueAnimation(new string[]{
            "ShowBattle", 
            "SlideInPlayers"
        });
        AnimationCounter++;
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        switch (AnimationCounter)
        {
            case 1:
                if (!_battleManager.GetBattleAnimator().IsQueuePlaying())
                {
                    _battleManager.GetBattleChatBox().WriteMessage("now Lucas is gonna throw a garbage pokemon");
                    AnimationCounter++;
                }
                break;
            case 2:
                if (!_battleManager.GetBattleChatBox().IsTyping())
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _battleManager.GetBattleAnimator().StartQueueAnimation(new string[]
                        {
                            "SwapEnemyPokemon"
                        });
                        AnimationCounter++;
                    }
                }
                break;
            case 3:
                if (!_battleManager.GetBattleAnimator().IsQueuePlaying())
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _battleManager.GetBattleChatBox().WriteMessage("Julian is the best!");
                        AnimationCounter++;
                    }
                }
                break;
            case 4:
                if (!_battleManager.GetBattleChatBox().IsTyping())
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _battleManager.GetBattleAnimator().StartQueueAnimation(new string[]
                        {
                            "SwapPlayerPokemon"
                        });
                        AnimationCounter++;
                    }
                }
                break;
            case 5:
                if (!_battleManager.GetBattleAnimator().IsQueuePlaying())
                {
                    _battleManager.GetBattleStateMachine().ChangeStateByKey("MenuState");
                    AnimationCounter++;
                }
                break;
            default:
                break;
        }
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }

}
