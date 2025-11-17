using System.Collections;
using Dan.Main;
using Dan.Models;
using TMPro;
using UnityEngine;

namespace Dan.Demo
{
    public class LeaderboardShowcase : MonoBehaviour
    {
        [Header("Gameplay:")]
        [SerializeField] private TextMeshProUGUI _playerScoreText;  // Текст с локальным счётом игрока

        [Header("Leaderboard Essentials:")]
        [SerializeField] private TMP_InputField _playerUsernameInput; // Имя игрока для отправки в лидерборд
        [SerializeField] private Transform _entryDisplayParent;       // Родитель UI-элементов в таблице
        [SerializeField] private EntryDisplay _entryDisplayPrefab;    // Префаб строки таблицы
        [SerializeField] private CanvasGroup _leaderboardLoadingPanel; // Панель "загрузка"

        [Header("Search Query Essentials:")]
        [SerializeField] private TMP_Dropdown _timePeriodDropdown;    // Фильтр по периоду (день/неделя/месяц/всё)
        [SerializeField] private TMP_InputField _pageInput, _entriesToTakeInput; // Пагинация
        [SerializeField] private int _defaultPageNumber = 1, _defaultEntriesToTake = 100;

        [Header("Personal Entry:")]
        [SerializeField] private RectTransform _personalEntryPanel;   // Панель личного рекорда
        [SerializeField] private TextMeshProUGUI _personalEntryText;

        private int _playerScore;                                     // Локальный счёт (из PlayerPrefs)
        private Coroutine _personalEntryMoveCoroutine;

        public void Load()
        {
            // ✔ Загружаем локальный счёт
            _playerScore = PlayerPrefs.GetInt("High_Score", 0);
            _playerScoreText.text = $"Ваш счёт: {_playerScore}";

            // ✔ Определяем выбранный период таблицы
            var timePeriod =
                _timePeriodDropdown.value == 1 ? Dan.Enums.TimePeriodType.Today :
                _timePeriodDropdown.value == 2 ? Dan.Enums.TimePeriodType.ThisWeek :
                _timePeriodDropdown.value == 3 ? Dan.Enums.TimePeriodType.ThisMonth :
                _timePeriodDropdown.value == 4 ? Dan.Enums.TimePeriodType.ThisYear : Dan.Enums.TimePeriodType.AllTime;

            // ✔ Пагинация: страница
            var pageNumber = int.TryParse(_pageInput.text, out var pageValue) ? pageValue : _defaultPageNumber;
            pageNumber = Mathf.Max(1, pageNumber);
            _pageInput.text = pageNumber.ToString();

            // ✔ Количество результатов
            var take = int.TryParse(_entriesToTakeInput.text, out var takeValue) ? takeValue : _defaultEntriesToTake;
            take = Mathf.Clamp(take, 1, 100);
            _entriesToTakeInput.text = take.ToString();

            // ✔ Формируем запрос к лидерборду
            var searchQuery = new LeaderboardSearchQuery
            {
                Skip = (pageNumber - 1) * take,
                Take = take,
                TimePeriod = timePeriod
            };

            _pageInput.image.color = Color.white;
            _entriesToTakeInput.image.color = Color.white;

            // ✔ Загружаем таблицу
            Leaderboards.DemoSceneLeaderboard.GetEntries(searchQuery, OnLeaderboardLoaded, ErrorCallback);
            ToggleLoadingPanel(true);
        }

        // Кнопки переключения страниц
        public void ChangePageBy(int amount)
        {
            var pageNumber = int.TryParse(_pageInput.text, out var pageValue) ? pageValue : _defaultPageNumber;
            pageNumber += amount;
            if (pageNumber < 1) return;
            _pageInput.text = pageNumber.ToString();
        }

        private void OnLeaderboardLoaded(Entry[] entries)
        {
            // ✔ Чистим старые элементы
            foreach (Transform t in _entryDisplayParent)
                Destroy(t.gameObject);

            // ✔ Создаём UI-элементы под каждую запись
            foreach (var t in entries)
                CreateEntryDisplay(t);

            ToggleLoadingPanel(false);
        }

        private void ToggleLoadingPanel(bool isOn)
        {
            _leaderboardLoadingPanel.alpha = isOn ? 1f : 0f;
            _leaderboardLoadingPanel.interactable = isOn;
            _leaderboardLoadingPanel.blocksRaycasts = isOn;
        }

        // Анимация боковой панели личного рекорда
        public void MovePersonalEntryMenu(float xPos)
        {
            if (_personalEntryMoveCoroutine != null)
                StopCoroutine(_personalEntryMoveCoroutine);
            _personalEntryMoveCoroutine = StartCoroutine(MoveMenuCoroutine(_personalEntryPanel,
                new Vector2(xPos, _personalEntryPanel.anchoredPosition.y)));
        }

        private IEnumerator MoveMenuCoroutine(RectTransform rectTransform, Vector2 anchoredPosition)
        {
            const float duration = 0.25f;
            var time = 0f;
            var startPosition = rectTransform.anchoredPosition;

            while (time < duration)
            {
                time += Time.deltaTime;
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, anchoredPosition, time / duration);
                yield return null;
            }

            rectTransform.anchoredPosition = anchoredPosition;
            _personalEntryMoveCoroutine = null;
        }

        private void CreateEntryDisplay(Entry entry)
        {
            var entryDisplay = Instantiate(_entryDisplayPrefab.gameObject, _entryDisplayParent);
            entryDisplay.GetComponent<EntryDisplay>().SetEntry(entry);
        }

        // Анимация текста "Загрузка..."
        private IEnumerator LoadingTextCoroutine(TMP_Text text)
        {
            var loadingText = "Загрузка";
            for (int i = 0; i < 3; i++)
            {
                loadingText += ".";
                text.text = loadingText;
                yield return new WaitForSeconds(0.25f);
            }

            StartCoroutine(LoadingTextCoroutine(text));
        }

        private void InitializeComponents()
        {
            StartCoroutine(LoadingTextCoroutine(_leaderboardLoadingPanel.GetComponentInChildren<TextMeshProUGUI>()));

            _pageInput.onValueChanged.AddListener(_ => _pageInput.image.color = Color.yellow);
            _entriesToTakeInput.onValueChanged.AddListener(_ => _entriesToTakeInput.image.color = Color.yellow);

            _pageInput.placeholder.GetComponent<TextMeshProUGUI>().text = _defaultPageNumber.ToString();
            _entriesToTakeInput.placeholder.GetComponent<TextMeshProUGUI>().text = _defaultEntriesToTake.ToString();
        }

        private void Start()
        {
            InitializeComponents();
            Load(); // ✔ загрузка локального счёта + запрос таблицы
        }

        // ⬆ Отправка рекорда игрока в лидерборд
        public void Submit()
        {
            Leaderboards.DemoSceneLeaderboard.UploadNewEntry(_playerUsernameInput.text, _playerScore, Callback, ErrorCallback);
        }

        // ⬆ Удаление своей записи
        public void DeleteEntry()
        {
            Leaderboards.DemoSceneLeaderboard.DeleteEntry(Callback, ErrorCallback);
        }

        // Сброс авторизации игрока
        public void ResetPlayer()
        {
            LeaderboardCreator.ResetPlayer();
        }

        // Загрузка только своей записи
        public void GetPersonalEntry()
        {
            Leaderboards.DemoSceneLeaderboard.GetPersonalEntry(OnPersonalEntryLoaded, ErrorCallback);
        }

        private void OnPersonalEntryLoaded(Entry entry)
        {
            _personalEntryText.text = $"{entry.RankSuffix()}. {entry.Username} : {entry.Score}";
            MovePersonalEntryMenu(0f);
        }

        private void Callback(bool success)
        {
            if (success)
                Load();
        }

        private void ErrorCallback(string error)
        {
            Debug.LogError(error);
        }
    }
}
