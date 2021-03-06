﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Critter : MonoBehaviour
{
    public float points;

    public float DistanceModifier = 1;

    public float CenterModifier = 1;

    public float FacingModifier = 1;

    private void Start()
    {
        //GetComponent<MeshRenderer>().enabled = false;
        gameObject.tag = "Critter";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }

    public void ActivateCritter()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    public ShotScore CalculatePoints(GameObject player, int numObjs, bool selfie)
    {
        var picValue = points;

        //Distance from player
        var distance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log("points for distance :" + (points * (DistanceModifier * (1 - Mathf.Clamp(distance, 0, 40)/20))));
        if (distance < 25)
        {
            picValue = picValue + (points * (DistanceModifier * (1 - Mathf.Clamp(distance, 0, 40)/20)));
        }

        //how close to center of the screen
        float center = Math.Abs(Vector3.Angle(player.transform.forward, transform.position - player.transform.position));
        //Debug.Log("Points for Angle off from center of screen:" + (points * CenterModifier * (90 - Mathf.Clamp(center, 0, 90))/90));
        picValue = picValue + (points * CenterModifier * (1 - Mathf.Clamp(center, 0 , 90))/90);

        //How close you are to facing eachother.
        float facing = Math.Abs(Vector3.Dot(player.transform.forward, transform.forward));
        //Debug.Log("Points for facing eachother:" + (points * FacingModifier * (1 - facing)));
        picValue = picValue + (points * FacingModifier * (1 - facing*2));

        float total = picValue * (1f + (0.2f * numObjs));
        
        if(selfie)
            total = total *1.1f;

        return new ShotScore()
        {
            total = total,
            facing = facing,
            center = center,
            distance = distance,
            selfie = selfie,
        };
    }
}
