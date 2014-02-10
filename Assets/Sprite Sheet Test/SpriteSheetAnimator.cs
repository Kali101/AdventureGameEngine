using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SpriteSheetAnimator : MonoBehaviour {
	string sheetName = "";
	Texture2D spriteSheet = null;
	public GUISkin spriteSheetEditorSkin;
	public GameObject animatedObject;
	public Texture2D packedAnimationSheet;
	public string packedAnimationSheetName;
	
	string xoffsetStr = "";
	string yoffsetStr = "";
	string cellwidthStr = "";
	string cellheightStr = "";
	string rowsStr = "";
	string colsStr = "";
	string fpsStr = "30";
	string animName = "";
	int xoffset;
	int yoffset;
	int cellwidth;
	int cellheight;
	int rows;
	int cols;
	int fps = 30;
	Texture2D gridOverlay = null;
	Rect spriteSheetLoc;
	Texture2D[,] frames;
	List<Texture2D> selectedFrames = new List<Texture2D>();
	Rect[] animCoords;
	
	void CutAssets() {
		frames = new Texture2D[(cols-1),(rows-1)];
		for(int i = 0; i < rows - 1; i++) {
			for(int j = 0; j < cols - 1; j++) {
				Texture2D newFrame = new Texture2D(cellwidth, cellheight);
				for(int x = 0; x < cellwidth; x++) {
					for(int y = 0; y < cellheight; y++) {
						newFrame.SetPixel(x, cellheight - y, spriteSheet.GetPixel(x + (i*cellwidth) + xoffset, (spriteSheet.height - yoffset - (y + (j*cellheight)))));
					}
				}
				newFrame.Apply();
				frames[j,i] = newFrame;
			}
		}
		gridOverlay = null;
		spriteSheet = null;
		

	}
	
	void OnGUI() {
		GUI.skin = spriteSheetEditorSkin;
		GUILayout.Space(10.0f);
		GUILayout.BeginHorizontal();
		GUILayout.Space(10.0f);
		sheetName = GUILayout.TextField(sheetName, GUILayout.Width(150.0f));
		GUILayout.Space(10.0f);
		GUILayout.BeginVertical();
		if(GUILayout.Button("Import sheet")) {
			spriteSheet = Resources.Load(sheetName) as Texture2D;
			frames = null;
			if(spriteSheet == null) {
				Debug.LogError("ERROR: File " + sheetName + " could not be loaded.");
			}
		}
		if(selectedFrames.Count > 0) {
			GUILayout.Label("FPS:");
			fpsStr = GUILayout.TextField(fpsStr, GUILayout.Width(150.0f));
			GUILayout.Label("Animation Name:");
			animName = GUILayout.TextField(animName, GUILayout.Width(150.0f));
			if(GUILayout.Button("Create animation!")) {
				Texture2D packedFrames = new Texture2D(cellwidth*selectedFrames.Count, cellheight);
				Texture2D[] framesToPack = new Texture2D[selectedFrames.Count];
				for(int i = 0; i < selectedFrames.Count; i++) {
					framesToPack[i] = selectedFrames[i];
				}
				animCoords = packedFrames.PackTextures(framesToPack, 0);
				packedAnimationSheet = packedFrames;
				packedAnimationSheetName = animatedObject.name + animName;
				
				AnimationData newAnimData = new AnimationData(packedAnimationSheetName, fps, animCoords);
				AnimationComponent acomp = animatedObject.GetComponent(typeof(AnimationComponent)) as AnimationComponent;
				if(acomp == null)
					acomp = animatedObject.AddComponent(typeof(AnimationComponent)) as AnimationComponent;
				acomp.AddAnimation(newAnimData);
				acomp.SetAnimation(packedAnimationSheetName);
			}
		}
		GUILayout.EndVertical();
		GUILayout.Space(10.0f);

		if(spriteSheet) {
			spriteSheetLoc = new Rect(10.0f, 150.0f, spriteSheet.width, spriteSheet.height);
			GUI.Box(spriteSheetLoc, spriteSheet);
			
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			GUILayout.Label("X Offset:");
			xoffsetStr = GUILayout.TextField(xoffsetStr, GUILayout.Width(100.0f));
			GUILayout.Label("Y Offset:");
			yoffsetStr = GUILayout.TextField(yoffsetStr, GUILayout.Width(100.0f));
			GUILayout.EndVertical();
			GUILayout.BeginVertical();
			GUILayout.Label("Cell Width:");
			cellwidthStr = GUILayout.TextField(cellwidthStr, GUILayout.Width(100.0f));
			GUILayout.Label("Cell Height:");
			cellheightStr = GUILayout.TextField(cellheightStr, GUILayout.Width(100.0f));
			GUILayout.EndVertical();
			GUILayout.BeginVertical();
			GUILayout.Label("Rows:");
			rowsStr = GUILayout.TextField(rowsStr, GUILayout.Width(100.0f));
			GUILayout.Label("Columns:");
			colsStr = GUILayout.TextField(colsStr, GUILayout.Width(100.0f));
			GUILayout.EndVertical();
			GUILayout.BeginVertical();
			if(GUILayout.Button("Generate Grid")) {
				xoffset = Convert.ToInt32(xoffsetStr);
				yoffset = Convert.ToInt32(yoffsetStr);
				cellwidth = Convert.ToInt32(cellwidthStr);
				cellheight = Convert.ToInt32(cellheightStr);
				rows = Convert.ToInt32(rowsStr);
				cols = Convert.ToInt32(colsStr);
				
				gridOverlay = new Texture2D(spriteSheet.width, spriteSheet.height);
				for(int i = 0; i < spriteSheet.height; i++) {
					for(int j = 0 ; j < rows ; j++) {
						int x = j*cellwidth + xoffset;
						if(x < spriteSheet.width) {
							gridOverlay.SetPixel(x, i, Color.cyan);
						}
					}				
				}
				for(int i = 0; i < spriteSheet.width; i++) {
					for(int j = 0 ; j < cols; j++) {
						int y = j*cellheight + yoffset;
						if(y < spriteSheet.height) {
							gridOverlay.SetPixel(i, spriteSheet.height - y, Color.cyan);
						}
					}				
				}
				gridOverlay.Apply();
				

			}
			if(gridOverlay) {
				if(GUILayout.Button("Cut Assets")) {
					CutAssets();
				}
			}
			GUILayout.EndVertical();
			GUI.Box(spriteSheetLoc, gridOverlay);
			GUILayout.EndHorizontal();	

		} else {
			xoffsetStr = "";
			yoffsetStr = "";
			cellwidthStr = "";
			cellheightStr = "";
			rowsStr = "";
			colsStr = "";		
			
			GUILayout.BeginArea(new Rect(0.0f, 150.0f, cols*cellwidth*2.0f, rows*cellheight*2.0f));
			if(frames != null && frames.Length != 0) {
				GUILayout.BeginHorizontal();
				for(int i = 0; i < cols - 1; i++) {
					for(int j = 0; j < rows - 1; j++) {
						GUILayout.FlexibleSpace();
						if(GUILayout.Button(frames[i,j])) {
							selectedFrames.Add(frames[i,j]);
						}
						GUILayout.FlexibleSpace();
					}
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndArea();

		}
		GUILayout.BeginHorizontal();
		if(selectedFrames.Count > 0) {
			for(int i = 0; i < selectedFrames.Count; i++) {
				if(GUILayout.Button(selectedFrames[i])) {
					selectedFrames.Remove(selectedFrames[i]);
				}
			}
		}
		else {
			fpsStr = "";
			fps = 30;
			animName = "";
		}
		GUILayout.EndHorizontal();
	}
	
}
