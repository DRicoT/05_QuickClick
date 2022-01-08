using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //Si quieres trabajar con Text Mesh Pro
using UnityEngine.UI; //Si quieres trabajar con cosas del Interface como la clase Button
using UnityEngine.SceneManagement; //Si quieres trabajar con reinicios de escenas
public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        loading,
        inGame,
        gameOver
    }

    public GameState gameState;
    
    
    
    [SerializeField] private List<GameObject> targetPrefabs;
    [SerializeField] private float spawnRate = 1.0f;

    public TextMeshProUGUI scoreText;
    private int _score;

    private int Score
    {
        set
        {
            _score = Mathf.Clamp(value, 0,99999);
        }
        get
        {
            return _score;
        }
    }
    
    [SerializeField] private GameObject gameOverLayout; //using UnityEngine.UI;
    [SerializeField] private GameObject titleLayout;

    [Range(0,4)]private int playerLives = 4;

    [SerializeField] private List<GameObject> lives;
    // Start is called before the first frame update
    void Start()
    {
        ShowMaxScore();
    }
    /// <summary>
    /// Inicia el Game play
    /// </summary>
    public void StartGame(int difficulty)
    {
        gameState = GameState.inGame;
        titleLayout.gameObject.SetActive(false);
        spawnRate /= difficulty;
        playerLives -= difficulty;
        for (int i = 0; i < playerLives; i++)
        {
            lives[i].gameObject.SetActive(true);
        }
        StartCoroutine(SpawnTarget());
        Score = 0;
        UpdateScore(0);
    }

    IEnumerator SpawnTarget()
    {
        while (gameState == GameState.inGame)
        {
            yield return new WaitForSecondsRealtime(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[index]);
        }
    }

    /// <summary>
    /// Actualiza la puntuación y lo muestra por pantalla
    /// </summary>
    /// <param name="scoreToAdd">Número de puntos a añadir a la puntuación global</param>
    internal void UpdateScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        scoreText.text = "Score: \n" + Score;
    }
    /// <summary>
    /// Muestra la puntuación máxima
    /// </summary>
    internal void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("MAX_SCORE", 0);
        scoreText.text = "Max Score: \n" + maxScore;
    }
    /// <summary>
    /// Actualiza la puntuación máxima si la nueva puntuación es mayor
    /// </summary>
    internal void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("MAX_SCORE", 0);
        if (Score>maxScore)
        {
            PlayerPrefs.SetInt("MAX_SCORE", Score);
        }
    }

    private void ChangeHeartAlpha(int hearts)
    {
        if (hearts >= 0)
        {
            Image heartImage = lives[hearts].GetComponent<Image>();
            var tempColor = heartImage.color;
            tempColor.a = 0.3f;
            heartImage.color = tempColor; 
        }
        
    }
    public void GameOver(int State)
    {
        if (State == 0)
        {
            for (int i = 0; i < playerLives; i++)
            {
                ChangeHeartAlpha(i);
            }
            SetMaxScore();
            gameState = GameState.gameOver;
            gameOverLayout.gameObject.SetActive(true);
        }
        else if (State == 1)
        {
            playerLives--;
            ChangeHeartAlpha(playerLives);
            if (playerLives<=0)
            {
                SetMaxScore();
                gameState = GameState.gameOver;
                gameOverLayout.gameObject.SetActive(true);
            }
        }
        
    }
    
    /// <summary>
    /// Recarga la escena actual
    /// </summary>
    public void RestartGame() //Debe ser void para ser llamada por un Button
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
