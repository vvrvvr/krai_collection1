using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitTheBit : MonoBehaviour
{
	[SerializeField] private Rigidbody rb;
	[SerializeField] private float power = 2000;
	[SerializeField] private float timer = 22.5f;
	[SerializeField] private float massGoal = 15;

	[Space]
	[SerializeField] private string nextScene;
	[SerializeField] private MainMenu mainMenu;


	private float _deltaMass;
	private KeepDistance[] _keepDistances;

	[Space]
	[SerializeField] private Rigidbody anchor;
	private Coroutine _disdisturb;
	private Coroutine _anchorBit;
	[SerializeField] private float wait1;
	[SerializeField] private float pause1;
	[SerializeField] private float wait2;
	[SerializeField] private float pause2;
	[SerializeField] private float waitForNextScene;

	private void Start()
	{
		_keepDistances = FindObjectsOfType<KeepDistance>();
		_deltaMass = (rb.mass - massGoal) / timer * Time.fixedDeltaTime;
		StartCoroutine(MassDown());
		_disdisturb = StartCoroutine(Disdisturb());
	}

	private IEnumerator MassDown()
	{
		do
		{
			timer -= Time.fixedDeltaTime;
			rb.mass = rb.mass - _deltaMass;

			yield return new WaitForFixedUpdate();
		} while (timer > 0);

		rb.mass = massGoal;

		power *= 10;

		_anchorBit = StartCoroutine(AnchorBit());
		yield return new WaitForSeconds(wait1);

		StopCoroutine(_disdisturb);
		StopCoroutine(_anchorBit);
		rb.freezeRotation = true;
		yield return new WaitForSeconds(pause1);

		_disdisturb = StartCoroutine(Disdisturb());
		_anchorBit = StartCoroutine(AnchorBit());
		rb.freezeRotation = false;
		yield return new WaitForSeconds(wait2);

		StopCoroutine(_disdisturb);
		StopCoroutine(_anchorBit);
		rb.freezeRotation = true;
		yield return new WaitForSeconds(pause2);

		_disdisturb = StartCoroutine(Disdisturb());
		rb.freezeRotation = false;
		yield return new WaitForSeconds(waitForNextScene);

		mainMenu.LoadScene(nextScene);
	}

	private IEnumerator Disdisturb()
	{
		do
		{
			float x = Random.Range(-power, power);
			float y = Random.Range(-power, power);
			float z = Random.Range(-power, power);
			rb.AddForce(new Vector3(x, y, z));

			yield return new WaitForFixedUpdate();

		} while (true);
	}

	private IEnumerator AnchorBit()
	{
		anchor.transform.position = transform.position + new Vector3(0, 5, 0);
		do
		{
			rb.AddForce((anchor.transform.position - transform.position) * power  / 4 );
			yield return new WaitForFixedUpdate();
		} while (true);
	}
}
