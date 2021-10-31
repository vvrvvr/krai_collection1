using UnityEngine;
using UnityEngine.UI;
using krai_menu;

namespace krai_menu
{
    public class Popit : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spr;
        [SerializeField] CanvasGroup canvGroup;
        [SerializeField] private Text messageText;
        [SerializeField] private Image img;


        void Update()
        {
            canvGroup.alpha = spr.color.a;
        }

        public void SetpopitGuts(string newText, Sprite spr)
        {
            messageText.text = newText;
            img.sprite = spr;
        }
       
    }
}
