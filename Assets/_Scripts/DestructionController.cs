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
        // TODO: Proper damage mechanics
        if(Input.GetMouseButtonDown(0)) {
            //HP -= 1;
        }
        if (HP == 0) {
            if (destructionEffect != null) {
                Instantiate(destructionEffect, transform.position, transform.rotation);
            }
            if (remains != null) {
                Instantiate(remains, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
