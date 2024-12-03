using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class grid : MonoBehaviour
{
    public GameObject tilePrefab;
    public TMP_Text positionText;

    public int x_colums;
    public int z_colums;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < x_colums; x++)
        {
            for (int y = 0; y < z_colums; y++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector3(x, 0, y);
                TileScript tileScript = tile.GetComponent<TileScript>();
                tileScript.x = x;
                tileScript.y = y;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            TileScript tileScript = hit.collider.GetComponent<TileScript>();
            if (tileScript != null)
            {
                positionText.text = "Position: (" + tileScript.x + ", " + tileScript.y + ")";
            }
        }
    }

}

