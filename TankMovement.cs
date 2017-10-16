using System;
using UnityEngine;

[System.Serializable]
public class TankMovement : MonoBehaviour {
    public int          ID;
    public float        moveSpeed;
    public float        turnSpeed;
    public string       moveAxisName;
    public string       turnAxisName;

    private Rigidbody   rbody;
    private float       moveInputValue;
    private float       turnInputValue;

    private void Awake() {
        rbody = GetComponent<Rigidbody>();
    }

    private void OnEnable() {
        rbody.isKinematic = false;
        moveInputValue = 0.0f;
        turnInputValue = 0.0f;
    }

    private void OnDisable() {
        rbody.isKinematic = true;
    }

    private void Update() {
        moveInputValue  = Input.GetAxis (moveAxisName);
        turnInputValue  = Input.GetAxis (turnAxisName);
    }

    private void FixedUpdate() {
        Move();
        Turn();
    }

    private void Move() {
        Vector3 movement = transform.forward * moveInputValue * moveSpeed;
        rbody.AddForce(movement, ForceMode.VelocityChange);
    }


    private void Turn() {
        float       turn            = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion  turnRotation    = Quaternion.AngleAxis(turn, transform.up);
        rbody.MoveRotation(rbody.rotation * turnRotation);
    }
}