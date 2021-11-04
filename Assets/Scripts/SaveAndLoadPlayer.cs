using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SaveAndLoadPlayer : MonoBehaviour
{
    public class SaveFile
    {
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
        LoadCurrentPosition();
    }

    // Update is called once per frame
    public void SaveCurrentPosition()
    {
        SaveFile newSaveFile = new SaveFile();

        //get current position of the player
        newSaveFile.PlayerPosition = player.transform.position;
        newSaveFile.CameraPosition = cam.transform.position;

        //create our file
        StreamWriter sw = new StreamWriter(path);

        string stringSaveFile = JsonUtility.ToJson(newSaveFile);
        sw.Write(stringSaveFile);
        Debug.Log("Save: " + stringSaveFile);
        sw.Close();

    }
    public void LoadCurrentPosition()
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
            if(loadedFile != null)
            {
                player.transform.position = loadedFile.PlayerPosition;
                cam.transform.position = loadedFile.CameraPosition;
            }
        }
    }
}
