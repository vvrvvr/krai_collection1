using System.Collections;
using UnityEngine;

namespace krai_room
{
    public class PlayerGrovingUp : MonoBehaviour
    {
        [SerializeField]
        private float grovingStep;
        private void Start()
        {
            SwitchImages.OnImageHited.AddListener(() => GrovingUp());
        }
        private void GrovingUp()
        {
            StartCoroutine(GrovingUpCoroutine());
        }
        private IEnumerator GrovingUpCoroutine()
        {
            for (float i = 0; i < grovingStep; i += 0.001f)
            {
                if (ImagesManager.currentLifeStage != Image.stage.oldness && ImagesManager.currentLifeStage != Image.stage.adulthood)
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.001f, transform.localScale.z);
                    yield return new WaitForFixedUpdate();
                }
                else if (ImagesManager.currentLifeStage == Image.stage.oldness)
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.001f, transform.localScale.z);
                    yield return new WaitForFixedUpdate();
                }
            }

        }
    }
}
