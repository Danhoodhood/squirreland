using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageSwitcher : MonoBehaviour
{
    // ћетод дл€ переключени€ €зыка
    public void SwitchLanguage(string localeIdentifier)
    {
        // ”становите новый €зык
        Locale newLocale = LocalizationSettings.AvailableLocales.GetLocale(localeIdentifier);
        if (newLocale != null)
        {
            LocalizationSettings.SelectedLocale = newLocale;
            Debug.Log($"язык изменен на: {newLocale.LocaleName}");
        }
        else
        {
            Debug.LogWarning($"язык {localeIdentifier} не найден.");
        }
    }
}
