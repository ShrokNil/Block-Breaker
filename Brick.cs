using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
public Sprite[] hitSprites;
public AudioClip x;
public GameObject smoke;

private int timesHit;
private LevelManager levelManager;
public static int breakableCount = 0;
private bool isBreakable;	
	// Use this for initialization
	void Start () {
	isBreakable	= (this.tag == "Breakable");
	//Keep track of breakable bricks
	if (isBreakable) {
	breakableCount++;
	} 
	
	levelManager = GameObject.FindObjectOfType<LevelManager>();
	timesHit = 0;
	
	}
	
	// Update is called once per frame
	void Update () {}
		
		void OnCollisionEnter2D (Collision2D collision) {
		
		if (isBreakable) {
		HandleHits();
	}
}
		void HandleHits() {
		timesHit++;
		int maxHits = hitSprites.Length + 1;
		if (timesHit >= maxHits) {
			breakableCount--;
			levelManager.BrickDestroyed();
			PuffSmoke();
			AudioSource.PlayClipAtPoint (Dicks, transform.position);
			Destroy(gameObject);
		} else {
			LoadSprites();
		}
	 }
	
	void PuffSmoke() {
		GameObject smokePuff = Instantiate (smoke, transform.position, Quaternion.identity) as GameObject;
		smokePuff.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	
	void LoadSprites(){
		int spriteIndex = timesHit - 1;
		if (hitSprites[spriteIndex]) {
	this.GetComponent<SpriteRenderer>().sprite = hitSprites [spriteIndex];
		} else {
		Debug.LogError ("Brick sprite missing");
		}
	}
	//TODO Remove this Method once we can win.
	void SimulateWin () {
	levelManager.LoadNextLevel();
	}
}
