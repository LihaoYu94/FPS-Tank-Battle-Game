using UnityEngine;
using UnityEngine.UI;

public class TankFire : MonoBehaviour
{
    public float        launchForce = 10.0f;
    public Rigidbody    shell;
    public Transform    origin;
    public string       fireButton;

    private Rigidbody   rbody;

    private void Awake() {
        rbody = GetComponent<Rigidbody>();
    }

    private void Fire() {
        Rigidbody shellInstance = Instantiate(shell, origin.position, origin.rotation) as Rigidbody;
//        shellInstance.velocity  = origin.forward * launchForce + rbody.velocity;
		shellInstance.velocity = origin.forward * 20;
    }

    private void Update() {
        if (Input.GetButtonUp(fireButton)) {
            Fire();
        }
    }
}