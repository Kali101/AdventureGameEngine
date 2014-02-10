using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

using Rand = UnityEngine.Random;

public class StatusSelectBox : MonoBehaviour 
{
    public TextMesh statusText;
    public Texture2D blankTex;
	
	private float zScaleFactor;
	
	private Vector2 startDimensions;
	
    private Texture2D image;
    public Texture2D Image
    {
        get { return image; }
        set 
		{ 
			image = value; 
			if(image == null)
				image = blankTex;
			/*if(image.width >= image.height) { 
				float ht = (image.height * startDimensions.x) / image.width; 
				gameObject.transform.localScale = (new Vector3(startDimensions.x, gameObject.transform.localScale.y, ht * zScaleFactor));
				Debug.Log("WIDTH width: "+startDimensions.x+" height: "+ht);
			} else {
				float wd = (image.width * startDimensions.y) / (image.height * zScaleFactor); 
				gameObject.transform.localScale = (new Vector3(wd, gameObject.transform.localScale.y, startDimensions.y));	
				Debug.Log("HEIGHT width: "+wd+" height: "+startDimensions.y);
			}*/
			renderer.material.SetTexture("_MainTex", image); 
		}
    }

    private string text = "";
    public string Text 
    {
        get { return text; }
        set { text = value; statusText.text = text; }
    }

	void Start()
	{
		startDimensions = new Vector2(transform.localScale.x, transform.localScale.z);
		zScaleFactor = transform.localScale.z / transform.localScale.x;
		Debug.Log("hey width "+startDimensions.x+" also height "+startDimensions.y);
	}
}
