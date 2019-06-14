using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayableDirector director = gameObject.AddComponent<PlayableDirector>();

        //director.wrapMode = DirectorWrapMode.Loop;
        director.initialTime = 0.0;
        director.timeUpdateMode = DirectorUpdateMode.GameTime;

        TimelineAsset timeline = (TimelineAsset)Resources.Load("MyTimeline");
        
        IEnumerator<PlayableBinding> outputs = timeline.outputs.GetEnumerator();

        PlayableBinding binding = outputs.Current;
        
        TrackAsset track = binding.sourceObject as TrackAsset;
        director.SetGenericBinding(track, gameObject);
        director.Play(timeline);
        director.time = 42.0f;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
