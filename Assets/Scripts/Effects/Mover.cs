﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public bool SetOnStart;
    
    public Rail rail;
    public STMethods.RailPlaymode mode;

    public float speed = 2.5f;
    [HideInInspector]public float curSpeed;
    public bool isReversed;
    public bool isLooping;
    public bool pingPong;

    public int currentSeg;
    public float transition;

    private bool isCompleted;

    // Start is called before the first frame update
    void Start()
    {
        if (SetOnStart)
        {
            if (!isReversed)
            {
                currentSeg = 0;
            }
            else
            {
                currentSeg = rail.nodes.Length - 2;
                transition = 1;
            }

            curSpeed = speed;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!rail) return;

        if (!isCompleted) Play(!isReversed);
    }

    private void Play(bool forward)
    {
        float m = (rail.nodes[currentSeg + 1].position - rail.nodes[currentSeg].position).magnitude;
        float s = (Time.deltaTime * 1 / m) * curSpeed;
        transition += (forward)?s:-s;
        if (transition > 1)
        {
            transition = 0;
            currentSeg++;
            if (currentSeg == rail.nodes.Length - 1)
            {
                if (isLooping)
                {
                    if (pingPong)
                    {
                        transition = 1;
                        currentSeg = rail.nodes.Length - 2;
                        isReversed = !isReversed;
                    }
                    else
                    {
                        currentSeg = 0;
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }
            }
        }
        else if (transition < 0)
        {
            transition = 1;
            currentSeg--;
            if (currentSeg == - 1)
            {
                if (isLooping)
                {
                    if (pingPong)
                    {
                        transition = 0;
                        currentSeg = 0;
                        isReversed = !isReversed;
                    }
                    else
                    {
                        currentSeg = rail.nodes.Length-2;
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }
            }
        }

        transform.position = rail.PositionOnRail(currentSeg, transition, mode);
        transform.rotation = rail.Orientation(currentSeg, transition);
    }
}