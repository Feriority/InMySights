using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DestructionController : NetworkBehaviour {

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
		if (!isServer)
			return;
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
				if (isDead) {
					HP = maxHP;
					isDead = false;
					RpcRespawn ();
				} else
                    isDead = true;
            } else {
                Destroy(gameObject);
            }
        }
    }

	[ClientRpc]
    void RpcRespawn() {
		if (isLocalPlayer) {
			transform.position = respawnPosition;
		}
	}

}
