using UnityEngine;
using System.Collections;

public class DestructionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    public GameObject remains;
    public GameObject destructionEffect;
    public int HP;
	// Update is called once per frame
	void Update () {
        if (HP <= 0) {
            if (destructionEffect != null) {
                GameObject effect = Instantiate(destructionEffect, transform.position, transform.rotation) as GameObject;
                ParticleSystem ps = effect.GetComponent(typeof(ParticleSystem)) as ParticleSystem;
                Destroy(effect, ps.duration);
            }
            if (remains != null) {
                Instantiate(remains, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
