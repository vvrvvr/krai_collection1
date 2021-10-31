using UnityEngine;
using krai_menu;

namespace krai_menu
{

    public class TitlesControl : MonoBehaviour
    {
        [SerializeField] private GameObject eyeCursor;
        [SerializeField] private MenuManager menuManager;
        private Transform eyeCursorTransform;
        private bool isCursorVisible;
        private Vector3 worldPosition = Vector3.zero;
        void Start()
        {
            eyeCursorTransform = eyeCursor.GetComponent<Transform>();
        }


        void Update()
        {
            if (isCursorVisible)
            {
                worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //позиция мыши в мире
                worldPosition.z = transform.position.z;
                eyeCursorTransform.position = worldPosition;
                if(Input.GetMouseButtonDown(0))
                {
                    menuManager.ShowTitles();
                }
            }
        }
        private void OnMouseOver()
        {
            isCursorVisible = true;
        }
        private void OnMouseEnter()
        {
            isCursorVisible = true;
            Cursor.visible = false;
            eyeCursor.SetActive(true);
        }
        private void OnMouseExit()

        {
            isCursorVisible = false;
            Cursor.visible = true;
            eyeCursor.SetActive(false);
        }

    }
}
