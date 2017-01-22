using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConfetti : MonoBehaviour {
    public Rigidbody toLaunch;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void launchObjects()
    {
        for (int i = 0; i < 10; i++)
        {
            Rigidbody instance = Instantiate(toLaunch, transform);
            GetRandomUpwardsVelocity(instance);
        }
    }

    private void GetRandomUpwardsVelocity(Rigidbody toLaunch)
    {
        toLaunch.velocity = new Vector3(Random.Range(0, 3), Random.Range(0, 10), Random.Range(0, 3));
    }
}
