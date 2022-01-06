using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


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
    
    [SerializeField] private TextMeshProUGUI gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.inGame;
        StartCoroutine(SpawnTarget());
        Score = 0;
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void GameOver()
    {
        gameState = GameState.gameOver;
        gameOverText.gameObject.SetActive(true);
    }
}
