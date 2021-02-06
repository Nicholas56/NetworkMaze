using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTopDown : MonoBehaviour
{
    static public Transform player;
    static public Transform token;
	public GameObject radar;
	public List<Sprite> radarSprites;
	int radarSprite = 0;
	public Transform playerPoints;
	int spawnPoint;
	public float height;
	void FixedUpdate()
	{
		if (player)
		{
			transform.position = new Vector3(player.position.x, height, player.position.z);
			if (token)
			{
				Vector2 vector = new Vector2(token.position.x - player.position.x, token.position.z - player.position.z).normalized;
				radar.transform.rotation = Quaternion.LookRotation(vector);
			}
		}
	}

	public void CyclePlayer()
	{
		player.GetComponent<PlayerScript>().Respawn();
	}

	public void CycleRadar()
	{
		radarSprite++;
		if (radarSprite == radarSprites.Count) { radarSprite = 0; }
		radar.transform.GetChild(0).GetComponent<Image>().sprite = radarSprites[radarSprite];
	}

	public void Zoom(int zoomAmount)
	{
		height = height + zoomAmount;
		height = Mathf.Max(height, 30);
		height = Mathf.Min(height, 90);
	}
}
