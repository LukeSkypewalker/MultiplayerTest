using UnityEngine;
using System.Collections;
using System.Net;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public float moveSpeed = 10f;
    public float turnSpeed = 150f;

    public GameObject bulletPrefub;
    public Transform bulletSpawn; 

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (!isLocalPlayer) return;
        
        float x = Input.GetAxis("Horizontal")*Time.deltaTime * turnSpeed;
        float z = Input.GetAxis("Vertical")* Time.deltaTime * moveSpeed;

	    transform.Rotate(0, x, 0);
	    transform.Translate(0, 0, z);

	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        CmdFire();
	    }

    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefub, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward*6f;
        NetworkServer.Spawn(bullet);
        Destroy(bullet,2); 
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
