using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayableDirector director = gameObject.AddComponent<PlayableDirector>();

        //director.wrapMode = DirectorWrapMode.Loop;
        director.initialTime = 0.0;
        director.timeUpdateMode = DirectorUpdateMode.GameTime;


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
