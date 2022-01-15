using UnityEngine;

namespace krai_trol
{
    public class ExitSceneTimer : MonoBehaviour
    {
        [SerializeField] protected MainMenu _mainMenu;
        [SerializeField] protected string _act;
        [SerializeField] private float onSceneSeconds;
        private float currentTime = 0;
        private bool isSwitchToNewScene = true;

        void Update()
        {
            currentTime += Time.deltaTime;
            if((currentTime >= onSceneSeconds) && isSwitchToNewScene)
            {
                isSwitchToNewScene = false;
                _mainMenu.LoadScene(_act);
            }
        }
    }
}
