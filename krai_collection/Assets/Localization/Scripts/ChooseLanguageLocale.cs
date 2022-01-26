using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class ChooseLanguageLocale : MonoBehaviour
{
    AsyncOperationHandle m_InitializeOperation;
    private List<Locale> locales;

    void Start()
    {
        // SelectedLocaleAsync will ensure that the locales have been initialized and a locale has been selected.
        m_InitializeOperation = LocalizationSettings.SelectedLocaleAsync;
        if (m_InitializeOperation.IsDone)
        {
            InitializeCompleted(m_InitializeOperation);
        }
        else
        {
            m_InitializeOperation.Completed += InitializeCompleted;
        }
    }

    void InitializeCompleted(AsyncOperationHandle obj)
    {
        locales = LocalizationSettings.AvailableLocales.Locales;
    }

    public void ChooseEng()
    {
        LocalizationSettings.SelectedLocale = locales[0];
        LanguageSettings.Singleton.isRussian = false;
        SceneManager.LoadScene("room_MainMenu");
    }
    public void ChooseRus()
    {
        LocalizationSettings.SelectedLocale = locales[1];
        LanguageSettings.Singleton.isRussian = true;
        SceneManager.LoadScene("room_MainMenu");
    }


}
