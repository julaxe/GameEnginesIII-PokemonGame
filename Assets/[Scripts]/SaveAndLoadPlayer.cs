using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SaveAndLoadPlayer : MonoBehaviour
{
    public class SaveFile
    {
        public Pokemon[] pokemons = new Pokemon[6];
        public Pokemon ActivePokemon;
        public string CharacterName;
        public bool NPC;
        public Sprite sprite;

        public Vector3 PlayerPosition;
        public Vector3 CameraPosition;
    }


    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject cam;

    private string path;

    void Start()
    {
        path = Application.dataPath + Path.DirectorySeparatorChar + "PlayerPosition.txt";
    }

    // Update is called once per frame
    public void SaveCurrentPosition()
    {
        SaveFile newSaveFile = new SaveFile();

        //get current position of the player
        newSaveFile.PlayerPosition = player.transform.position;
        newSaveFile.CameraPosition = cam.transform.position;
        Character temp = player.GetComponent<Character>();

        for(int i = 0; i < temp.pokemons.Length; i++)
        {
            newSaveFile.pokemons[i] = temp.pokemons[i];
        }

        newSaveFile.ActivePokemon = temp.ActivePokemon;
        newSaveFile.CharacterName = temp.CharacterName;
        newSaveFile.NPC = temp.NPC;
        newSaveFile.sprite = temp.sprite;

    //create our file
        StreamWriter sw = new StreamWriter(path);

        string stringSaveFile = JsonUtility.ToJson(newSaveFile);
        sw.Write(stringSaveFile);
        Debug.Log("Save: " + stringSaveFile);
        sw.Close();

    }
    public void LoadCurrentPosition()
    {

        if (File.Exists(path))
        {
            //get the file
            StreamReader sr = new StreamReader(path);
            //get all the object
            string stringLoadedFile = sr.ReadToEnd();
            if(stringLoadedFile != null)
            {
                Debug.Log("Loaded: " + stringLoadedFile);

                //convert it to the object that we want
                SaveFile loadedFile = JsonUtility.FromJson<SaveFile>(stringLoadedFile);


          
                //change the position of our player to the one loaded.
                if (loadedFile != null)
                {
                    player.GetComponent<Character>().sprite = loadedFile.sprite;
                    player.GetComponent<Character>().NPC = loadedFile.NPC;
                    player.GetComponent<Character>().CharacterName = loadedFile.CharacterName;
                    player.GetComponent<Character>().ActivePokemon = loadedFile.ActivePokemon;

                    for (int i = 0; i < player.GetComponent<Character>().pokemons.Length; i++)
                    {
                        player.GetComponent<Character>().pokemons[i] = loadedFile.pokemons[i];
                    }

                    player.transform.position = loadedFile.PlayerPosition;
                    cam.transform.position = loadedFile.CameraPosition;
                }
            }
        }
    }
}
