using System.Collections.Generic;
using UnityEngine;

public class Sword2 : MonoBehaviour
{
    public GameObject TipOfSword;
    public List<Vector3> PositionsinCreaturesTip = new List<Vector3>();
    public List<float> Speed = new List<float>();
    public int maxListLength;
    public float TrackingInterval;
    float timer;
    float TimeInCreature;
    public bool TrackingSword = false;
    public float distance;
    public float damage;
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
        //tracking where sword is in the object and every .25 seconds add a new point to a list - done
        //maximum of 50 - done
    }
    private void OnTriggerEnter(Collider other)
    {
        TimeInCreature = 0;
        if (!other.gameObject.CompareTag("Enemy")) return;
        TrackingSword = true;
        //save the local enter position of the object - done
        //start tracking speed and depth - done
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        TrackingSword = false;
        //save the local exit position of the object
        //stop tracking speed and depth - done
        //method to calculate and deal damage - done
        distance = CalculateDamage();
        damage *= distance;
    }
    public void TrackSword()
    {
        TimeInCreature += Time.deltaTime;
        if (timer >= TrackingInterval)
        {
            if (PositionsinCreaturesTip.Count < 50)
            {
                PositionsinCreaturesTip.Add(TipOfSword.transform.position);
            }
            else
            {
                PositionsinCreaturesTip[49] = TipOfSword.transform.position;
            }
            timer = 0;
        }
    }
    public float CalculateDamage()
    {
        float Distance = 0;
        for (int i = 0; i < PositionsinCreaturesTip.Count - 1; i++)
        {
            if (i != PositionsinCreaturesTip.Count)
            {
                float a = Vector3.Distance(PositionsinCreaturesTip[i], PositionsinCreaturesTip[i + 1]);
                Distance += a;
            }
        }
        //get the distance between two points - done
        //get distance between the second and the thrid and reapeat this until all are done - done
        //damage calculations
        PositionsinCreaturesTip.Clear();
        return Distance;
    }
}
