using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	[HideInInspector] public GameObject target = null;
	public float maxSpeed;
	[HideInInspector] public Vector3 velocity;
    public int damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			transform.position = transform.position + velocity * Time.deltaTime;
		} else {
			float time = Vector3.Distance(target.transform.position, transform.position) / maxSpeed;
			transform.position = Vector3.SmoothDamp (transform.position, target.transform.position, ref velocity, time, maxSpeed);
		}
	}
        
    void OnCollisionEnter(Collision collision) {
        GameObject other = collision.gameObject;
        // TODO: particle
        Destroy (gameObject);
        DestructionController controller = other.GetComponent(typeof(DestructionController)) as DestructionController;
        if (controller != null) {
            controller.HP -= damage;
        }
	}
}
