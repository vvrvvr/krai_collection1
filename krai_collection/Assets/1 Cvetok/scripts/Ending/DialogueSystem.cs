using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using krai_cvetok;

namespace krai_cvetok
{
    public class DialogueSystem : MonoBehaviour
    {
        public static DialogueSystem instance;

        public ELEMENTS elements;
        [SerializeField] Font fontSelf;
        [SerializeField] Font fontGirl;
        [SerializeField] AudioClip[] girlTalks;
        [SerializeField] AudioClip[] selfTalks;
        private AudioSource audiosource;
        private int girlindex;
        private int selfindex;

        void Awake()
        {
            instance = this;
            audiosource = GetComponent<AudioSource>();
        }

        // Use this for initialization
        void Start()
        {
            girlindex = 0;
            selfindex = 0;
        }

        /// <summary>
        /// Say something and show it on the speech box.
        /// </summary>
        public void Say(string speech, string speaker = "")
        {
            StopSpeaking();

            speaking = StartCoroutine(Speaking(speech, false, speaker));
        }

        /// <summary>
        /// Say something to be added to what is already on the speech box.
        /// </summary>
        public void SayAdd(string speech, string speaker = "")
        {
            StopSpeaking();

            speechText.text = targetSpeech;

            speaking = StartCoroutine(Speaking(speech, true, speaker));
        }

        public void StopSpeaking()
        {
            if (isSpeaking)
            {
                StopCoroutine(speaking);
            }
            speaking = null;
        }

        public bool isSpeaking { get { return speaking != null; } }
        [HideInInspector] public bool isWaitingForUserInput = false;

        public string targetSpeech = "";
        Coroutine speaking = null;
        IEnumerator Speaking(string speech, bool additive, string speaker = "")
        {
            speechPanel.SetActive(true);
            targetSpeech = speech;

            if (!additive)
                speechText.text = "";
            else
                targetSpeech = speechText.text + targetSpeech;

            DetermineSpeaker(speaker);//temporary

            isWaitingForUserInput = false;

            while (speechText.text != targetSpeech)
            {
                speechText.text += targetSpeech[speechText.text.Length];
                yield return new WaitForSeconds(0.03f);//WaitForEndOfFrame();
            }
            audiosource.Stop();
            //text finished
            isWaitingForUserInput = true;
            while (isWaitingForUserInput)
                yield return new WaitForEndOfFrame();

            StopSpeaking();
        }

        private void DetermineSpeaker(string s)
        {
            if (s == "")
            {
                SpeechBoxImageSelf.SetActive(true);
                SpeechBoxImageGirl.SetActive(false);
                speechText.font = fontSelf;
                PlaySpeech(0);
            }
            else
            {
                SpeechBoxImageSelf.SetActive(false);
                SpeechBoxImageGirl.SetActive(true);
                speechText.font = fontGirl;
                PlaySpeech(1);
            }
            //string retVal = speakerNameText.text;//default return is the current name
            //if (s != speakerNameText.text && s != "")
            //	retVal = (s.ToLower().Contains("narrator")) ? "" : s;


        }

        [System.Serializable]
        public class ELEMENTS
        {
            /// <summary>
            /// The main panel containing all dialogue related elements on the UI
            /// </summary>
            public GameObject speechPanel;
            // public Text speakerNameText;
            public Text speechText;
            public GameObject SpeechBoxImageSelf;
            public GameObject SpeechBoxImageGirl;
        }

        private void PlaySpeech(int i)
        {
            // self voice
            if (i == 0)
            {

                var index = Random.Range(0, selfTalks.Length);
                if (index == selfindex)
                {
                    while (index == selfindex)
                    {
                        index = Random.Range(0, selfTalks.Length);
                    }
                }
                selfindex = index;
                audiosource.clip = selfTalks[selfindex];
                audiosource.Play();
            }
            //girl voice
            if (i == 1)
            {

                var index = Random.Range(0, girlTalks.Length);
                if (index == girlindex)
                {
                    while (index == girlindex)
                    {
                        index = Random.Range(0, girlTalks.Length);
                    }
                }
                girlindex = index;
                audiosource.clip = girlTalks[girlindex];
                audiosource.Play();
            }


        }



        public GameObject speechPanel { get { return elements.speechPanel; } }
        //public Text speakerNameText { get { return elements.speakerNameText; } }
        public Text speechText { get { return elements.speechText; } }
        public GameObject SpeechBoxImageSelf { get { return elements.SpeechBoxImageSelf; } }
        public GameObject SpeechBoxImageGirl { get { return elements.SpeechBoxImageGirl; } }
    }
}
