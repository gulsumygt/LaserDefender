using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    Waveconfig waveconfig;

    List<Transform> wayPoints;
    int waypointIndex = 0;
    // Use this for initialization
    void Start()
    {
        wayPoints = waveconfig.GetWaypoints();

        transform.position = wayPoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(Waveconfig waveconfig)
    {
        this.waveconfig = waveconfig;
    }

    private void Move()
    {
        if (waypointIndex < wayPoints.Count)
        {
            var targetPosition = wayPoints[waypointIndex].transform.position;
            var speed = waveconfig.GetmoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
