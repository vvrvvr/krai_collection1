using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using krai_menu;

namespace krai_menu
{

    public class CustomCursor : MonoBehaviour
    {
        [SerializeField] private ChooseIcons chooseIcons;
        private Transform tr;
        public LayerMask iconLayer;

        //дабл клик
        private float doubleClickTime = 0f;
        private float DoubleClickSpeed = 0.5f;

        private void Start()
        {
            tr = GetComponent<Transform>();
        }

        private void Update()
        {
            if (doubleClickTime < 1f)
                doubleClickTime += Time.deltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                //Double click functionality
                if (doubleClickTime <= DoubleClickSpeed)
                {
                    //Debug.Log("double click");
                    CheckMousePressed(true);
                }
                doubleClickTime = 0f;

                CheckMousePressed(false);
            }

        }

        private void CheckMousePressed(bool isDoubleClick)
        {
            RaycastHit rayHit;
            if (Physics.Raycast(tr.position, new Vector3(0, 0, 1), out rayHit, Mathf.Infinity, iconLayer))
            {
                chooseIcons.ChooseIcon(rayHit.collider.GetComponent<Button>().buttonIndex, isDoubleClick);
                //Debug.Log(rayHit.collider.GetComponent<Button>().buttonIndex);
            }
            else
            {
                chooseIcons.CancelSelect();
            }

        }

    }
}
