using UnityEngine;
using krai_cvetok;

namespace krai_cvetok
{
    public class WaypointsMoving : MonoBehaviour
    {
        [SerializeField] private Transform[] wayPointsArray;
        [SerializeField] private Transform objectToMove;
        public float Speed;
        public bool Forward = true;
        private int targetIndex = 1;

        //private void OnEnable()
        //{
        //    wayPointsArray = GetComponentsInChildren<Transform>().ToArray(); // index 0 in array is self
        //}

        private void Update()
        {
            objectToMove.position = Vector3.MoveTowards(objectToMove.position, wayPointsArray[targetIndex].position, Time.deltaTime * Speed);
            if (Vector3.Distance(objectToMove.position, wayPointsArray[targetIndex].position) <= 0.01)
            {
                if (Forward)
                    targetIndex++;
                else
                    targetIndex--;
            }
            if (targetIndex >= wayPointsArray.Length)
                targetIndex = 0;
            if (targetIndex < 0)
                targetIndex = wayPointsArray.Length - 1;
        }

    }
}
