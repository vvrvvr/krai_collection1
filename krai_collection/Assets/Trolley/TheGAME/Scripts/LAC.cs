using UnityEngine;

public class LAC : MonoBehaviour
{
    private Transform mainCamera;
    private float saveX;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        saveX = transform.eulerAngles.x;
        transform.LookAt(mainCamera);
        transform.eulerAngles = new Vector3(saveX, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
