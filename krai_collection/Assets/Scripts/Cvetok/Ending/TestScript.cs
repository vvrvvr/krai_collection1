using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;
using UnityEngine.Video;
using krai_cvetok;

namespace krai_cvetok
{
    public class TestScript : MonoBehaviour
    {
        DialogueSystem dialogue;
        [SerializeField] Image girlImage;
        [SerializeField] Sprite cond1;
        [SerializeField] Sprite cond2;
        [SerializeField] Sprite cond3;
        [SerializeField] Sprite cond4;
        private bool startSpeaking;
        private bool hasControl = true;
        [SerializeField] Image transition;

        [SerializeField] GameObject[] endingsRus;
        [SerializeField] GameObject[] endingsEng;

        private string[] s = new string[0];
        public List<string[]> str;

        //video
        [SerializeField] VideoPlayer video;
        [SerializeField] GameObject videoScreen;

        //pause
        public static System.Action OnPauseEvent;

        //translation
        private bool isRussian;
        private void Awake()
        {
            video.Stop();
            videoScreen.SetActive(false);
            foreach (var item in endingsRus)
            {
                item.SetActive(false);
            }
            foreach (var item in endingsEng)
            {
                item.SetActive(false);
            }

            if (Endings.Singleton != null)
                isRussian = Endings.Singleton.isRussian;
            if (isRussian)
            {
                str = new List<string[]>
            {

                new string[]
                { "Привет, я не слишком опоздал?",
                "Я сама только что пришла... оу, ты принёс мне цветы|g",
                "Да, кажется это фиалки или розы, хочешь чая?",
                "Я бы предпочла фиалковый..|g"},
                new string[]
                { "Привет, как твои руки?",
                "Всё в порядке, ожоги не слишком сильные|g",
                "Я могу выбить для тебя пару недель отпуска вне плантации, может где-то на Отцветшем Севере",
                "Всё хорошо, правда, не забивай себе голову, ты же понимаешь, нам *надо* это сделать|g",
                "Действительно, кажется другого шанса может не быть, хочешь чая?",
                "Фиалковый, пожалуйста|g"},
                new string[]
                { "Я.. не.. опоздал?",
                "Зачем ты прятался, ты же знаешь, наша встреча это.. она предначертана судьбой, так что я всегда буду тебя ждать|g",
                "Это.. то что они тебе сказали?",
                "Ты забавный, кто это они?|g",
                "Кабинет двадца...",
                "[перебивает]: Ты странный, но.. всё в порядке, я принесла тебе маленький подарок: я нашла милого жука, bombus serrisquama, хочешь..|g",
                "[перебивает]: где вообще сейчас можно найти такое в наше время, это из лаборатории?",
                "Я же говорю, я гуляла в поле вчера вечером, и увидела его сидящим на ветке осины, хочешь я посажу его на твою камелию, мне кажется это было бы очень романтично и хорошо сказалось бы на нашем будущем.. опылении|g",
                "Я же.. я не могу отказаться, да?",
                "Ох.. конечно ты можешь, но с чего бы тебе это делать, подумай лучше о том, чем мы можем заняться вечером.. О да, и ещё, принеси мне чая, я хочу фиалковый.|g"},
                new string[]
                { "Я принесла тебе букет|g",
                "Привет..",
                "Я принесла тебе букет, это всё что осталось, всё что нам когда-либо мешало здесь, в этом букете, я вырвала это из себя и принесла тебе|g",
                "Знаешь..",
                "Нам больше не нужно быть рассредоточенными, я чувствую себя  спокойно, закрытой в комнате с тобой и этим букетом..|g",
                "Знаешь, я ведь тоже принёс тебе букет..",
                "А ещё я.. кажется я забыл твоё имя, старое или новое, что-то щёлкнуло в моей голове, и я совсем его забыл",
                "О не волнуйся, это было *то*|g",
                "Теперь меня зовут:|g",
                "Лилия|g"},

            };
            }
            else
            {
                str = new List<string[]>
            {

                new string[]
                { "Hi, am I too late?",
                "I just came myself... Oh, did you bring me flowers?|g",
                "Yes, it seems to be violets or roses. Would you like some tea?",
                "I would prefer the violet one.|g"},
                new string[]
                { "Hi, how are your hands?",
                "It's fine, burns are not too severe.|g",
                "I can get you a couple of weeks off the Plantation, maybe somewhere in the Faded North.",
                "It's ok, really, don't bother. You understand that we *should* do it|g",
                "Indeed, there may not be another chance, would you like some tea?",
                "Violet, please.|g"},
                new string[]
                { "Am... I... too late?",
                "Why did you hide? You know, our meeting (this) .. it is predetermined by the fate, so I will always wait for you.|g",
                "This... is what they told you?",
                "You are funny, who are \"they\"?|g",
                "Room twent...",
                "[Interrupting]: You're strange, but... It's all right, I brought you a small present. I found a nice bug, bombus serrisquama, do you want..|g",
                "[Interrupting]: Where is it possible to find such a thing nowadays? Is it from the laboratory?",
                "I'm telling you, I was walking in the field last night, and I saw it sitting on an aspen branch. Do you want me to plant it on your camellia? I think it would be very romantic and would have a good effect on our future .. pollination|g",
                "I... I can't refuse, can I?",
                "Oh sure you can, but why would you? Think better of what we can do tonight .. Oh yes, and also bring me some tea, I want the violet one.|g"},
                new string[]
                { "I brought you a bouquet.|g",
                "Hi...",
                "I brought you a bouquet, that's all what is left. Everything that has ever hindered us is now here in this bouquet. I ripped it out of myself and brought it to you.|g",
                "You know...",
                "We don't have to be dispersed anymore, I feel calm locked in this room with you and this bouquet.|g",
                "You know, I also brought you a bouquet ...",
                "And I also... It seems that I forgot your name, an old or a new one, something clicked in my head and I totally forgot it.",
                "Don't worry, it was *that*|g",
                "Now my name is:|g",
                "Lily|g"},

            };
            }
        }
        int index = 0;
        private int currentEnding;
        private void Start()
        {
            transition.DOFade(0f, 1.5f).OnComplete(() => startSpeaking = true);

            if (Endings.Singleton != null)
                currentEnding = Endings.Singleton.Ending;
            else
                currentEnding = 1;
            dialogue = GetComponent<DialogueSystem>();
            switch (currentEnding)
            {
                case 1:
                    s = str[0];
                    girlImage.sprite = cond1;
                    break;
                case 2:
                    s = str[1];
                    girlImage.sprite = cond2;
                    break;
                case 3:
                    s = str[2];
                    girlImage.sprite = cond3;
                    break;
                case 4:
                    s = str[3];
                    girlImage.sprite = cond4;
                    break;
            }

        }
        private void Update()
        {
            if (hasControl)
            {
                if (Input.anyKey || startSpeaking)
                {
                    startSpeaking = false;
                    if (!dialogue.isSpeaking || dialogue.isWaitingForUserInput)
                    {
                        if (index >= s.Length)
                        {
                            hasControl = false;
                            transition.DOFade(1f, 1.5f).OnComplete(() => EndingImage());//SceneManager.LoadScene("game"));
                            return;
                        }
                        Say(s[index]);
                        index++;
                    }
                }
            }
            if (Input.GetButtonDown("Cancel"))
            {
                OnPauseEvent?.Invoke();
            }
        }
        private void Say(string s)
        {
            string[] parts = s.Split('|');
            string speech = parts[0];
            string speaker = (parts.Length >= 2) ? parts[1] : "";
            dialogue.Say(speech, speaker);
        }
        private void EndingImage()
        {
            int endingsSum = 0;
            if (Endings.Singleton != null)
            {
                foreach (var item in Endings.Singleton.openedEndings)
                {
                    if (item == 1)
                        endingsSum++;
                }
            }
            else
                endingsSum = 1;
            if (isRussian)
                endingsRus[endingsSum - 1].SetActive(true);
            else
                endingsEng[endingsSum - 1].SetActive(true);
            StartCoroutine(RestartGame());
        }

        private IEnumerator RestartGame()
        {
            yield return new WaitForSeconds(4f);
            videoScreen.SetActive(true);
            video.Play();
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene("flower_game");
        }

    }
}
