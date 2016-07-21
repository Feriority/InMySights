using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int damage;
    void OnCollisionEnter(Collision collision) {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Destructible")) {
            DestructionController controller = other.GetComponent(typeof(DestructionController)) as DestructionController;
            if (controller != null) {
                controller.HP -= damage;
            }
        }
        // TODO: particle
		Destroy (gameObject);
	}
}
