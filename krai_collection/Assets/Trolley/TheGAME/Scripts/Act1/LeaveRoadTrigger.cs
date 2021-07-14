using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveRoadTrigger : MonoBehaviour
{
    private MusicBox musicBox;
    private float sphereRadius;
    public bool isOffroad;
    public bool isInCity = true;
    [SerializeField] LayerMask layerMask;
    Vector3 origin;

    //find distance to closest road point
    private float minSearchingRadius = 2f;
    private float maxSearchingRadius = 22f;
    private float radiusAddition = 0.5f;

    //private float currentHitDistance;


	private void Start()
	{
        musicBox = GameObject.FindGameObjectWithTag("MusicBox").GetComponent<MusicBox>();
        sphereRadius = minSearchingRadius;
	}

    private void Update()
    {
        origin = transform.position;
        if (isOffroad)
        {
            if (isInCity)
            {
                CheckRoadDistance(origin);
                musicBox.OffroadSetParameters(sphereRadius);
            }
            else
            {
                musicBox.OffroadSetParameters(5f);
            }
        }
        
    }
    private void CheckRoadDistance(Vector3 position)
    {
        bool shrink = false;
        bool norm = false;
        if (Physics.CheckSphere(position, sphereRadius, layerMask))
            norm = true;
        if (Physics.CheckSphere(position, sphereRadius - radiusAddition, layerMask))
            shrink = true;

        if (norm)
        {
            if (shrink)
            {
                sphereRadius -= radiusAddition;
                if (sphereRadius <= minSearchingRadius)
                    sphereRadius = minSearchingRadius;
            }
        }
        else
        {
            sphereRadius += radiusAddition;
            if (sphereRadius >= maxSearchingRadius)
                sphereRadius = maxSearchingRadius;
        }

            //Debug.Log(sphereRadius);
        }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Road")
        {
            //musicBox.PlayVoices();
            Debug.Log("Offroad");
            isOffroad = true;
        }
    }

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Road")
        {
            musicBox.OffroadSetParameters(0f);
           // musicBox.StopVoices();
            isOffroad = false;
            sphereRadius = minSearchingRadius;
            

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin , sphereRadius);
    }

}
