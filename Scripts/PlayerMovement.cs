using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayerMovement : MonoBehaviourPun,IPunObservable
{
    public int health=100;

    public PhotonView pv;

    Rigidbody rb;

    private Vector3 smoothMove;

    private GameObject sceneCamera;
    public GameObject playerCamera;

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        if (photonView.IsMine)
        {
            sceneCamera = GameObject.Find("Main Camera");
            sceneCamera.SetActive(false);
            playerCamera.SetActive(true);
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            ProcessInputs();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            smoothMovement();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

    }

    private void smoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 10);
    }

    private void ProcessInputs()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-3 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(3 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, 3 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -3 * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, bulletSpawn.transform.position, Quaternion.identity);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if (stream.IsReading)
        {
            smoothMove = (Vector3)stream.ReceiveNext();
        }
    }
    public void takeDamage(int damage)
    {
        health -= damage;
    }

}
