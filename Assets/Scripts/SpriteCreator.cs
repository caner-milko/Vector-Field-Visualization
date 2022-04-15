using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCreator: MonoBehaviour {

	public Sprite createdSprite;
	public int spriteEdges = 16;


	public Sprite CreateEmptySprite() {
		Sprite sprite = Sprite.Create(new Texture2D(spriteEdges, spriteEdges), new Rect(Vector2.zero, new Vector2(spriteEdges, spriteEdges)), new Vector2(0.5f, 0.5f));
		this.createdSprite = sprite;
		return sprite;
	}

}
