using UnityEngine;

public class BattleState : IStateBase
{
    private bool _character1GoesFirst = false;
    private int _sequenceNumber = 0;
    private Character _characterAttacking;
    private Character _characterGettingHit;
    private int damageCalculation;
    private bool _changingPokemon = false;

    public override void OnEnterState(BattleManager battleManager)
    {
        base.OnEnterState(battleManager);
        Debug.Log("Battle State entered");

        battleManager.ShowBattleMessage(); //it's always gonna be in this UI state.

        //AI here to pick an ability.
        if (character2 != null) //this means that we are against a character and not just a pokemon.
        {
            //for now we are just going to attack random.

            //set random number
            int maxRandom = 0;
            while (maxRandom < 4)
            {
                if (character2.ActivePokemon.abilities[maxRandom] == null) break;
                maxRandom++;
            }

            int randomNumber = Random.Range(0, maxRandom - 1);
            character2.ActivePokemon.currentAbility = character2.ActivePokemon.abilities[randomNumber];
        }

        if (character1.ActivePokemon.currentAbility.abilityName == "ChangePokemon")
        {
            SetCharacterAttacking(character1);
            _changingPokemon = true;
        }
        else
        {
            //check which pokemon goes first depending of speed.
            if (character1.ActivePokemon.speed >= character2.ActivePokemon.speed)
            {
                _character1GoesFirst = true;
                SetCharacterAttacking(character1);
            }
            else
            {
                _character1GoesFirst = false;
                SetCharacterAttacking(character2);
            }
        }
    }

    private void SetCharacterAttacking(Character attacker)
    {
        _sequenceNumber = 0;
        if (attacker == character1)
        {
            _characterAttacking = character1;
            _characterGettingHit = character2;
        }
        else
        {
            _characterAttacking = character2;
            _characterGettingHit = character1;
        }
    }

    private void BattleSequence()
    {
        if (_changingPokemon)
        {
            switch (_sequenceNumber)
            {
                case 0:
                    if (_characterAttacking.ActivePokemon.hp > 0)
                    {
                        _battleManager.GetBattleChatBox().WriteMessage(
                                                                       "Come back! " +
                                                                       _characterAttacking.ActivePokemon.pokemonName);
                    }
                    _sequenceNumber++;
                    break;
                case 1:
                    if (!_battleManager.GetBattleChatBox().IsTyping())
                    {
                        if (_characterAttacking.ActivePokemon.hp > 0)
                        {
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                _battleManager.GetBattleAnimator().StartQueueAnimation(new string[]
                                {
                                    "Character1Death"
                                });
                                _sequenceNumber++;
                            }
                        }
                        else
                        {
                            _sequenceNumber++;
                        }
                    }

                    break;
                case 2:
                    if (!_battleManager.GetBattleAnimator().IsQueuePlaying())
                    {
                        _characterAttacking.ActivePokemon =
                            _characterAttacking.pokemons[_characterAttacking.ActivePokemon.currentAbility.damage];
                        _battleManager.GetBattleChatBox().WriteMessage(
                            "Go! " +
                            _characterAttacking.ActivePokemon.pokemonName);
                        _sequenceNumber++;
                    }
                    
                    break;
                case 3:
                    if (!_battleManager.GetBattleChatBox().IsTyping())
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            _battleManager.GetBattleAnimator().StartQueueAnimation(new string[]
                            {
                                "SwapPlayerPokemon"
                            });
                            _sequenceNumber++;
                        }
                    }
                    break;
                case 4:
                    if (!_battleManager.GetBattleAnimator().IsQueuePlaying())
                    {
                        _changingPokemon = false;
                        SetCharacterAttacking(character2);
                    }
                    break;
                default:
                    break;
            }

            
            return;
        }
        
        switch (_sequenceNumber)
        {
            //message saying the pokemon name and the ability that he's using
            case 0:
                if (_characterAttacking.ActivePokemon.currentBeingAffectedBy != null &&_characterAttacking.ActivePokemon.currentBeingAffectedBy.effectType == EffectType.PARALIZE)
                {
                    if (Random.Range(0, 100) <
                        _characterAttacking.ActivePokemon.currentBeingAffectedBy.chanceOfEffectApplied)
                    {
                        _battleManager.GetBattleChatBox().WriteMessage(
                            _characterAttacking.ActivePokemon.pokemonName +
                            " can't attack because is " +
                            _characterAttacking.ActivePokemon.currentBeingAffectedBy.name);
                        _sequenceNumber = 7;
                        break;
                    }
                }
                _battleManager.GetBattleChatBox().WriteMessage(_characterAttacking.ActivePokemon.pokemonName +
                                                               " used " +
                                                               _characterAttacking.ActivePokemon.currentAbility.abilityName);
                _sequenceNumber++;
                
                break;
            //battle animation.
            case 1:
                if (!_battleManager.GetBattleChatBox().IsTyping())
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        if (_characterAttacking == character1)
                        {
                            _battleManager.GetBattleAnimator().StartQueueAnimation(new string[]
                            {
                                "Character1Attacks"
                            });
                        }
                        else
                        {
                            _battleManager.GetBattleAnimator().StartQueueAnimation(new string[]
                            {
                                "Character2Attacks"
                            });
                        }

                        _sequenceNumber++;
                    }
                }

                break;
            //character 2 receive damage
            case 2:
                //message saying if the abilty was effective or not.
                if (!_battleManager.GetBattleAnimator().IsQueuePlaying())
                {
                    damageCalculation = _characterAttacking.ActivePokemon.attack +
                                        _characterAttacking.ActivePokemon.currentAbility.damage - _characterGettingHit.ActivePokemon.defense;
                    string abilityEffectiveness =
                        _battleManager.GetTypeEffectiveness(_characterAttacking.ActivePokemon.currentAbility.type,
                            _characterGettingHit.ActivePokemon.type);
                    if (abilityEffectiveness == "Super Effective")
                    {
                        damageCalculation *= 2;
                    }
                    else if (abilityEffectiveness == "Not very effective")
                    {
                        damageCalculation /= 2;
                    }

                    _characterGettingHit.ActivePokemon.hp -= damageCalculation;

                    if (abilityEffectiveness != "")
                    {
                        _battleManager.GetBattleChatBox().WriteMessage("It's " + abilityEffectiveness);
                    }

                    _sequenceNumber++;
                }

                break;
            //character 2 update's hp.
            case 3:
                if (!_battleManager.GetBattleChatBox().IsTyping())
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        float percentile =
                            ((float) damageCalculation / (float) _characterGettingHit.ActivePokemon.maxHp) *
                            100.0f;
                        if (_characterGettingHit == character1)
                        {
                            _battleManager.GetBattleAnimator().UpdateCharacter1HP((int) percentile);
                        }
                        else
                        {
                            _battleManager.GetBattleAnimator().UpdateCharacter2HP((int) percentile);
                        }

                        _sequenceNumber++;
                    }
                }

                break;
            
            //check pokemon being hit status.
            case 4:
                if (!_battleManager.GetBattleAnimator().IsUpdatingHP())
                {
                    if (_characterGettingHit.ActivePokemon.hp > 0)
                    {
                        _sequenceNumber++;//go to check effects
                    }
                    else
                    {
                        if (_characterGettingHit == character1)
                        {
                            _battleManager.GetBattleAnimator().StartQueueAnimation(new string[]
                            {
                                "Character1Death"
                            });
                        }
                        else
                        {
                            _battleManager.GetBattleAnimator().StartQueueAnimation(new string[]
                            {
                                "Character2Death"
                            });
                        }

                        _sequenceNumber = 8;//check pokemons available.
                    }
                }

                break;
            //Ability effects
            case 5:
                if (_characterAttacking.ActivePokemon.currentAbility.Effect != null)
                {
                    if (_characterAttacking.ActivePokemon.currentAbility.Effect.affectsTheEnemy)
                    {
                        if (Random.Range(0, 100) < _characterAttacking.ActivePokemon.currentAbility.Effect
                            .chanceOfEffectApplied)
                        {
                            _characterGettingHit.ActivePokemon.ApplyEffect(_characterAttacking.ActivePokemon.currentAbility.Effect);
                            if (_characterAttacking == character1)
                            {
                                _battleManager.GetBattleAnimator().UpdateCharacter2Status();
                            }
                            else
                            {
                                _battleManager.GetBattleAnimator().UpdateCharacter1Status();
                            }
                            _battleManager.GetBattleChatBox().WriteMessage(_characterGettingHit.ActivePokemon.pokemonName + " is being affected by " + _characterGettingHit.ActivePokemon.currentBeingAffectedBy.name);
                        }
                    }
                    else //affects itself
                    {
                        if (_characterAttacking.ActivePokemon.currentAbility.Effect.effectType == EffectType.IMMUNITY)
                        {
                            if (_characterAttacking == character1)
                            {
                                _battleManager.GetBattleAnimator().UpdateCharacter1Status();
                            }
                            else
                            {
                                _battleManager.GetBattleAnimator().UpdateCharacter2Status();
                            }
                            _battleManager.GetBattleChatBox().WriteMessage(_characterAttacking.ActivePokemon.pokemonName + " is being affected by " + _characterGettingHit.ActivePokemon.currentAbility.Effect.name);
                        }
                    }
                }
                else
                {
                    if (_characterGettingHit.ActivePokemon.currentBeingAffectedBy != null)
                    {
                        _characterGettingHit.ActivePokemon.ApplyEffect(_characterGettingHit.ActivePokemon.currentBeingAffectedBy);
                        _battleManager.GetBattleChatBox().WriteMessage(_characterGettingHit.ActivePokemon.pokemonName + " is being affected by " + _characterGettingHit.ActivePokemon.currentBeingAffectedBy.name);
                    }
                   
                    if (_characterAttacking == character1)
                    {
                        _battleManager.GetBattleAnimator().UpdateCharacter1Status();
                    }
                    else
                    {
                        _battleManager.GetBattleAnimator().UpdateCharacter2Status();
                    }
                    
                    
                }
                _sequenceNumber++;

                break;
            //apply effect damage if there is
            case 6:
                if (!_battleManager.GetBattleChatBox().IsTyping())
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        if (_characterGettingHit.ActivePokemon.currentBeingAffectedBy != null &&
                            _characterGettingHit.ActivePokemon.currentBeingAffectedBy.damageOverRound > 0)
                        {
                            float percentile =
                                ((float) _characterGettingHit.ActivePokemon.currentBeingAffectedBy.damageOverRound / (float) _characterGettingHit.ActivePokemon.maxHp) *
                                100.0f;
                            if (_characterGettingHit == character1)
                            {
                                _battleManager.GetBattleAnimator().UpdateCharacter1HP((int) percentile);
                            }
                            else
                            {
                                _battleManager.GetBattleAnimator().UpdateCharacter2HP((int) percentile);
                            }

                        }
                        _sequenceNumber++;
                    }
                }

                break;
            //check if pokemons are alive.
            case 7:
                if (!_battleManager.GetBattleChatBox().IsTyping() && !_battleManager.GetBattleAnimator().IsUpdatingHP())
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        //if after effects it's still alive.
                        if (_characterGettingHit.ActivePokemon.hp > 0)
                        {
                            if (_character1GoesFirst && _characterAttacking == character1)
                            {
                                SetCharacterAttacking(character2);
                            }
                            else if (!_character1GoesFirst && _characterAttacking == character2)
                            {
                                SetCharacterAttacking(character1);
                            }
                            else
                            {
                                //Go to main menu
                                _battleManager.GetBattleStateMachine().ChangeStateByKey("MenuState");
                            }
                        }
                        else
                        {
                            if (_characterGettingHit == character1)
                            {
                                _battleManager.GetBattleAnimator().StartQueueAnimation(new string[]
                                {
                                    "Character1Death"
                                });
                            }
                            else
                            {
                                _battleManager.GetBattleAnimator().StartQueueAnimation(new string[]
                                {
                                    "Character2Death"
                                });
                            }

                            _sequenceNumber = 8; //check pokemons available.
                        }
                    }
                }

                break;
            //check if there are more pokemons available  (this is when pokemon died).
            case 8:
                if (!_battleManager.GetBattleAnimator().IsQueuePlaying())
                {
                    for(int i = 0; i < 6; i++)
                    {
                        if (_characterGettingHit.pokemons[i] == null) continue;
                        if (_characterGettingHit.pokemons[i].hp > 0)
                        {
                            if (_characterGettingHit == character1)
                            {
                                _battleManager.GetBattleStateMachine().ChangeStateByKey("ChangePokemonState");
                            }
                            else
                            {
                                _characterGettingHit.ActivePokemon = _characterGettingHit.pokemons[i];
                                _battleManager.GetBattleChatBox().WriteMessage(character2.CharacterName + " sent out " + _characterGettingHit.pokemons[i]);
                                _sequenceNumber++;
                            }

                            return;
                        }
                    }
                    _battleManager.GetBattleStateMachine().ChangeStateByKey("FinalState");
                }

                break;
            case 9:
                if (!_battleManager.GetBattleChatBox().IsTyping())
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _battleManager.GetBattleAnimator().StartQueueAnimation(new string[]
                        {
                            "SwapEnemyPokemon"
                        });
                        _sequenceNumber++;
                    }
                }

                break;
            case 10:
                if (!_battleManager.GetBattleAnimator().IsQueuePlaying())
                {
                    _battleManager.GetBattleStateMachine().ChangeStateByKey("MenuState");
                }

                break;
            //check character 2 status
            //check character 2 hp < 0
            //if okay
            //character 2 attack sequence.
            //not okay
            //death animation
            //check if character has more pokemons.
            //if yes
            //change active pokemon to the next one
            //Swap pokemon animation.
            //if not
            //go to final state.
            //check character 1 status
            //check character 2 hp < 0
            //if okay
            //go back to menu state
            //not okay
            //death animation
            //check if character has more pokemons.
            //if yes
            //go to change pokemon state
            //if not
            //go to final state.
        }

    }
    

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        BattleSequence();
    }
}
