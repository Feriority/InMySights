using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : FirstPersonController {

	public GameObject projectile;
	private int projectileSpawnDistance = 2;
	private float maxAngle = 40;

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();

		if(CrossPlatformInputManager.GetButtonDown ("Fire1")) {
			Transform cameraTransform = m_Camera.transform;
			Vector3 spawnPoint = cameraTransform.position + (cameraTransform.forward * projectileSpawnDistance);

			GameObject newObject = (GameObject) Instantiate (
				projectile,
				spawnPoint,
				cameraTransform.rotation
			);

			Projectile newProjectile = newObject.GetComponent<Projectile> ();
			newProjectile.velocity = cameraTransform.forward * newProjectile.maxSpeed;
			newProjectile.target = FindNearestTarget (spawnPoint, cameraTransform.forward);
		}
	}

	private GameObject FindNearestTarget(Vector3 fromPoint, Vector3 fromDirection) {
		float minDistance = float.MaxValue;
		float minAngle = float.MaxValue;
		GameObject nearestTarget = null;

		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Target");
		foreach (GameObject obj in objs) {
			Vector3 ray = obj.transform.position -  fromPoint;
			float distance = Vector3.Magnitude (ray);
			float angle = Vector3.Angle (fromDirection, ray);
			if (angle > maxAngle) { continue; }

			if (distance < minDistance || (distance == minDistance && angle < minAngle)) {
				minDistance = distance;
				minAngle = angle;
				nearestTarget = obj;
			} 
		}

		return nearestTarget;
	}
}
