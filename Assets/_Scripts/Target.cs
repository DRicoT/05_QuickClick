using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float minForce = 12;
    [SerializeField] private float maxForce = 17;
    [SerializeField] private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -5;
    [SerializeField] private int valuePoints;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private float volumeAudioClip;
    private AudioSource _audioSource; 
    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GameObject.Find("Game Manager").GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        _rigidbody.AddTorque(RandomTorque(),RandomTorque(), RandomTorque(),ForceMode.Impulse);
        transform.position = RandomSpawnPosition();
        gameManager = FindObjectOfType<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// Genera un vector aleatorio en 3D
    /// </summary>
    /// <returns>Fuerza aleatoria hacia arriba</returns>
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }
    
    /// <summary>
    /// Genera un número aleatorio
    /// </summary>
    /// <returns>Valor aleatorio entre -maxTorque y maxTorque</returns>
    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    
    /// <summary>
    /// Genera una posición aleatoria
    /// </summary>
    /// <returns>Posición aleatoria en 3D, con la coordenada z = 0</returns>
    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos); // z = 0
    }

    private void OnMouseDown()
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            Destroy(gameObject);
            _audioSource.PlayOneShot(explosionAudioClip, volumeAudioClip);
            Instantiate(explosionParticles, transform.position, explosionParticles.transform.rotation);
            gameManager.UpdateScore(valuePoints);
            if (gameObject.CompareTag("Bad"))
            {
                gameManager.GameOver(0);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Good"))
            {
                gameManager.GameOver(1);
            }
        }
    }
    
    
}
