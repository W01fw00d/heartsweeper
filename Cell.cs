using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

    public bool hasMine = false;
    private readonly float percentage_mines =  0.15f;

    public Sprite[] emptyTextures;
    public Sprite mineTexture;

    private readonly string covered_sprite_name = "panel";

	// Use this for initialization
	void Start () {
        hasMine = (Random.value < percentage_mines);

        int x = (int) this.transform.position.x;
        int y = (int) this.transform.position.y;

        GridHelper.cells[x, y] = this;

        //LoadTexture(0); //Debugging
	}


    public bool IsCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == covered_sprite_name;
    }

    public void OnMouseUpAsButton()
    {
        if (IsCovered())
        {
            if (hasMine)
            {
                // TODO: Mostrar gameover y cerrar app
                Debug.Log("BOOOM! Te enamoraste!");
                GridHelper.UncoverAllTheMines();
            }
            else
            {
                // TODO: cambiar la imagen
                int x = (int)this.transform.position.x;
                int y = (int)this.transform.position.y;

                LoadTexture(GridHelper.CountAdjacentMines(x, y));
                // TODO: descubrir toda el área sin minas alrededor
                // TODO: comprobar si el juego ha terminado
            }
        }
    }

    public void LoadTexture(int loadTextureIndex)
    {
        if (hasMine)
        {
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        } else
        {
            GetComponent<SpriteRenderer>().sprite = emptyTextures[loadTextureIndex];
        }
    }
}
