using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using krai_cvetok;

namespace krai_cvetok
{
    public class DialoguePrinter : MonoBehaviour
    {
        public static DialoguePrinter instance;
        public Elements elements;

        [System.Serializable]
        public class Elements
        {
            public GameObject SpeechPanel;
            public Text SpeechText;
        }
        public bool isSpeaking { get { return speaking != null; } }
        public GameObject SpeechPanel { get { return elements.SpeechPanel; } }
        public Text SpeechText { get { return elements.SpeechText; } }
        private int index = 0;
        private string[] str;
        private bool startSpeaking = false;
        [HideInInspector] public bool isWaitingForUserInput;
        private string targetSpeech = "";
        Coroutine speaking = null;
        Coroutine offPanel = null;

        //my staff
        private bool isDialogue;
        private GameManager gameManager;
        private SoundManager soundManager;

        private void Awake()
        {
            instance = this;
            gameManager = GetComponent<GameManager>();
            soundManager = SoundManager.Singleton;
        }

        private void Update()
        {
            if (isDialogue)
            {
                if (Input.GetMouseButtonDown(0) || startSpeaking)
                {
                    startSpeaking = false;
                    if (!isSpeaking || isWaitingForUserInput)
                    {
                        if (index >= str.Length)
                        {
                            if (offPanel != null)
                                StopCoroutine(offPanel);
                            offPanel = null;
                            SwitchOffTextPanel();
                            return;
                        }

                        Say(str[index]);
                        index++;
                    }
                }
            }
        }

        public void NewSay(string[] s)
        {
            str = s;
            startSpeaking = true;
            isDialogue = true;
        }


        public void Say(string speech)
        {
            StopSpeaking();
            speaking = StartCoroutine(Speaking(speech));
        }


        public void StopSpeaking()
        {
            if (isSpeaking)
            {
                StopCoroutine(speaking);
            }
            speaking = null;

        }


        IEnumerator Speaking(string speech)
        {
            SpeechPanel.SetActive(true);
            targetSpeech = speech;
            SpeechText.text = "";

            soundManager.PlaySpeech();
            isWaitingForUserInput = false;
            while (SpeechText.text != speech)
            {
                SpeechText.text += targetSpeech[SpeechText.text.Length];
                yield return new WaitForSeconds(0.02f);//WaitForEndOfFrame();
            }
            //text finished
            soundManager.StopPlayingSpeech();
            isWaitingForUserInput = false;
            while (isWaitingForUserInput)
                yield return new WaitForEndOfFrame();
            offPanel = StartCoroutine(SwitchOffTextPanelTimer());
            //Debug.Log("sdfsdf");
            StopSpeaking();
        }

        IEnumerator SwitchOffTextPanelTimer()
        {
            yield return new WaitForSeconds(4.0f);
            SwitchOffTextPanel();

        }
        private void SwitchOffTextPanel()
        {
            SpeechPanel.SetActive(false);
            isDialogue = false;
            gameManager.StopDialogue();
            index = 0;
            if (offPanel != null)
                StopCoroutine(offPanel);
            offPanel = null;
        }

    }
}
