using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;
using TMPro;

namespace krai_room
{
    public class ImagesManager : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> stageObjects;
        private int countOfImagesInCurrentStage;

        [SerializeField]
        private PlayableDirector endingCatScene;

        private int stageNumber;
        private int currentImageNumber;

        private GameObject[] currentImages;

        [SerializeField]
        private Animator[] lightAnimator;

        [HideInInspector]
        public static Image.stage currentLifeStage;

        public static UnityEvent OnNextStage = new UnityEvent();


        void Start()
        {
            stageNumber = 0;
            currentImageNumber = 0;
            LounchStage(stageNumber);
            SwitchImages.OnImageHited.AddListener(() => ImageClicked());
            SwitchImages.OnImageClicked.AddListener((image) => PlayImageSound(image));
        }

        private void PlayImageSound(GameObject image)
        {
            image.GetComponentInParent<Image>().PlayAudio();
        }
        private void ImageClicked()
        {

            currentImageNumber++;
            if (currentImageNumber < currentImages.Length)
            {
                currentImages[currentImageNumber].SetActive(true);
            }
            if (currentImageNumber >= countOfImagesInCurrentStage)
            {

                if (stageNumber < stageObjects.Count - 1)
                {
                    OnNextStage.Invoke();
                    stageNumber += 1;
                    LounchStage(stageNumber);
                    CloseLastStage();
                }
                else
                {
                    endingCatScene.Play();
                }
            }
        }
        private void LounchStage(int stageNumber)
        {
            currentLifeStage = (Image.stage)stageNumber;
            stageObjects[stageNumber].SetActive(true);
            Stage currentStage = stageObjects[stageNumber].GetComponentInChildren<Stage>();
            currentStage.WindowText.SetActive(true);

            currentImages = currentStage.StageImages;
            countOfImagesInCurrentStage = currentImages.Length;
            currentImageNumber = 0;
        }

        private void CloseLastStage()
        {
            for (int i = 0; i < lightAnimator.Length; i++)
            {
                lightAnimator[i].Play("Animation");
            }
            Stage lastStage = stageObjects[stageNumber - 1].GetComponentInChildren<Stage>();
            for (int i = 0; i < stageNumber; i++)
            {
                Stage currentStage = stageObjects[i].GetComponentInChildren<Stage>();
                currentStage.currentAlpha += 0.25f;
                for (int j = 0; j < currentStage.StageImages.Length; j++)
                {
                    TMP_Text currentText = currentStage.StageImages[j].GetComponentInChildren<TMP_Text>();
                    currentText.color = new Color(currentText.color.r, currentText.color.g, currentText.color.b, currentText.color.a - currentStage.currentAlpha);
                }
            }

            lastStage.WindowText.SetActive(false);
            StartCoroutine(ShowCloseText(lastStage.CloseStageText));
        }

        private IEnumerator ShowCloseText(GameObject gameObject)
        {
            yield return new WaitForSeconds(3);
            //stageObjects[stageNumber - 1].SetActive(false);
            gameObject.SetActive(true);
            SpriteRenderer text = gameObject.GetComponent<SpriteRenderer>();
            text.color = Color.clear;
            for (float i = 0; i <= 1; i += 0.1f)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, i);
                yield return null;
            }
        }


    }
}
