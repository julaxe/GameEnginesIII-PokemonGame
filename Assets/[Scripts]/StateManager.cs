using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class StateManager : MonoBehaviour
{
   private Dictionary<string, IStateBase> availableStates;
   public IStateBase _currentState = null;

   private BattleManager _battleManager;
   
   private void Start()
   {
      _battleManager = GameObject.FindObjectOfType<BattleManager>();
      availableStates = new Dictionary<string, IStateBase>();
      InitialiazeStateMachine();
   }

   public void StartStateMachine(string firstState)
   {
      ChangeStateByKey(firstState);
   }
   private void Update()
   {
      if(_currentState != null)
         _currentState.OnUpdateState();
   }

   public void ChangeStateByKey(string key)
   {
      if (_currentState != null)
      {
         _currentState.OnExitState();
      }
      _currentState = availableStates[key];
      _currentState.OnEnterState(_battleManager);
   }

   private void InitialiazeStateMachine()
   {
      availableStates.Add("StartState",new StartState());
      availableStates.Add("MenuState",new MenuState());
   }
}
