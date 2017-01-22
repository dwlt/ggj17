using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConfetti : MonoBehaviour {
    public Rigidbody toLaunch;
    public int confettis = 10;
    public Vector3 rangeForConf;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void launchObjects()
    {
        for (int i = 0; i < confettis; i++)
        {
            Rigidbody instance = Instantiate(toLaunch, transform);
            GetRandomUpwardsVelocity(instance);
        }
    }

    private void GetRandomUpwardsVelocity(Rigidbody toLaunch)
    {
        toLaunch.velocity = new Vector3(Random.Range(0, rangeForConf.x), Random.Range(1, rangeForConf.y), Random.Range(0, rangeForConf.z));
    }
}
