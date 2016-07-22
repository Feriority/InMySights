using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : FirstPersonController {

    [SerializeField] private GameObject projectile;
    private float projectileSpawnDistance = 1.75f;
    private float projectileSpawnOffsetX = 1.1f;
    [SerializeField] private float maxAngle;
    [SerializeField] private float secondsPerShot = 0.5f;
    private float timeSinceLastShot = 0;

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();

        if (timeSinceLastShot <= secondsPerShot) {
            timeSinceLastShot += Time.deltaTime;
        } else if (m_IsWalking && CrossPlatformInputManager.GetButton ("Fire1_P" + playerNumber)) {
            timeSinceLastShot = 0;

			Transform cameraTransform = m_Camera.transform;
			Vector3 spawnPoint = cameraTransform.position + (cameraTransform.forward * projectileSpawnDistance);
            spawnPoint += cameraTransform.right * projectileSpawnOffsetX;

			GameObject newObject = (GameObject) Instantiate (
				projectile,
				spawnPoint,
                cameraTransform.rotation * Quaternion.Euler(0,90,90)
			);

			Projectile newProjectile = newObject.GetComponent<Projectile> ();
			newProjectile.velocity = cameraTransform.forward * newProjectile.maxSpeed;
			newProjectile.target = FindNearestTarget (spawnPoint, cameraTransform.forward);
            AudioSource.PlayClipAtPoint(newProjectile.fireSound, new Vector3(0,0,0));
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

            RaycastHit hit;
            bool sightBlocked = Physics.Raycast (m_Camera.transform.position, ray, out hit, distance);
            if (sightBlocked) { continue; }

			if (distance < minDistance || (distance == minDistance && angle < minAngle)) {
				minDistance = distance;
				minAngle = angle;
				nearestTarget = obj;
			} 
		}

		return nearestTarget;
	}
}
