#pragma strict

function Start () {

}

var remains: GameObject;
var destructionEffect: GameObject;
var HP: int;
function Update () {
    // TODO: Proper damage mechanics
    if(Input.GetMouseButtonDown(0)) {
        HP -= 1;
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