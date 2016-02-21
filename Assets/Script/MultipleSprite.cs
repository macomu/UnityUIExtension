using UnityEngine;
using System.Collections;

/// <summary>複数のSpriteを含むオブジェクト.</summary>
public class MultipleSprite : MonoBehaviour {
	
	/// <summary>入れるスプライトのリスト.</summary>
	[SerializeField] private Sprite[] m_Sprites;
	
	/// <summary>名前からSpriteを取得する.</summary>
	/// <param name="spriteName">名前.</param>
	public Sprite GetSpriteByName (string spriteName) {
		int length = m_Sprites.Length;
		for (int index = 0; index < length; ++ index) {
			if (m_Sprites[index].name == spriteName) {
				return m_Sprites[index];
			}
		}
		
		return null;
	}
	
	/// <summary>全てのSpriteを取得.</summary>
	/// <returns>全Sprite.</returns>
	public Sprite[] GetAllSprite () {
		return m_Sprites;
	}
	
	/// <summary>Spriteのセット.<summary>
	/// <param name="sprites">セットするSprite配列.</param>s
	public void SetSprites (Sprite[] sprites) {
		m_Sprites = sprites;
	}
}
