using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

	public bool alive;

	private SpriteRenderer sprite;
	private bool nextAlive;
	private Cell[] neighborsCell;

	void Awake(){
		sprite = GetComponentInChildren<SpriteRenderer> ();
		Cell[] cellsInGame = GameObject.FindObjectsOfType<Cell> ();
		neighborsCell = new Cell[8];
		int numbCell = 0;
		foreach (Cell thisCell in cellsInGame) {
			float posX = transform.position.x - thisCell.transform.position.x;
			float posY = transform.position.y - thisCell.transform.position.y;
			// If it's the same cell
			if(posX == 0 & posY == 0){
				continue;
			}
			if(posX > 1 || posX < -1){
				continue;
			}
			if(posY > 1 || posY < -1){
				continue;
			}
			neighborsCell[numbCell++] = thisCell;
		}
	}
	
	public bool IsAlive(){
		return alive;
	}

	public void Step(){
		if (nextAlive) {
			ReviveCell ();
		} else {
			KillCell();
		}
	}

	public void PrepareStep(){
		int counter = CountNumberNeighbors ();
		if (alive) {
			if(counter < 2){
				nextAlive = false;
			} else if(counter > 3){
				nextAlive = false;
			} else {
				nextAlive = true;
			}
		} else {
			if(counter == 3){
				nextAlive = true;
			}
		}
	}

	int CountNumberNeighbors(){
		int counter = 0;
		foreach (Cell cell in neighborsCell) {
			if(cell == null){
				continue;
			}
			if(!cell.IsAlive()){
				continue;
			}
			counter++;
		}
		return counter;
	}

	public void KillCell(){
		alive = false;
		nextAlive = false;
		sprite.color = Color.black;
	}

	public void ReviveCell(){
		alive = true;
		nextAlive = true;
		sprite.color = Color.red;
	}

	void OnMouseDown(){
		if (alive) {
			KillCell ();
		} else {
			ReviveCell();
		}
	}
}
