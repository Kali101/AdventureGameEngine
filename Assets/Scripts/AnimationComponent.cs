using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AnimationComponent : MonoBehaviour {
	List<AnimationData> animations = new List<AnimationData>();

	AnimationData currentAnimation;
	
	void Update() {
		foreach(AnimationData ad in animations) {
			if(ad.animationName == currentAnimation.animationName) {
				int index = Convert.ToInt32(Time.fixedTime * ad.fps);
				index = index % ad.animationUVs.Length;
			
				renderer.material.SetTextureOffset("_MainTex", new Vector2(ad.animationUVs[index].x, ad.animationUVs[index].y));
    			renderer.material.SetTextureScale("_MainTex", new Vector2(ad.animationUVs[index].width, ad.animationUVs[index].height));
			}
		}
	}
	
	public void AddAnimation(AnimationData ad) {
		animations.Add(ad);
	}
	
	public void SetAnimation(string name) {
		foreach(AnimationData ad in animations) {
			if(ad.animationName == name) {
				currentAnimation = ad;
				renderer.material.SetTexture("_MainTex", Resources.Load(ad.animationName) as Texture2D);
			}
		}
	}
}
