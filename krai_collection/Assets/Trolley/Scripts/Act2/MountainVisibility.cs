
using UnityEngine;

public class MountainVisibility : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float playerYMin = 4f;
    private float playerYMax = 33f;
    private float mountaingYMax;
    private float mountaingYMin = 78f; //если поменяется террейн - отредактировать
    private Transform mountainTransform;
    private float scale;

    private void Start()
    {
        mountainTransform = GetComponent<Transform>();
        mountaingYMax = mountainTransform.position.y;
        scale = (mountaingYMax - mountaingYMin) / (playerYMax - playerYMin);
    }

    void Update()
    {
        if(player != null)
        {
            if(player.position.y > playerYMin && player.position.y < playerYMax)
            {
                var newY = mountaingYMax - scale * (player.position.y - playerYMin);
                mountainTransform.position = new Vector3(mountainTransform.position.x, newY, mountainTransform.position.z);
            }
        }
    }
}
