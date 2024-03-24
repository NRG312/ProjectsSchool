using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public Transform transGun;
    public Transform gun;
    public GameObject bulletObj;

    void Update() {
        float vertical = Input.GetAxis("Vertical") * speed;
        float horizontalRot = Input.GetAxis("Horizontal") * rotationSpeed;

        vertical *= Time.deltaTime;
        horizontalRot *= Time.deltaTime;
        
        transform.Translate(0, 0, vertical);
        
        transform.Rotate(0, horizontalRot, 0);

        if (Input.GetKey(KeyCode.T)) {

            transGun.RotateAround(transGun.position, transGun.right, -2.0f);
        } else if (Input.GetKey(KeyCode.G)) {

            transGun.RotateAround(transGun.position, transGun.right, 2.0f);
        } else if (Input.GetKeyDown(KeyCode.B)) {

            Instantiate(bulletObj, gun.position, gun.rotation);
        }
    }
}
