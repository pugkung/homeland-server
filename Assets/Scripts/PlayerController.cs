using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    public float speed = 10f;

    void Update()
    {
        if (photonView.isMine)
            InputMovement();
    }

    void InputMovement()
    {
        if (Input.GetKey(KeyCode.W))
            gameObject.transform.position += Vector3.forward * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            gameObject.transform.position -= Vector3.forward * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            gameObject.transform.position += Vector3.right * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
            gameObject.transform.position -= Vector3.right * speed * Time.deltaTime;
    }
}
