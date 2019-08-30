using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletScript : MonoBehaviourPun { 
    public float speed = 10f;
    public float destroyTime = 2f;

    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(destroyTime);
        this.GetComponent<PhotonView>().RPC("destroy", RpcTarget.AllBuffered);
    }
    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    [PunRPC]
    public void destroy()
    {
        Destroy(this.gameObject);
    }
}
