using UnityEngine;
using System.Collections;

public class DestructionController : MonoBehaviour {

    public GameObject remains;
    public GameObject destructionEffect;

    [HideInInspector] public AudioSource source;
    public AudioClip damageSound;
    public AudioClip destructionSound;

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

    void Awake () {
        source = GetComponent<AudioSource>();
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
            if (destructionSound != null) {
                AudioSource.PlayClipAtPoint(destructionSound, new Vector3(0,0,0));
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
        transform.position = respawnPosition;
        HP = maxHP;
    }

    public void playHit() {
        if (damageSound != null) {
            AudioSource.PlayClipAtPoint(damageSound, new Vector3(0,0,0), 0.7f);
        }
    }
}
