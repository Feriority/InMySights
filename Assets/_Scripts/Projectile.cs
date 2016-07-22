﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	[HideInInspector] public GameObject target = null;
	public float maxSpeed;
	[HideInInspector] public Vector3 velocity;
    public int damage;
    public GameObject impactFX;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos;
		if (target == null) {
            newPos = transform.position + velocity * Time.deltaTime;
		} else {
			float time = Vector3.Distance(target.transform.position, transform.position) / maxSpeed;
            newPos = Vector3.SmoothDamp (transform.position, target.transform.position, ref velocity, time, maxSpeed);
		}
        GetComponent<Rigidbody> ().MovePosition (newPos);
	}
        
    void OnCollisionEnter(Collision collision) {
        GameObject other = collision.gameObject;
        DestructionController controller = other.GetComponent(typeof(DestructionController)) as DestructionController;
        if (controller != null) {
            controller.HP -= damage;
        }
        if (impactFX != null) {
            // TODO: Clean up explosions
            Instantiate(impactFX, transform.position, transform.rotation);
        }
		Destroy (gameObject);
	}
}
