using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour {

    public GameObject bullet;
    public GameObject turret;
    public GameObject enemy;
    public Transform turretBase;

    #region Propetries

    private float _speed = 15.0f;
    private float _rotSpeed = 5.0f;
    private float _moveSpeed = 1.0f;

    private static float _delayReset = 0.8f;
    private float _delay
    {
        get => _delayReset;
        set => _delayReset = value;
    }

    #endregion


    void CreateBullet() {

        GameObject shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = _speed * turretBase.forward;
    }

    float? RotateTurret() {

        float? angle = CalculateAngle(false);

        if (angle != null) {

            turretBase.localEulerAngles = new Vector3(360.0f - (float)angle, 0.0f, 0.0f);
        }
        return angle;
    }

    float? CalculateAngle(bool low) {

        Vector3 targetDir = enemy.transform.position - transform.position;
        float y = targetDir.y;
        targetDir.y = 0.0f;
        float x = targetDir.magnitude - 1.0f;
        float gravity = 9.8f;
        float sSqr = _speed * _speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0.0f) {

            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low) {
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            }
            else
            {
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
            }
        } else
            return null;
    }

    void Update() {

        _delay -= Time.deltaTime;
        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotSpeed);
        float? angle = RotateTurret();

        if (angle != null && _delay <= 0.0f) {

            CreateBullet();
            _delay = _delayReset;
        } else {

            transform.Translate(0.0f, 0.0f, Time.deltaTime * _moveSpeed);
        }
    }
}
