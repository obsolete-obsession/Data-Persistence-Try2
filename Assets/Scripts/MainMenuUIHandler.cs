using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIHandler : MonoBehaviour
{

    [SerializeField] private Button startButton;

    public InputField nameInputField;
    public string name;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log(DataManager.Name);
    }

    public void SetName()
    {
        name = nameInputField.text;
        DataManager.Name = name;
        Debug.Log(name);
    }
}
