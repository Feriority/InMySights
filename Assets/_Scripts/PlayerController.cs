using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : FirstPersonController {

	public GameObject projectile;
	private int projectileSpawnDistance = 2;
	private int projectileVelocity = 10;

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();

		if(CrossPlatformInputManager.GetButtonDown ("Fire1")) {
			Transform cameraTransform = m_Camera.transform;
			GameObject newProjectile = (GameObject) Instantiate (
				projectile,
				cameraTransform.position + (cameraTransform.forward * projectileSpawnDistance),
				cameraTransform.rotation
			);
			Rigidbody rbody = newProjectile.GetComponent<Rigidbody> ();
			rbody.velocity = cameraTransform.forward * projectileVelocity;
		}
	}
}
