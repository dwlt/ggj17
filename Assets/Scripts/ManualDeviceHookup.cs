using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualDeviceHookup : MonoBehaviour {


    public SteamVR_TrackedObject.EIndex device;
	// Use this for initialization
	void Start () {

        this.gameObject.GetComponent<SteamVR_TrackedObject>().index = this.device;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
