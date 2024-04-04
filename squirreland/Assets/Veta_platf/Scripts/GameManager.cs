using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform endMarker;
    public Transform player;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public Button restartButton;
    

    private bool hasWon = false;
    private bool hasLost = false;
  

    void Start()
    {
        Time.timeScale = 1;
        Save_progress.Save();
    }

    void Update()
    {
        // Проверка условия проигрыша 1: игрок упал вниз
        if (player.position.y < 0f && !hasLost)
        {
            hasLost = true;
            ShowLoseMessage();
        }

        // Проверка условия проигрыша 2: игрок выбрал на кнопке завершить игру
        if (hasLost && Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        // Проверка условия выигрыша: наступил на последний блок земли
        if (Vector2.Distance(player.position, endMarker.position) < 1.5f && !hasWon)
        {
            ShowWinMessage();
        }
    }

    void ShowWinMessage()
    {

        winText.gameObject.SetActive(true);
        SceneManager.LoadScene(3);
    }

    void ShowLoseMessage()
    {
        loseText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
