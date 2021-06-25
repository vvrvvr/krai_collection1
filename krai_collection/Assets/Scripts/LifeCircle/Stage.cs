using UnityEngine;

namespace krai_room
{
    public class Stage : MonoBehaviour
    {
        [HideInInspector]
        public int ImagesCount;
        public GameObject CloseStageText;
        public GameObject[] StageImages;

        public GameObject WindowText;
        public float currentAlpha;

        void Start()
        {
            ImagesCount = StageImages.Length;
        }
    }
}
