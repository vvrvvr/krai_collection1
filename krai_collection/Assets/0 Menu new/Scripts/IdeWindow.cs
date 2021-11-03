using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using krai_menu;

namespace krai_menu
{

    public class IdeWindow : MonoBehaviour
    {
        [SerializeField] private Text screenText;
        [SerializeField] private Scrollbar scrollbar;
        [SerializeField] private MenuMusic music;
        private bool isScrollbarLocked;
        private bool isTimerActive = true;
        private float waitingTime = 0;
        private int count = 0;
        public bool IsPause;
        private Tween coding;

        private string[] code = new string[]{
        "<color=lightblue>void Start()</color>\n    {\n        pointerMainTransform = pointerMain.<color=#dcc976>GetComponent</color><color=teal>Transform</color>();\n        pointerSecondaryTransform = pointerSecondary.<color=#dcc976>GetComponent</color><color=teal>ransform</color>();\n    }\n\n    <color=lightblue>void Update()</color>\n    {\n        <color=magenta>if</color> (isCursorVisible)\n        {\n            worldPosition = <color=teal>Camera</color>.main.<color=#dcc976>ScreenToWorldPoint</color>(<color=teal>Input</color>.mousePosition); <color=green>//позиция мыши в мире</color>\n            worldPosition.z = transform.position.z;\n            <color=magenta>if</color> (isCursorVisible)\n                pointerMainTransform.position = worldPosition;\n        }\n    }\n    <color=lightblue>private void OnMouseOver</color>()\n    {\n        isCursorVisible = true;\n        <color=green>Vector3</color> ",
      "<color=lightblue>dirLeft</color> = worldPosition - anchMain.position;\n        <color=lightblue>dirLeft</color> = <color=lightblue>dirLeft</color> * PointerScale;\n <color=green>//масштаб</color>\n        <color=lightblue>dirLeft</color> = <color=magenta>Quaternion</color>.<color=#dcc976>AngleAxis</color>(rotX, <color=green>Vector3</color>.forward) * <color=lightblue>dirLeft</color>; <color=green>//угол</color>\n        pointerSecondaryTransform.position = ancSecondary.position + <color=lightblue>dirLeft</color>;\n\n    }\n    <color=lightblue>private void</color> <color=#dcc976>OnMouseEnter</color>()\n    {\n        ",
       "pointerMain.<color=#dcc976>SetActive</color>(<color=lightblue>true</color>);\n        pointerSecondary.<color=#dcc976>SetActive</color>(<color=lightblue>true</color>);\n        <color=green>Cursor</color>.visible = <color=lightblue>false</color>;\n    }\n    <color=lightblue>private void OnMouseExit</color>()\n    {\n        isCursorVisible = <color=lightblue>false</color>;\n        pointerMain.<color=#dcc976>SetActive</color>(<color=lightblue>false</color>);\n        pointerSecondary.<color=#dcc976>SetActive</color>(<color=lightblue>false</color>);\n        <color=teal>Cursor</color>.visible = <color=lightblue>true</color>;\n    }\n",
        "[<color=teal>SerializeField</color>] <color=#509cce>private</color> <color=teal>GameObject</color>[] iconsCollection;\n    <color=#509cce>private int</color> previousChoosen = -1;\n    <color=#509cce>void Start</color>()\n    {\n        <color=magenta>foreach</color> (<color=#509cce>var</color> <color=teal>item</color> <color=magenta>in</color> iconsCollection)\n        {\n            <color=teal>item</color>.<color=#dcc976>SetActive</color>(<color=#509cce>false</color>);\n        }\n    }\n    <color=#509cce>public void</color> <color=#dcc976>ChooseIcon</color>(<color=#509cce>int</color> <color=teal>index</color>, <color=#509cce>bool</color> <color=lightblue>isDoubleclick</color>)\n    {\n        <color=magenta>if</color> (previousChoosen == <color=teal>index</color> && <color=teal>isDoubleclick</color>)\n        {\n            ",
         "<color=teal>Debug</color>.<color=#dcc976>Log</color>(<color=teal>index</color>);\n            <color=green>//double click functionality here</color>\n        }\n        <color=magenta>else if</color> (previousChoosen == <color=teal>index</color>)\n            <color=magenta>return</color>;\n        previousChoosen = <color=teal>index</color>;\n        <color=magenta>for</color> (<color=#509cce>int</color> <color=lightblue>i</color> = 0; i  iconsCollection.Length; <color=lightblue>i</color>++)\n        {\n\n            <color=magenta>if</color> (<color=lightblue>i</color> == <color=lightblue>index</color>)\n            {\n                ",
           "iconsCollection[<color=lightblue>i</color>].<color=#dcc976>SetActive</color>(<color=#509cce>true</color>);\n                <color=magenta>continue</color>;\n            }\n            iconsCollection[<color=lightblue>i</color>].<color=#dcc976>SetActive</color>(<color=#509cce>false</color>);\n        }\n    }\n    <color=#509cce>public void</color> <color=#dcc976>CancelSelect</color>()\n    {\n        previousChoosen = -1;\n        <color=magenta>for</color> (<color=#509cce>int</color> <color=lightblue>i</color> = 0; i  iconsCollection.Length; <color=lightblue>i</color>++)\n        {\n            iconsCollection[<color=lightblue>i</color>].<color=#dcc976>SetActive</color>(<color=#509cce>false</color>);\n        }\n    }\n"

    };
        void Start()
        {
            scrollbar.value = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (isScrollbarLocked)
                scrollbar.value = 0;
            if (isTimerActive && !IsPause)
            {
                waitingTime -= Time.deltaTime;
                if (waitingTime < 0)
                {
                    count++;
                    if (count > 3)
                    {
                        screenText.text = "";
                        count = 0;
                    }
                    PrintCode();
                    isTimerActive = false;
                }
            }

        }

        private void PrintCode()
        {
            music.PlayTypingSound(true);
            isScrollbarLocked = true;
            var i = Random.Range(0, code.Length);
            //Debug.Log(i);
            coding = screenText.DOText(code[i], 35f, true).SetRelative().SetEase(Ease.Linear).OnComplete(CompleteCode);
            scrollbar.value = 0;
        }
        private void CompleteCode()
        {
            music.PlayTypingSound(false);
            isScrollbarLocked = false;
            isTimerActive = true;
            waitingTime = Random.Range(8, 15);
        }

        public void PauseCoding()
        {
            coding.Kill();
            IsPause = true;
        }
        public void PlayCoding()
        {
            IsPause = false;
            isScrollbarLocked = false;
            isTimerActive = true;
            waitingTime = 1f;
        }
    }
}
