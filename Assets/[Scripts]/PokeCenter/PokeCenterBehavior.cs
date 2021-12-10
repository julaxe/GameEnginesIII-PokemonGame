using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum pokecenterState
{
    CHOOSE_POKEMON,
    CHOSE_CURRENT_ABILITY_TO_CHANGE,
    CHOOSE_NEW_ABILITIE_TO_SWAP
}
public class PokeCenterBehavior : MonoBehaviour
{
    private Character _player;

    public Canvas pokeCenterCanvas;

    public TextMeshProUGUI instructionText;

    private pokecenterState _pokeCenterCurrentState;

    private GameObject _pokemonSlot1;
    private GameObject _pokemonSlot2;
    private GameObject _pokemonSlot3;
    private GameObject _pokemonSlot4;
    private GameObject _pokemonSlot5;
    private GameObject _pokemonSlot6;

    private GameObject _CurrentPokemon;
    private GameObject _CurrentAbilitie1;
    private GameObject _CurrentAbilitie2;
    private GameObject _CurrentAbilitie3;
    private GameObject _CurrentAbilitie4;

    public ScriptableObject[] abilities;

    private GameObject _NewAbilitie1;
    private GameObject _NewAbilitie2;
    private GameObject _NewAbilitie3;
    private GameObject _NewAbilitie4;

    // Start is called before the first frame update
    void Start()
    {
        pokeCenterCanvas = transform.Find("Canvas").GetComponent<Canvas>();
        instructionText = transform.Find("Canvas/InstructionsText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    private void showPokemons()
    {
        instructionText.text = "Choose the pokemon you want to change abilities";

       
        _pokemonSlot1 = GameObject.Find("Canvas/PokemonGrid/PokemonSlot1");
        _pokemonSlot2 = GameObject.Find("Canvas/PokemonGrid/PokemonSlot2");
        _pokemonSlot3 = GameObject.Find("Canvas/PokemonGrid/PokemonSlot3");
        _pokemonSlot4 = GameObject.Find("Canvas/PokemonGrid/PokemonSlot4");
        _pokemonSlot5 = GameObject.Find("Canvas/PokemonGrid/PokemonSlot5");
        _pokemonSlot6 = GameObject.Find("Canvas/PokemonGrid/PokemonSlot6");

        _CurrentPokemon = GameObject.Find("Canvas/CurrentAbilities/Pokemon");

        _CurrentAbilitie1 = GameObject.Find("Canvas/CurrentAbilities/CurrentAbilitie1");
        _CurrentAbilitie2 = GameObject.Find("Canvas/CurrentAbilities/CurrentAbilitie2"); 
        _CurrentAbilitie3 = GameObject.Find("Canvas/CurrentAbilities/CurrentAbilitie3"); 
        _CurrentAbilitie4 = GameObject.Find("Canvas/CurrentAbilities/CurrentAbilitie4");


        _NewAbilitie1 = GameObject.Find("Canvas/NewAbilities/NewAbility1");
        _NewAbilitie2 = GameObject.Find("Canvas/NewAbilities/NewAbility2");
        _NewAbilitie3 = GameObject.Find("Canvas/NewAbilities/NewAbility3");
        _NewAbilitie4 = GameObject.Find("Canvas/NewAbilities/NewAbility4");


        UpdateSlot(_pokemonSlot1, 0);
        UpdateSlot(_pokemonSlot2, 1);
        UpdateSlot(_pokemonSlot3, 2);
        UpdateSlot(_pokemonSlot4, 3);
        UpdateSlot(_pokemonSlot5, 4);
        UpdateSlot(_pokemonSlot6, 5);

        UpdateUI();
        //_changePokemonAbility = ScriptableObject.CreateInstance<Ability>();
        // _changePokemonAbility.abilityName = "ChangePokemon";

    }

    private void UpdateSlot(GameObject pokemonSlot, int pokemonNumber)
    {

        if (_player.pokemons[pokemonNumber] != null)
        {
            pokemonSlot.gameObject.SetActive(true);
            pokemonSlot.transform.Find("Image").GetComponent<Image>().sprite = _player.pokemons[pokemonNumber].image;
            pokemonSlot.transform.Find("RightSide/Name/Text").GetComponent<TextMeshProUGUI>().text = _player.pokemons[pokemonNumber].pokemonName;
            pokemonSlot.transform.Find("RightSide/Hp/Text").GetComponent<TextMeshProUGUI>().text = "HP: " + _player.pokemons[pokemonNumber].hp;
            pokemonSlot.transform.Find("RightSide/Level/Text").GetComponent<TextMeshProUGUI>().text = "Lv" + _player.pokemons[pokemonNumber].level;
            pokemonSlot.GetComponent<Button>().onClick.AddListener(delegate { ShowAbilities(pokemonNumber); });
        }
        else
        {
            pokemonSlot.gameObject.SetActive(false);
        }
    }


    private void ShowAbilities(int pokemonNumber)
    {
        instructionText.text = "Choose the Ability to swap";

        _pokeCenterCurrentState = pokecenterState.CHOSE_CURRENT_ABILITY_TO_CHANGE;
        UpdateSlot(_CurrentPokemon, pokemonNumber);

        UpdateCurrentAbilitie(_CurrentAbilitie1, 0, pokemonNumber);
        UpdateCurrentAbilitie(_CurrentAbilitie2, 1, pokemonNumber);
        UpdateCurrentAbilitie(_CurrentAbilitie3, 2, pokemonNumber);
        UpdateCurrentAbilitie(_CurrentAbilitie4, 3, pokemonNumber);

        UpdateUI();

        
    }

    private void UpdateCurrentAbilitie(GameObject AbilitieSlot, int abilitieNumber, int pokemonNumber)
    {
        if (_player.pokemons[pokemonNumber].abilities[abilitieNumber] != null)
        {
            AbilitieSlot.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = _player.pokemons[pokemonNumber].abilities[abilitieNumber].abilityName;
            AbilitieSlot.GetComponent<Button>().onClick.AddListener(delegate { ShowNewAbilities(pokemonNumber, abilitieNumber); });
        }
        else
        {
            AbilitieSlot.gameObject.SetActive(false);
        }
    
    }

    private void ShowNewAbilities(int pokemonNumber, int currentAbilitieNumber)
    {

        instructionText.text = "Choose the new Ability";

        _pokeCenterCurrentState = pokecenterState.CHOOSE_NEW_ABILITIE_TO_SWAP;

        UpdateUI();

        UpdateNewAbilitie(_NewAbilitie1, 0, currentAbilitieNumber, pokemonNumber);
        UpdateNewAbilitie(_NewAbilitie2, 1, currentAbilitieNumber, pokemonNumber);
        UpdateNewAbilitie(_NewAbilitie3, 2, currentAbilitieNumber, pokemonNumber);
        UpdateNewAbilitie(_NewAbilitie4, 3, currentAbilitieNumber, pokemonNumber);

    }

    private void UpdateNewAbilitie(GameObject AbilitieSlot, int newAbiliteNumber, int currentAbilitieNumber, int pokemonNumber)
    {
        Ability temp = (Ability)Instantiate(abilities[newAbiliteNumber]);

        if (abilities[newAbiliteNumber] != null)
        {
            AbilitieSlot.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = temp.abilityName;
            AbilitieSlot.GetComponent<Button>().onClick.AddListener(delegate { SwapAbillite(pokemonNumber, newAbiliteNumber, currentAbilitieNumber); });
        }
        else
        {
            _NewAbilitie1.SetActive(false);
        }
    }

    private void SwapAbillite(int pokemonNumber, int newAbilitieNumber, int currentAbilitieNumber)
    {
        instructionText.text = "SUCCES";
        _player.pokemons[pokemonNumber].abilities[currentAbilitieNumber] = (Ability)Instantiate(abilities[newAbilitieNumber]);
    }
    private void UpdateUI()
    {
        switch (_pokeCenterCurrentState)
        {
            case pokecenterState.CHOOSE_POKEMON:

                _CurrentPokemon.SetActive(false);

                _CurrentAbilitie1.SetActive(false);
                _CurrentAbilitie2.SetActive(false);
                _CurrentAbilitie3.SetActive(false);
                _CurrentAbilitie4.SetActive(false);


                _NewAbilitie1.SetActive(false);
                _NewAbilitie2.SetActive(false);
                _NewAbilitie3.SetActive(false);
                _NewAbilitie4.SetActive(false);

                break;

            case pokecenterState.CHOSE_CURRENT_ABILITY_TO_CHANGE:

                _pokemonSlot1.SetActive(false);
                _pokemonSlot2.SetActive(false);
                _pokemonSlot3.SetActive(false);
                _pokemonSlot4.SetActive(false);
                _pokemonSlot5.SetActive(false);
                _pokemonSlot6.SetActive(false);

                _CurrentPokemon.SetActive(true);
                _CurrentAbilitie1.SetActive(true);
                _CurrentAbilitie2.SetActive(true);
                _CurrentAbilitie3.SetActive(true);
                _CurrentAbilitie4.SetActive(true);

                break;

            case pokecenterState.CHOOSE_NEW_ABILITIE_TO_SWAP:

                _CurrentPokemon.SetActive(false);
                _CurrentAbilitie1.SetActive(false);
                _CurrentAbilitie2.SetActive(false);
                _CurrentAbilitie3.SetActive(false);
                _CurrentAbilitie4.SetActive(false);

                _NewAbilitie1.SetActive(true);
                _NewAbilitie2.SetActive(true);
                _NewAbilitie3.SetActive(true);
                _NewAbilitie4.SetActive(true);

                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.gameObject.GetComponent<Character>();

        pokeCenterCanvas.gameObject.SetActive(true);

        if (_player)
        {
            foreach(var pk in _player.pokemons)
            {
                if (pk)
                {
                    pk.hp = pk.maxHp;
                    foreach(var ab in pk.abilities)
                    {
                        if (ab)
                        {
                            ab.pp = ab.maxPP;
                        }
                    }
                }
            }
        }

        _pokeCenterCurrentState = pokecenterState.CHOOSE_POKEMON;
        showPokemons();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pokeCenterCanvas.gameObject.SetActive(false);
    }

}
