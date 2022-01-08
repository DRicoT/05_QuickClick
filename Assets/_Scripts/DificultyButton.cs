using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DificultyButton : MonoBehaviour
{
    private Button _button;
    private GameManager gameManager;

    [SerializeField,Range(1,3)] private int difficulty;
    
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetDificulty);
        gameManager = FindObjectOfType<GameManager>();
    }

    void SetDificulty()
    {
        Debug.Log("Has elegido el nivel "+gameObject.name+" de dificultad.");
        gameManager.StartGame(difficulty);
    }
}
