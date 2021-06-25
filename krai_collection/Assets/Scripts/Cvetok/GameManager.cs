using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using krai_cvetok;

namespace krai_cvetok
{
    public class GameManager : MonoBehaviour
    {
        public int worldState = 0;
        private int noteNumber = 0;
        [SerializeField] private ObjectState[] changingObjects = new ObjectState[0];
        [SerializeField] private Furniture[] furniture = new Furniture[0];
        [SerializeField] private GameObject[] environment;
        [SerializeField] private Material[] skyboxes;
        [Header("       notes")]
        [SerializeField] GameObject noteObject;
        private string[] notes = new string[7];
        [SerializeField] private Image note;
        [SerializeField] private Text noteText;
        [SerializeField] private FirstPersonAIO player;
        [Header("footsteps")]
        [SerializeField] private List<AudioClip> grassSet1;
        [SerializeField] private List<AudioClip> grassSet2;
        [SerializeField] private List<AudioClip> grassSet4;
        [SerializeField] private List<AudioClip> grassSet6;

        [SerializeField] private List<AudioClip> betonSet1;
        [SerializeField] private List<AudioClip> betonSet2;
        [SerializeField] private List<AudioClip> betonSet4;
        [SerializeField] private List<AudioClip> betonSet6;

        //notes and flowers
        [Header("notes and flowers")]
        [SerializeField] private Text notesText;
        [SerializeField] private Text flowersText;
        private int currentFlowers;
        private int maxFlowers = 5;
        private int currentNotes;
        private int maxNotes = 6;


        //dialogue
        private bool isDialogue;
        private Vector3 objectToLook;
        DialoguePrinter dialogue;
        private bool controllerOnPause;
        public bool isSwitchTrack;

        private bool isNoteShowing;
        public string[] doorPhrase = new string[] { "Цветов слишком мало для букета" };


        //transition
        [SerializeField] private Image transition;


        //saturation
        [SerializeField] Volume vol;
        private Bloom b;

        //translation
        private bool isRussian;
        void Start()
        {

            isRussian = Endings.Singleton.isRussian;
            vol.profile.TryGet(out b);
            currentFlowers = 0;
            currentNotes = 0;
            notesText.text = $"{currentNotes}/{maxNotes}";
            dialogue = DialoguePrinter.instance;
            flowersText.text = $"{currentFlowers}/{maxFlowers}";
            Cursor.visible = false;
            //note.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            if (isRussian)
            {
                notes[0] = "не используется, потому что я так захотел";
                notes[1] = $"И тогда он заявил: \"Ты хороший механизатор, но сейчас тебе нужно остановиться, такие исследования больше не прерогатива ЦАГИ, отдохни немного, возьми отпуск, я не знаю, посети соляные пещеры в конце-концов\". Его зрачок(чёрный с жёлтым ободком) бешено вращался по радужке, так что можно было сказать наверняка - он смущён, возможно даже немного напуган, и несмотря на его горестный вид, так и говорящий о внеплановом урезании бюджетов, истинная причина этого разговора крылась в странной одержимости нынешнего руководства. Он отвернулся к резервуарам, в явной попытке скрыть нервозность, завладевшую им во время этого вынужденного разговора, и, когда я уже собирался выходить из кабинета, добавил: \"Может попробуешь найти кого-нибудь, пока будешь там, просто..мы уже..просто, это не должен быть кто - то особенный, ну подумай..какой пример, не очень..приятный\".   С каждым разом, когда он заводил этот разговор, заминки , болезненные предыхания, паузы в попытках подобрать слова - эти вечные эндемики его мыслительного процесса постепенно выкраивались из его речи, освобождая место категорическому императиву, сформулированному здесь и далее как оппозит принципу неизвестности. Она смотрит с любопытством, как бы спрашивая: \"насколько это продукт любви, а насколько продукт ужаса ? \". Она хихикает, и в отрывках её заигрываний :\"Я не хочу быть предназначена\". Гаденько мимикрируя, вы сливаетесь в архитипическом процессе подкупа, как  демонстрации силы. \"Я люблю розы, но цветом менее насыщенным, чем #FF0000\", тогда, бездумно срывая цветы, ты обозначаешь свою субъектность.";
                notes[2] = "Изорванность и неестественность - это первое, о чём я вспоминаю, когда думаю о ней. Я видел подобное и раньше - солнечные ожоги неравномерно покрывали части её тела, отражая выверенную картографию, возникшую как результат перепланировки фабрики. Позже, когда я пытался представить наше первое свидание, я пришёл к выводу, что вынужденная дискретность её движений никак нам не помешает, напротив - может помочь. Вырезанные последовательности её визуальных проявлений напоминали тогда численный ряд, неуклонно сходящийся к одной единственный точке. Конечный элемент расчёта, в таком случае, представлялся мне моментом любовного триумфа, объединение двух разнородных сущностей под надзором времени и, возможно, некоего высшего сознания -Бога, или более вероятно, комнаты 306 ОКК.В любом случае, утрата контроля в рамках данного цикла подразумевала возможность восстановления в цикле следующем, так что рано или поздно, возможно, когда я принесу цветы, и она тем самым завершит букет, который всегда держит при себе.";
                notes[3] = "Однажды Тристан Тцара вставил подсолнух себе в глаз. Белые трафаретные буквы на стене плантации: \"Здесь был Флавермэн\".Вопрос о целесообразности использования белой краски при световом потоке в 20000 люмен никогда серьёзно не поднимался.Сообщение было передано на другом уровне -Каротиноидами, содержащимися в качестве присадок в нефтехимии, \"Не уподобляйтесь синтетическим системам\", после чего -пространное рассуждение о природе флороязыков и невозможности цветочного разумения.Тогда я снова подумал о нас, в этот раз, как о счетверённой системе, транспримитивизм которой был так же достижим как и любой другой примирительный опыт. Пока же мы вместе бродили по зловещей долине, никогда не касаясь медианной точки: начиная свой путь в инертности, и последовательно проходя все стадии приближения к одушевлённости.";
                notes[4] = "Я чувствую, что моя цветочная часть хочет её больше, чем человеческая. Я наблюдал за ней несколько дней назад из окна моего кабинета, тогда она стояла в одном из ангаров плантации, подчиняющейся Министерству Хлоропитания. Её вместе с сотней других работников облучали жёстким коротковолновым излучением, и  из трубок на её шее сочилась мутная жидкость: колокольчики, фиалки, может даже полынь. Труд в этом случае имел своей основой не только отчуждение деятельности, но и отчуждение самого бытия, как минимум *той* его части. Позже, подключенный к аппарату с бутлегерским хим-порно, я вдруг(?) одуванчики, вернее я почуствовал не-ос - мы - сля - е - мо - е сродство с ней, где любовь осмыслялась мной как некий кризис цветочной унификации, каждый цветок в моей голове был подобен соответствующему в ней. \"Это не я\", -подумал я тогда, \"Мыслить цветы - кратчайший путь к расстройствам\".";
                notes[5] = "Иногда мне кажется, что слова Мао были восприняты слишком буквально. Факт в том, что мы живём при цветочном коммунизме. Это надтоталитарная (?)-идеология, однажды принятая всеми нами и с тех пор выцветшая не более чем на два тона.Я видел членов Grüncommando, прямиком из цветочной комнаты, крутящихся рядом с моей квартирой, иногда я чувствую, как их системы прорастают из плесени в моей ванной.Фирма напрямую в подчинении у Министерства Геномного Разнообразия, вечно грезящим идеями эволюционного прогресса, должно быть следят за мной долгое время, должно быть они видели пыльцу на моих пальцах, видели как я копаюсь в соцветиях lilium bulbiferum, проросших из моих лодыжек, может у них даже есть фотографии.Они определяют это как сексуально - политическую перверсию, боятся, ненавидят: \"из собственных цветков произрастают диссиденты\",  достаточно, чтобы понять: они не потерпят замкнутости на себе, и пресекут любую попытку погружения в невременье.Сегодня я нашёл под дверью записку с прикреплённым к ней жуком, *они * дают мне последний шанс, может быть и она из* них*, \"вас и семьёй не назвать, но смотрите, что у меня есть: новый телевизор\".Я чувствую тревогу уже несколько дней, надо подготовиться.";
                notes[6] = "В те дни, в приступах отчаяния, я несколько раз пытался перестать дышать, запершись в туалетной комнате, перекрыв вентиляцию, упершись одной ногой в стиральную машину, другой - в дверной косяк. Внутри пустеет, я засыпаю, только  чтобы проснуться на обочине мостовой, со словами, написанными биолюминисцентной краской на руке:\"Подбросьте меня домой, пожалуйста, я перебрал\". Они используют нас, управляют нашим развитием против законов цветочной диалектики. Колебательный контур здесь на улицах всё больше смещён в зелёный его спектр, тогда как наверху он перестал быть (?)-уловимым. Я всё меньше(?) человеческие эмоции, остаточные знания теперь укладываются в простейшую гормонально-ферментную сетку, с каждодневно выпадающими по закону B-I - N - G - O элементами.Сдвоенные системы здесь, в ангарах, всё больше показывают свою несостоятельность, огорачают *его * или уже* их*, они разлагаются, фальшивят, раз за разом проходя попытки конверсии, они неизбежно принимают какую-либо из сторон, и навечно в ней застывают.Я больше не думаю, что я живу здесь.Я не думаю, что я живу здесь, и я чувствую, как перестаю понимать(?)/ языковые  конструкции.Впервые в своей жизни, стоя перед дырой, куда осыпаются обе части моего бытия, я принял неотвратимость выбора.Цветы, что я собрал сегодня утром, пахнут прекрасно.";
            }
            else
            {
                notes[0] = "not used";
                notes[1] = "And then he claimed: “You are a good machine operator but now you have to stop, such research is no longer the TsAGI’s prerogative, have a rest, take a vacation, I don’t know, visit the salt caves after all”. The pupil of his eye (black with a yellow rim) was turning madly around the iris so one could definitely conclude he was embarrassed, maybe even a bit frightened. Despite his sorrowful appearance as if talking about unplanned budget cuts the real reason for this conversation was hidden in a strange obsession of the new governance. He turned his face towards the water tanks in an apparent attempt to hide the nervousness he was overcoming during this forced conversation. When I was already going to leave, he added: “Maybe you can try to find somebody while you’re there, I mean... just... we have already... It shouldn’t be someone special, you know... The example is not really… pleasant”. Each time he started this conversation, there were these stumbles, painful aspirations, pauses in attempts to choose the correct words. All these permanent endemics of his thinking process were gradually carving out of his speech, leaving free space to the categorical imperative formulated hereinafter as the opposite to the principle of the unknown. She looks with curiosity as if asking: “To what extent is it a product of love or a product of horror?” She giggles, and that’s what in the fragments of her flirting: “I don’t want to be dedicated”. Nastily mimicking you merge in an archetypal process of bribe as the demonstration of force. “I like roses but less saturated than #FF000”, then mindlessly picking flowers you denote your subjectivity”.";
                notes[2] = "Tatteredness and unnaturalness are the two first things I remember when I think of her. I’ve seen the same before: sunburns covered parts of her body unevenly, showing carefully calculated cartography popped up as a result of the factory redevelopment. Later, when I tried to imagine our first date, I concluded that the forced discreteness of her movements won’t hinder us in any way, on the contrary it can even help. Tattered consequences of her visual manifestations reminded me of a row of numbers converging steadily to one single point. In this case, the final element of the calculation seems to me a moment of the triumph of love, a merge of two heterogeneous essences under the supervision of time and probably some superior consciousness, a God or more likely a room 306 OKK. Anyway, the loss of control in the frame of one cycle implied the possibility of its regaining in the following one, so it’ll happen sooner or later, probably when I bring the flowers, and she will thereby complete the bouquet that she always keeps with her.";
                notes[3] = "One day Tristan Tzara put a sunflower in his eye. White stencil letters on the plantation’s wall: “Flowerman was here”. The question of the advisability of using white paint with a luminous flux of 20,000 lumens has never been seriously raised. The following message was transferred on another level by the Carotenoids contained as additives in petrochemistry: “Do not become like synthetic systems''.After that there was a lengthy judgment about the nature of floral languages and the impossibility of floral comprehension. And again I thought about you, this time as of a quadruple system whose transprimitivism was as achievable as any other conciliatory experience.In the meantime, we wandered together through the ominous valley never touching the median point: starting our path in inertia and successively going through all the stages to achieve spirituality.";
                notes[4] = "I feel that my floral part wants her more than my human one. A few days ago I was watching her from my office window, she was standing in one of the hangars of the plantation subordinate to the Ministry of Chlorine Nutrition. She, along with a hundred other workers, was irradiated with hard shortwave radiation and some cloudy liquid oozed from the tubes on  her neck: bells, violets, maybe even wormwood. Labor in this case had as its basis not only the alienation of activity but also the alienation of existence itself, at least of * that * part of it. Later, connected to the bootlegged chemical porn machine, I suddenly (?) dandelions, I mean I felt an un-in-tel-li-gible kinship with her, where love was interpreted by me as a kind of crisis of floral unification, each flower in my head was similar to the corresponding flower in her. “This is not me”, I thought. “Thinking flowers is the shortest way to disorders”.";
                notes[5] = "Sometimes it seems to me that Mao’s words were understood too literally. The fact is that we live under floral communism. It’s a supra-totalitarian (?)-ideology once adopted by all of us and since then faded by no more than two tones. I saw the members of Grüncommando, straight from the floral room, hanging out near my flat. Sometimes I feel their systems sprouting out of the mold in my bathroom. The firm is under direct supervision of the Ministry of Genomic Diversity, permanently dreaming of evolutionary progress ideas. They should be following me for a while, they should have seen the pollen on my finger and how I dig in inflorescences of lilium bulbiferum sprouted from my ankles, they probably even have photographs. They define it as a sexual-political perversion, they are afraid of it, they hate it. “Dissidents grow from their own flowers”, it’s enough to understand: they won’t tolerate self-obsession and they will suppress every attempt to dive into timelessness. Today I found a note under my door with a bug attached to it. “They” give me the last chance, and she is probably one of “them”, “you can’t even be considered as a family, but look what I have: a new TV”. I have been feeling anxious for several days now, I need to prepare.";
                notes[6] = "In those days, in attacks of despair, I tried to stop breathing several times, locking myself in the washroom, shutting off the ventilation, with one foot on the washing machine, the other one on the doorframe. It’s getting empty inside, I’m falling asleep just to awaken on the side of the pavement with the words written on my hand with bioluminescent paint: “Please, give me a ride home, I overdrank”. They use us, they manage our development against the laws of floral dialectics. The oscillatory contour here on the streets is more and more shifted to its green spectrum while above it has ceased to be (?) - perceptible. Less and less I (?) human emotions, residual knowledge now fits into the simplest hormonal-enzyme network, with elements falling out daily according to the law of B-I-N-G-O. Dual systems here in hangars show their insolvency more and more, they upset “him” or already “them”, they decompose, they act out of tune in their eternal attempts to come to conversion, they inevitably take one of the sides, and forever freeze in it. I don’t think that I live here anymore.I don’t think that I live here, and I feel that I cease to understand(?) language constructs. For the first time in my life standing in front of the hole where both parts of my being crumble into, I accepted the inevitability of choice. The flowers I picked this morning smell great.";

                doorPhrase = new string[] { "Not enough flowers for a bouquet." };
            }

        }

        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.L))
            //{
            //    ChangeWorldState();
            //    currentFlowers = 8;
            //    UpdateFlowers();
            //}
            if (isNoteShowing)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isNoteShowing = false;
                    HideNote();
                }
            }

            //if(worldState == 5)
            //{
            //    ChangeSkyTint();
            //}
        }
        public void ChangeWorldState()
        {
            worldState++;
            b.intensity.value += 0.05f;
            foreach (ObjectState obj in changingObjects)
            {

                if (obj != null)
                    obj.NextState();

            }
            if (worldState < environment.Length)
            {
                environment[worldState].SetActive(true);
            }
            if (worldState < skyboxes.Length)
            {
                RenderSettings.skybox = skyboxes[worldState];
            }
            //footsteps counds
            if (worldState == 1)
            {
                player.dynamicFootstep.grassClipSet = grassSet1;
                player.dynamicFootstep.rockAndConcreteClipSet = betonSet1;
            }
            if (worldState == 2)
            {
                player.dynamicFootstep.grassClipSet = grassSet2;
                player.dynamicFootstep.rockAndConcreteClipSet = betonSet2;
            }
            if (worldState == 4)
            {
                player.dynamicFootstep.grassClipSet = grassSet4;
                player.dynamicFootstep.rockAndConcreteClipSet = betonSet4;
            }
            if (worldState == 6)
            {
                foreach (Furniture fur in furniture)
                {

                    if (fur != null)
                        fur.FullArtHouse();

                }
                player.dynamicFootstep.grassClipSet = grassSet6;
                player.dynamicFootstep.rockAndConcreteClipSet = betonSet6;
            }
        }
        public void ShowNote()
        {
            player.ControllerPause();
            noteObject.SetActive(true);
            if (noteNumber < notes.Length)
            {
                if (worldState < notes.Length)
                    noteText.text = notes[worldState];
                noteNumber++;
            }
            note.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            note.transform.DOScale(Vector3.one, 0.5f).OnComplete(() => isNoteShowing = true);
        }
        public void HideNote()
        {
            note.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f).OnComplete(() => noteObject.SetActive(false));
            isNoteShowing = false;
            player.ControllerPause();
            UpdateNotes();
        }

        void LateUpdate()
        {
            if (isDialogue)
            {
                //if(objectToLook!= Vector3.zero)
                //SmoothLook(objectToLook);
                SmoothLookAt(objectToLook);
            }

        }
        //void SmoothLook(Vector3 newDirection)
        //{
        //    Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.LookRotation(newDirection), Time.deltaTime);
        //    //Camera.main.transform.LookAt(newDirection);
        //}
        void SmoothLookAt(Vector3 newDirection)
        {
            //Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.LookRotation(newDirection), Time.deltaTime);
            //Camera.main.transform.rotation = Quaternion.LookRotation(newDirection);
            Camera.main.transform.LookAt(newDirection);
        }

        public void InitiateDialogue(string[] text, Vector3 obj, bool isForcedLook, bool isTakeControl)
        {
            // Debug.Log(obj);
            if (isTakeControl)
            {
                player.ControllerPause();
                controllerOnPause = true;
            }
            dialogue.NewSay(text);
            objectToLook = obj;
            if (isForcedLook)
                isDialogue = true;
            //Debug.Log("initiate");
        }
        public void StopDialogue()
        {
            if (controllerOnPause)
            {
                player.ControllerPause();
                controllerOnPause = false;
            }
            objectToLook = Vector3.zero;
            isDialogue = false;
            if (isSwitchTrack)
            {
                SoundManager.Singleton.SwitchTrack();
                isSwitchTrack = false;
            }
            //Debug.Log("exit");
        }
        //private void ChangeSkyTint()
        //{
        //    float r = Random.Range(0f, 1f);
        //    float g = Random.Range(0f, 1f);
        //    float b = Random.Range(0f, 1f);
        //     var currentColor = new Color(r, g, b);
        //    if (RenderSettings.skybox.HasProperty("_Tint"))
        //        RenderSettings.skybox.SetColor("_Tint", currentColor);
        //    else if (RenderSettings.skybox.HasProperty("_SkyTint"))
        //        RenderSettings.skybox.SetColor("_SkyTint", currentColor);
        //}
        private void UpdateNotes()
        {
            currentNotes++;
            //notesText.text = $"{currentNotes}/{maxNotes}";
            //indication sound on each ending
            if (currentNotes % 2 == 0)
            {
                SoundManager.Singleton.NewEndingSound();
            }
        }
        public void UpdateFlowers()
        {
            currentFlowers++;
            flowersText.text = $"{currentFlowers}/{maxFlowers}";
        }
        public void EnterDoor()
        {
            if (currentFlowers < 5)
            {
                InitiateDialogue(doorPhrase, Vector3.zero, false, false);
            }
            else
            {
                Endings.Singleton.WriteEnding(FigureOutEnding());
                player.ControllerPause();
                transition.DOFade(1f, 0.5f).OnComplete(() => SceneManager.LoadScene("flower_cafe")); // поменять на название сцены
            }
        }

        private int FigureOutEnding()
        {
            var currentEnding = 0;
            if (currentNotes <= 1)
            {
                currentEnding = 1;
            }
            else if (currentNotes > 1 && currentNotes <= 3)
            {
                currentEnding = 2;
            }
            else if (currentNotes > 3 && currentNotes <= 5)
            {
                currentEnding = 3;
            }
            else if (currentNotes > 5)
            {
                currentEnding = 4;
            }
            return currentEnding;
        }

    }
}
