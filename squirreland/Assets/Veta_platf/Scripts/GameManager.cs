using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Transform endMarker;
    public Transform player;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public Button restartButton;
    public GameObject LOAD;
    

    private bool hasWon = false;
    private bool hasLost = false;
    [SerializeField] private AudioSource audioSourceFallPlayer;

    void Start()
    {
        Time.timeScale = 1;
        Save_progress.Save();
    }

    void Update()
    {
        // Проверка условия проигрыша 1: игрок упал вниз
        if (player.position.y < -12f && !hasLost)
        {

            // Проигрываем звук падения игрока
            audioSourceFallPlayer.Play();

            // Вызываем функцию ShowLoseMessage с задержкой в 1 секунду
            Invoke("ShowLoseMessage", 3f);

            hasLost = true;// проиграл
            HeartSystem.health = 0;
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
        StartCoroutine(Wait_for_win());
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
    IEnumerator Wait_for_win()
    {
        yield return new WaitForSeconds(2);
        LOAD.GetComponent<LOAD>().load_please();
    }
}
