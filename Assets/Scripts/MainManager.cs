using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text DataText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private void Awake()
    {
        LoadHighScoreData();
        DataText.text = "High Score: " + DataManager.HighScore + " Name: " + DataManager.HighScoreName;
    }

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        if(m_Points >= DataManager.HighScore)
        {
            DataManager.HighScore = m_Points;
            DataManager.HighScoreName = DataManager.Name;
            SaveHighScoreData();
        }
        GameOverText.SetActive(true);
    }

    [System.Serializable]
    public class SaveData
    {
        public string HighScoreName;
        public int HighScore;


    }

    public void SaveHighScoreData()
    {
        SaveData saveData = new SaveData();

        saveData.HighScoreName = DataManager.HighScoreName;
        saveData.HighScore = DataManager.HighScore;

        string jsonData = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", jsonData);
        
    }

    public void LoadHighScoreData()
    {
        if(File.Exists(Application.persistentDataPath + "/savefile.json"))
        {
            
            string jsonData = File.ReadAllText(Application.persistentDataPath + "/savefile.json");

            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData);

            DataManager.HighScoreName = saveData.HighScoreName;
            DataManager.HighScore = saveData.HighScore;
        }
    }
}
