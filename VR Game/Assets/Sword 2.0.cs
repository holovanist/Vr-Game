using System.Collections.Generic;
using UnityEngine;

public class Sword2 : MonoBehaviour
{
    public List<Vector3> PositionsinCreatures = new List<Vector3>();
    public List<float> Speed = new List<float>();
    public int maxListLength;
    public float Trackinginterval;
    float timer;
    float TimeInCreature;
    public bool TrackingSword = false;
    public float distance;
    private void Update()
    {
        if(TrackingSword)
        {
            timer += Time.deltaTime;
            TrackSword();
        }
        else
        {
            if (timer != 0)
                timer = 0;
        }
        //if statement to start tracking the speed and depth - done
        //track the speed unneeded
        //track the depth not needed
        //tracking where sword is in the object and every .25 seconds add a new point to a list - done
        //maximum of 50 - done
    }
    private void OnTriggerEnter(Collider other)
    {
        TimeInCreature = 0;
        if (!other.gameObject.CompareTag("Enemy")) return;
        TrackingSword = true;
        //save the local enter position of the object
        //start tracking speed and depth - done
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        TrackingSword = false;
        //save the local exit position of the object
        //stop tracking speed and depth - done
        //method to calculate and deal damage - done
        CalculateDamage();
    }
    public void TrackSword()
    {
        TimeInCreature += Time.deltaTime;
        if (timer >= Trackinginterval)
        {
            if (PositionsinCreatures.Count < 50)
            {
                PositionsinCreatures.Add(transform.position);
            }
            else
            {
                PositionsinCreatures[49] = transform.position;
            }
        }
    }
    public void CalculateDamage()
    {
        for (int i = 0; i < PositionsinCreatures.Count - 1; i++)
        {
            if (i != PositionsinCreatures.Count)
            {
                float a = Vector3.Distance(PositionsinCreatures[i], PositionsinCreatures[i + 1]);
                distance += a;
            }
        }
        //get the distance between two points - done
        //get distance between the second and the thrid and reapeat this until all are done - done
        //damage calculations

        PositionsinCreatures.Clear();
    }
}
