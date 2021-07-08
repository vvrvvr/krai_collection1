using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Busstop : MonoBehaviour
{
	[SerializeField] private float _maxPassengers = 6;
	[SerializeField] private float _landSpeed;
	[SerializeField] private GameObject _passengersObject;
	[SerializeField] private Transform _swapPoint;
	[Space]
	[SerializeField] private MusicBox _musicBox;
	[FMODUnity.EventRef] [SerializeField] protected string _scream;


	private Rigidbody _rb;
	private GameObject _passengers;
	private float _currentPassengersCount;
	private bool isOnBusStop;
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Bus")
		{
			isOnBusStop = true;
		}
	}
    private void OnTriggerExit(Collider other)
    {
		if (other.tag == "Bus")
		{
			isOnBusStop = false;
		}
	}

    private void OnTriggerStay(Collider other)
	{
		if ( other.tag == "Bus")
		{
			if (_rb == null)
				_rb = other.GetComponent<Rigidbody>();

			if (_rb.velocity.magnitude < _landSpeed && _passengers != null)
			{
				Destroy(_passengers);
				_currentPassengersCount = 0;
				_musicBox.PlayScream(_scream, 1);
			}
		}
	}

	public void SetPassenger ( GameObject passenger)
	{
		if (!isOnBusStop)
		{
			if (_passengers == null)
				_passengers = Instantiate(_passengersObject, _swapPoint.transform);

			if (_currentPassengersCount < _maxPassengers)
			{
				Instantiate(passenger, _passengers.transform).transform.localPosition =
					new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-1.5f, 0f));

				_currentPassengersCount++;
			}
		}
	}
}
