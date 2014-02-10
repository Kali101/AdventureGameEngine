using UnityEngine;
using System.Collections;

public struct AnimationData {
	public string animationName;	// name of animation and its corresponding sprite sheet in resources
	public int fps;
	public Rect[] animationUVs;
	
	public AnimationData(string anims, int f, Rect[] auv) {
		animationName = anims;
		fps = f;
		animationUVs = auv;
	}

}
