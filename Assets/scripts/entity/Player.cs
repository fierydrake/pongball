using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Player controlled paddle
/// </summary>

public class Player : PaddleBase {

	// Use this for initialization
	public override void Start()
    {
		base.Start();
		
		// Give the player faster movement
		SetThrust(20);
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();

        float haxis = Input.GetAxis("Horizontal");
        float vaxis = Input.GetAxis("Vertical");

        if (haxis != 0)
        {

        }

        if (vaxis != 0)
        {
            MovePaddles(vaxis);
        }

        // If Fire1 is pressed, trigger pull animation
        if (Input.GetButton("Fire1"))
        {
            animator.SetBool("pull", true);
            animator.SetBool("hit", true);
        }
        else
        {
            animator.SetBool("pull", false);
            animator.SetBool("hit", false);
        }
    }

    private new void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) &&  (myPowers.Count > 0 || currentPowerName!=""))
        {
            TryActivate();
        }
    }

    new public void TryActivate()
    {
        if (!NetworkManager.singleton.isNetworkActive || NetworkServer.connections.Count > 0)
        {
            myPowers[myPowers.Count - 1].Activate();
        }
        else
        {
            PaddleNetworking pn = gameObject.GetComponent<PaddleNetworking>();
            pn.CmdActivatePower();
            currentPowerName = "";
        }
    }
}
