using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Scripts_
{
    public class BattleChatBox : MonoBehaviour
    {
        private GameObject _OBattleMessage;
        private GameObject _OBattleOptions;
        private GameObject _OAbilitiesOptions;

        private TMPro.TextMeshProUGUI _currentText;
        private string _message;
        private int _currentIndexMessage;
        private float _textSpeed= 0.05f;
        private bool isTyping = false;

        public void StartBattleChatBox()
        {
            _OBattleMessage = transform.Find("BattleMessage").gameObject;
            _OBattleOptions = transform.Find("BattleOptions").gameObject;
            _OAbilitiesOptions = transform.Find("AbilitiesOptions").gameObject;
            _currentText = _OBattleMessage.transform.Find("Text/TextMessage").GetComponent<TMPro.TextMeshProUGUI>();
        }

        public GameObject GetBattleOptions()
        {
            return _OBattleOptions;
        }

        public GameObject GetAbiltiesOptions()
        {
            return _OAbilitiesOptions;
        }
        public void StartBattleMessage()
        {
            SetChatBoxActive(_OBattleMessage);
            _currentText = _OBattleMessage.transform.Find("Text/TextMessage").GetComponent<TMPro.TextMeshProUGUI>();
        }

        public void StartBattleOptions()
        {
            SetChatBoxActive(_OBattleOptions);
            _currentText = _OBattleOptions.transform.Find("LeftSide/Text/TextMessage").GetComponent<TMPro.TextMeshProUGUI>();
        }

        public void StartAbilitiesOptions()
        {
            SetChatBoxActive(_OAbilitiesOptions);
        }

        public bool IsTyping()
        {
            return isTyping;
            
        }
        public void WriteMessage(string message)
        {
            _message = message;
            _currentText.text = "";
            StartCoroutine(TypeLine());
        }
        IEnumerator TypeLine()
        {
            isTyping = true;
            foreach (char c in _message)
            {
                _currentText.text += c;
                yield return new WaitForSecondsRealtime(_textSpeed);
            }

            isTyping = false;
        }

        public void SetChatBoxActive(GameObject gameObject)
        {
            _OBattleOptions.SetActive(false);
            _OAbilitiesOptions.SetActive(false);
            _OBattleMessage.SetActive(false);
            if (_OBattleMessage == gameObject) _OBattleMessage.SetActive(true);
            else if (_OBattleOptions == gameObject) _OBattleOptions.SetActive(true);
            else _OAbilitiesOptions.SetActive(true);
        }

    }
}