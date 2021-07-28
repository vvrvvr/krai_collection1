using UnityEngine;
using krai_cvetok;

namespace krai_cvetok
{
    public class LookAt : MonoBehaviour
    {
        void LateUpdate()
        {
            if (Camera.main != null)
                transform.LookAt(Camera.main.transform.position);
        }
    }
}
