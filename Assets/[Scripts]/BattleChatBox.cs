using UnityEngine;

namespace _Scripts_
{
    public class BattleChatBox
    {
        private GameObject _OBattleChatBox;
        private Character _playerRef;
        
        private GameObject _OBattleMessage;
        private GameObject _OBattleOptions;
        private GameObject _OAbilitiesOptions;

        private TMPro.TextMeshProUGUI _currentText;
        
        public BattleChatBox(GameObject value, Character playerRef)
        {
            _OBattleChatBox = value;
            _playerRef = playerRef;

            _OBattleMessage = _OBattleChatBox.transform.Find("BattleMessage").gameObject;
            _OBattleOptions = _OBattleChatBox.transform.Find("BattleOptions").gameObject;
            _OAbilitiesOptions = _OBattleChatBox.transform.Find("AbilitiesOptions").gameObject;
        }

        public void StartBattleMessage()
        {
            SetChatBoxActive(_OBattleMessage);
            _currentText = _OBattleMessage.transform.Find("Text/TextMessage").GetComponent<TMPro.TextMeshProUGUI>();
        }

        public void StartBattleOptions()
        {
            SetChatBoxActive(_OBattleOptions);
            _currentText = _OBattleMessage.transform.Find("LeftSide/Text/TextMessage").GetComponent<TMPro.TextMeshProUGUI>();
        }

        public void StartAbilitiesOptions()
        {
            SetChatBoxActive(_OAbilitiesOptions);
        }

        public void ShowMessage(string msg)
        {
            //show message word by word.
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