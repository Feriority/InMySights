using UnityEngine;
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
        if (target != null) {
            DestructionController dc = target.GetComponent<DestructionController> ();
            if (dc != null && dc.isDead) {
                target = null;
            }
        }

        Vector3 newPos;
		if (target == null) {
            newPos = transform.position + velocity * Time.deltaTime;
		} else {
			float time = Vector3.Distance(target.transform.position, transform.position) / maxSpeed;
            newPos = Vector3.SmoothDamp (transform.position, target.transform.position, ref velocity, time, maxSpeed);

            Quaternion newHeading = Quaternion.LookRotation(velocity) * Quaternion.Euler(0,90,90);

            GetComponent<Rigidbody>().MoveRotation(newHeading);
		}
        GetComponent<Rigidbody> ().velocity = (newPos - transform.position) / Time.deltaTime;
	}
        
    void OnCollisionEnter(Collision collision) {
        GameObject other = collision.gameObject;
        DestructionController controller = other.GetComponent(typeof(DestructionController)) as DestructionController;
        if (controller != null) {
            controller.HP -= damage;
        }
        if (impactFX != null) {
            GameObject effect = Instantiate(impactFX, transform.position, transform.rotation) as GameObject;
            ParticleSystem ps = effect.GetComponent(typeof(ParticleSystem)) as ParticleSystem;
            Destroy(effect, ps.duration);
        }
		Destroy (gameObject);
	}
}
