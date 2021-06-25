using UnityEngine;
using UnityEngine.UI;


namespace krai_room
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField]
        private Slider musicSlider;

        [SerializeField]
        private Slider soundsSlider;

        [SerializeField]
        private Slider sensivitySlider;

        public void OnSettingsMenuOpen()
        {
            musicSlider.value = GetMusicSettings();
            soundsSlider.value = GetSoundsSettings();
            sensivitySlider.value = GetSensivitySettings();
        }

        public void SaveSettings()
        {
            SaveMusicSettings();
            SaveSensivitySettings();
            SaveSoundsSettings();
            PlayerPrefs.Save();
        }
        private void SaveMusicSettings()
        {
            PlayerPrefs.SetFloat("Music", musicSlider.value);
        }
        private void SaveSoundsSettings()
        {
            PlayerPrefs.SetFloat("Sounds", soundsSlider.value);
        }
        private void SaveSensivitySettings()
        {
            PlayerPrefs.SetFloat("Sensivity", sensivitySlider.value);
        }

        public static float GetMusicSettings()
        {
            return PlayerPrefs.GetFloat("Music", 1);
        }
        public static float GetSoundsSettings()
        {
            return PlayerPrefs.GetFloat("Sounds", 1);
        }
        public static float GetSensivitySettings()
        {
            return PlayerPrefs.GetFloat("Sensivity", 15);
        }
    }
}
