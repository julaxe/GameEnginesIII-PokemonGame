using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangePokemonState : IStateBase
{
    private GameObject _pokemonSlot1;
    private GameObject _pokemonSlot2;
    private GameObject _pokemonSlot3;
    private GameObject _pokemonSlot4;
    private GameObject _pokemonSlot5;
    private GameObject _pokemonSlot6;
    private Ability _changePokemonAbility;
    public override void OnEnterState(BattleManager battleManager)
    {
        base.OnEnterState(battleManager);
        _battleManager.ShowChangePokemonUI(true);

        _pokemonSlot1 = GameObject.Find("UI/Canvas/ChangePokemon/PokemonGrid/PokemonSlot1");
        _pokemonSlot2 = GameObject.Find("UI/Canvas/ChangePokemon/PokemonGrid/PokemonSlot2");
        _pokemonSlot3 = GameObject.Find("UI/Canvas/ChangePokemon/PokemonGrid/PokemonSlot3");
        _pokemonSlot4 = GameObject.Find("UI/Canvas/ChangePokemon/PokemonGrid/PokemonSlot4");
        _pokemonSlot5 = GameObject.Find("UI/Canvas/ChangePokemon/PokemonGrid/PokemonSlot5");
        _pokemonSlot6 = GameObject.Find("UI/Canvas/ChangePokemon/PokemonGrid/PokemonSlot6");
        
        UpdateSlot(_pokemonSlot1, 0);
        UpdateSlot(_pokemonSlot2, 1);
        UpdateSlot(_pokemonSlot3, 2);
        UpdateSlot(_pokemonSlot4, 3);
        UpdateSlot(_pokemonSlot5, 4);
        UpdateSlot(_pokemonSlot6, 5);
        
        _changePokemonAbility = ScriptableObject.CreateInstance<Ability>();
        _changePokemonAbility.abilityName = "ChangePokemon";
        
    }

    private void UpdateSlot(GameObject pokemonSlot, int pokemonNumber)
    {
        if (character1.pokemons[pokemonNumber] != null)
        {
            pokemonSlot.gameObject.SetActive(true);
            pokemonSlot.transform.Find("Image").GetComponent<Image>().sprite = character1.pokemons[pokemonNumber].image;
            pokemonSlot.transform.Find("RightSide/Name/Text").GetComponent<TextMeshProUGUI>().text = character1.pokemons[pokemonNumber].pokemonName;
            pokemonSlot.transform.Find("RightSide/Hp/Text").GetComponent<TextMeshProUGUI>().text = "HP: " + character1.pokemons[pokemonNumber].hp;
            pokemonSlot.transform.Find("RightSide/Level/Text").GetComponent<TextMeshProUGUI>().text = "Lv" + character1.pokemons[pokemonNumber].level;
            pokemonSlot.GetComponent<Button>().onClick.AddListener(delegate { SetAbilityChangePokemon(pokemonNumber);});
        }
        else
        {
            pokemonSlot.gameObject.SetActive(false);
        }
    }

    public void SetAbilityChangePokemon(int newPokemonNumber)
    {
        _changePokemonAbility.damage = newPokemonNumber;
        character1.ActivePokemon.currentAbility = _changePokemonAbility;
        _battleManager.ShowChangePokemonUI(false);
        _battleManager.GetBattleStateMachine().ChangeStateByKey("BattleState");
    }
}
