using UnityEngine;
using krai_menu;

namespace krai_menu
{
    public class ChooseIcons : MonoBehaviour
    {
        [SerializeField] private GameObject[] iconsCollection;
        private int previousChoosen = -1;
        void Start()
        {
            foreach (var item in iconsCollection)
            {
                item.SetActive(false);
            }
        }
        public void ChooseIcon(int index, bool isDoubleclick)
        {
            if (previousChoosen == index && isDoubleclick)
            {
                Debug.Log(index);
                //double click functionality here
            }
            else if (previousChoosen == index)
                return;
            previousChoosen = index;
            for (int i = 0; i < iconsCollection.Length; i++)
            {

                if (i == index)
                {
                    iconsCollection[i].SetActive(true);
                    continue;
                }
                iconsCollection[i].SetActive(false);
            }
        }
        public void CancelSelect()
        {
            previousChoosen = -1;
            for (int i = 0; i < iconsCollection.Length; i++)
            {
                iconsCollection[i].SetActive(false);
            }
        }
    }
}
