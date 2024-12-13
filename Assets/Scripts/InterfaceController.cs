
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    [SerializeField]ScreenFade screenFade;
    [SerializeField]Menu menu;
    [SerializeField]GameOver gameOver;
    [SerializeField]Button playButton;
    [SerializeField]Button exitButton;
    [SerializeField]Button soundButton;
    [SerializeField]Button menuButton;
    [SerializeField]TextMeshProUGUI score;
    [SerializeField]TextMeshProUGUI gameOverScore;
    [SerializeField]Hud hud;
    [SerializeField] Image HealtBarImage;
    GameController _gameController;
    
    private void Start()
    {
        playButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
        soundButton.onClick.AddListener(Sound);
        menuButton.onClick.AddListener(Menu);
    }
    
    public void configure( GameController gameController )
    {
        _gameController = gameController;
    }
    
    public void StartMenu()
    {
        screenFade.gameObject.SetActive(true);
        screenFade.FadeOutScreen();
    }
    public void StartGame()
    {
        screenFade.gameObject.SetActive(true);
        screenFade.FadeInOutScreen(menu.gameObject);
        _gameController.StartGame();
    }

    public void ExitGame()
    {
        _gameController.ExitGame();
    }
    public void Sound()
    {
        _gameController.Sound();
    }

    public void UpdateScore(int score)
    {
        var scoreText = "Score: " + score;
        this.score.text = scoreText;
    }

    public void GameOver(int score)
    {
        hud.gameObject.SetActive(false);
        var scoreText = "Score: " + score;
        gameOver.gameObject.SetActive(true);
        gameOverScore.text = scoreText;
    }

    public void Menu()
    {
        _gameController.Reset();
    }

    public void HealtBar(float healt)
    {
        HealtBarImage.fillAmount = healt;
    }
}