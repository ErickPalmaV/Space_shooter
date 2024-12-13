using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] InterfaceController interfaceController;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] GameObject player;
    [SerializeField] SoundController soundController;
    [SerializeField] ObjectPool[] objectPool;
    [SerializeField] private int score;
    bool gameIsRunning;
    private void Start()
    {   
        score = 0;
        player.GetComponent<ShipMediator>().Configure(this);
        StopGame();
        interfaceController.configure(this);
        interfaceController.StartMenu();
        foreach (var pool in objectPool)
        {
            pool.Configure(this);
        }
    }
    private void StopGame()
    {
        StopAllCoroutines();
        gameIsRunning = false;
        enemySpawner.IsGameRunning(gameIsRunning);
        player.SetActive(gameIsRunning);
        enemySpawner.gameObject.SetActive(gameIsRunning);
        Parallax.Enable = gameIsRunning;
        foreach (var pool in objectPool)
        {
            pool.DisableAll();
        }
    }
    public void GameOver()
    {
        StopGame();
        interfaceController.GameOver(score);
    }
    public void StartGame()
    {
        gameIsRunning = true;
        enemySpawner.IsGameRunning(gameIsRunning);
        player.SetActive(gameIsRunning);
        Parallax.Enable = gameIsRunning;
        enemySpawner.gameObject.SetActive(gameIsRunning);
        foreach (var pool in objectPool)
        {
            pool.StartGame();
        }
        StartCoroutine(ScoreUp());
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Sound()
    {
        soundController.SetAudio();
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EnemyDead()
    {
        score +=50;
        interfaceController.UpdateScore(score);
    }

    IEnumerator ScoreUp()
    {
        while (gameIsRunning)
        {
            yield return new WaitForSeconds(1f);
            score++;
            interfaceController.UpdateScore(score);
        }
        
    }

    public void setHealth(float health)
    {
        interfaceController.HealtBar(health);
    }
}