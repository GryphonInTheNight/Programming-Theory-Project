using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //ENCAPSULATION
    /// <summary>
    /// Singleton instance of the game manager.
    /// </summary>
    public static GameManager Instance { get; private set; }
    /// <summary>
    /// Whether the player has lost.
    /// </summary>
    public bool hasLost { get; private set; }
    [SerializeField] private PlayerController player;
    /// <summary>
    /// Array of all enemy types.
    /// </summary>
    [SerializeField] private Enemy[] typesOfEnemies;
    /// <summary>
    /// Radius for spawning enemies.
    /// </summary>
    [SerializeField] private float spawnRange = 60.0f;
    /// <summary>
    /// The game score.
    /// </summary>
    [SerializeField] private int score;
    /// <summary>
    /// The wave number.
    /// </summary>
    [SerializeField] private int waveNumber;
    /// <summary>
    /// The TextMeshPro gui for the score.
    /// </summary>
    [SerializeField] private TextMeshProUGUI scoreText;
    /// <summary>
    /// The TextMeshPro gui for the wave.
    /// </summary>
    [SerializeField] private TextMeshProUGUI waveText;
    /// <summary>
    /// The game object holding the loss screen.
    /// </summary>
    [SerializeField] private GameObject loseScreen;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        //Initializes things.
        score = 0;
        waveNumber = 1;
        hasLost = false;
        UpdateGUI();
        SpawnWave(1);
    }

    /// <summary>
    /// Spawns a new wave of enemies.
    /// </summary>
    /// <param name="numOfEnemies">Number of enemies to spawn.</param>
    private void SpawnWave(int numOfEnemies)
    {
        for (int i = 0; i < numOfEnemies; i++)
        {
            Enemy toSpawn = typesOfEnemies[Random.Range(0, typesOfEnemies.Length)];
            //spawns at a random angle at a distance of spawnRange.
            float angle = Random.Range(0, 2 * Mathf.PI);
            Enemy spawned = Instantiate(toSpawn,
                new Vector3(Mathf.Cos(angle)*spawnRange, toSpawn.transform.position.y, Mathf.Sin(angle)*spawnRange),
                toSpawn.transform.rotation);
            //makes the enemy go after the player.
            spawned.target = player.gameObject;
        }
       // foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>())
         //   enemy.target = player.gameObject;
    }
    /// <summary>
    /// Updates the score.
    /// </summary>
    /// <param name="scoreChange">The amount by which to change the score.</param>
    public void UpdateScore(int scoreChange)
    {
        score += scoreChange;
        UpdateGUI();
    }
    /// <summary>
    /// Checks if there is one or fewer enemies left in the scene, which should happen when the last enemy on screen is destroyed.
    /// If so, creates a new wave!
    /// </summary>
    public void CheckIfReadyToSpawn()
    {
        if (GameObject.FindObjectsOfType<Enemy>().Length <= 1)
        {
            waveNumber++;
            SpawnWave(waveNumber);
            UpdateGUI();
        }
    }
    /// <summary>
    /// Updates the GUI.
    /// </summary>
    private void UpdateGUI()
    {
        scoreText.text = "Score: " + score;
        waveText.text = "Wave: " + waveNumber;
    }
    public void LoseGame()
    {
        foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>())
            enemy.target = null;
        loseScreen.SetActive(true);
        hasLost = true;
    }
    /// <summary>
    /// Restarts the game.
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
