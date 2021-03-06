﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour {

    public Transform player;
    public Transform reciever;
    public Renderer recieverPlane;
    public Renderer forwardRenderPlane;

    private bool playerIsOverlapping = false;

	// Update is called once per frame
	void Update () {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0)
            {
                float RotationDiff = Quaternion.Angle(transform.rotation, reciever.rotation);

                RotationDiff += 180;
                player.Rotate(Vector3.up, RotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, RotationDiff, 0f) * portalToPlayer;
                player.position = reciever.position + positionOffset;

                playerIsOverlapping = false;
                forwardRenderPlane.enabled = false;
                recieverPlane.enabled = true;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }
}
