using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    //save data
    public static string Name;
    public static string HighScoreName;
    public static int HighScore;
    
    private void Awake()
    {
        //establish singleton
       if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }
}
