using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using krai_menu;

namespace krai_menu
{

    public class PersonAbberation : MonoBehaviour
    {
        public float radius = 1.0f;
        public float speed = 5.0f;
        public float timeLeft = 1;
        private Color targetColor;
        private SpriteRenderer sprt;
        public bool isChangeColor;

        // The point we are going around in circles
        private Vector2 basestartpoint;
        // Destination of our current move
        private Vector2 destination;
        // Start of our current move
        private Vector2 start;
        // Current move's progress
        private float progress = 0.0f;

        // Use this for initialization
        void Start()
        {
            start = transform.localPosition;
            basestartpoint = transform.localPosition;
            progress = 0.0f;
            sprt = GetComponent<SpriteRenderer>();
            PickNewRandomDestination();
        }

        void Update()
        {
            if (isChangeColor)
            {
                if (timeLeft <= Time.deltaTime)
                {
                    // transition complete
                    // assign the target color
                    sprt.color = targetColor;

                    // start a new transition
                    targetColor = new Color(Random.value, Random.value, Random.value);
                    timeLeft = 1.0f;
                }
                else
                {
                    // transition in progress
                    // calculate interpolated color
                    sprt.color = Color.Lerp(sprt.color, targetColor, Time.deltaTime / timeLeft);

                    // update the timer
                    timeLeft -= Time.deltaTime;
                }
            }



            bool reached = false;
            // Update our progress to our destination
            progress += speed * Time.deltaTime;
            // Check for the case when we overshoot or reach our destination
            if (progress >= 1.0f)
            {
                progress = 1.0f;
                reached = true;
            }
            // Update out position based on our start postion, destination and progress.
            transform.localPosition = (destination * progress) + start * (1 - progress);
            // If we have reached the destination, set it as the new start and pick a new random point. Reset the progress
            if (reached)
            {
                start = destination;
                PickNewRandomDestination();
                progress = 0.0f;
            }
        }

        void PickNewRandomDestination()
        {
            // We add basestartpoint to the mix so that is doesn't go around a circle in the middle of the scene.
            destination = Random.insideUnitCircle * radius + basestartpoint;
        }
    }
}
