using UnityEngine;
using System.Collections;

public class DestructionController : MonoBehaviour {

    public GameObject remains;
    public GameObject destructionEffect;
    public int maxHP;
    [HideInInspector] public int HP;
    public bool respawn = false;
    private Vector3 respawnPosition;
    [HideInInspector] public bool isDead = false;

	// Use this for initialization
	void Start () {
        HP = maxHP;
        respawnPosition = transform.position;
	}

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
            if (respawn) {
                if (isDead)
                    Respawn ();
                else
                    isDead = true;
            } else {
                Destroy(gameObject);
            }
        }
    }

    private void Respawn() {
        gameObject.SetActive (false);
        transform.position = respawnPosition;
        HP = maxHP;
		isDead = false;
        gameObject.SetActive (true);
    }
}
