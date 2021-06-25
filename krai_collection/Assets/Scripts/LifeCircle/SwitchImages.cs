using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace krai_room
{
    public class SwitchImages : MonoBehaviour
    {

        private Transform transform;


        public static UnityEvent OnImageHited = new UnityEvent();

        public static ImageClicked OnImageClicked = new ImageClicked();


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = UnityEngine.Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Image"))
                    {
                        SwitchImageToText(hit.collider.gameObject);
                        OnImageClicked.Invoke(hit.collider.gameObject);
                        OnImageHited.Invoke();
                    }
                }
            }
        }


        private void SwitchImageToText(GameObject imageObject)
        {
            Image currentImage = imageObject.GetComponentInParent<Image>();
            currentImage.Sprite.SetActive(false);
            currentImage.Text.SetActive(true);
            StartCoroutine(LatterAnimation(0.1f, currentImage.Text.GetComponent<TMP_Text>()));
        }

        private IEnumerator LatterAnimation(float delay, TMP_Text text)
        {
            char[] chars = text.text.ToCharArray();
            text.text = "";
            for (int i = 0; i < chars.Length; i++)
            {
                text.text += chars[i].ToString();
                yield return new WaitForSeconds(delay);
            }
        }
    }

    public class ImageClicked : UnityEvent<GameObject>
    {

    }
}
