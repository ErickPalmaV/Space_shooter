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
    bool _gameIsRunning;
    private void Start()
    {   
        score = 0;
        player.GetComponent<ShipMediator>().Configure(this);
        StopGame();
        interfaceController.Configure(this);
        interfaceController.StartMenu();
        foreach (var pool in objectPool)
        {
            pool.Configure(this);
        }
    }
    private void StopGame()
    {
        StopAllCoroutines();
        _gameIsRunning = false;
        enemySpawner.IsGameRunning(_gameIsRunning);
        player.SetActive(_gameIsRunning);
        enemySpawner.gameObject.SetActive(_gameIsRunning);
        Parallax.Enable = _gameIsRunning;
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
        _gameIsRunning = true;
        enemySpawner.IsGameRunning(_gameIsRunning);
        player.SetActive(_gameIsRunning);
        Parallax.Enable = _gameIsRunning;
        enemySpawner.gameObject.SetActive(_gameIsRunning);
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
        while (_gameIsRunning)
        {
            yield return new WaitForSeconds(1f);
            score++;
            interfaceController.UpdateScore(score);
        }
        
    }

    public void SetHealth(float health)
    {
        interfaceController.HealtBar(health);
    }
}
