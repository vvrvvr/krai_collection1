using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

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
            Debug.Log("b_if");
            InitializeCompleted(m_InitializeOperation);
            Debug.Log("if");
        }
        else
        {
            Debug.Log("b_else");
            m_InitializeOperation.Completed += InitializeCompleted;
            Debug.Log("else");
        }
    }

    void InitializeCompleted(AsyncOperationHandle obj)
    {
        Debug.Log("b_complete");
        locales = LocalizationSettings.AvailableLocales.Locales;
        Debug.Log("complete");
    }

    public void ChooseEng()
    {
        LocalizationSettings.SelectedLocale = locales[0];
        Debug.Log(locales[0]);
    }
    public void ChooseRus()
    {
        LocalizationSettings.SelectedLocale = locales[1];
    }


}
