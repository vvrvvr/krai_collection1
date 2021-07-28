using System.Collections;
using UnityEngine;

namespace krai_room
{
    public class WallsMovement : MonoBehaviour
    {
        [SerializeField]
        GameObject roof;

        [SerializeField]
        GameObject[] walls;

        [SerializeField]
        float imageStep;
        private void Start()
        {
            SwitchImages.OnImageHited.AddListener(() => MoveRoof());
        }
        private void MoveRoof()
        {
            StartCoroutine(MoveRoofCoroutine());
        }
        private IEnumerator MoveRoofCoroutine()
        {
            for (float i = 0; i < imageStep; i += 0.001f)
            {
                roof.transform.position = new Vector3(roof.transform.position.x, roof.transform.position.y - 0.001f, roof.transform.position.z);

                yield return new WaitForFixedUpdate();
            }
            for (int i = 0; i < walls.Length; i++)
            {
                for (float j = 0; j < imageStep * 2; j += 0.001f)
                {
                    walls[i].transform.position += walls[i].transform.forward * 0.001f;
                    yield return new WaitForFixedUpdate();
                }
            }

        }
    }
}
