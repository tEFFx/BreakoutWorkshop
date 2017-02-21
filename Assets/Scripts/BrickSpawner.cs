using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour {
    public int bricksX;
    public int bricksY;
    public GameObject brickPrefab;
    public Sprite[] rowSprites;

    private GameObject[] m_Bricks;

	// Use this for initialization
	void Start () {
        m_Bricks = new GameObject [ bricksX * bricksY ];

        Bounds brickBounds = brickPrefab.GetComponent<SpriteRenderer> ( ).bounds;
        Bounds playfieldBounds = CalculatePlayfieldBounds ( );

        for ( int x = 0 ; x < bricksX ; x++ ) {
            for ( int y = 0 ; y < bricksY; y++ ) {
                GameObject brick = Instantiate ( brickPrefab, transform );
                brick.transform.position = playfieldBounds.min + Vector3.Scale ( brickBounds.size, new Vector3 ( x, y, 1 ) ) + brickBounds.size * 0.5f;
                brick.GetComponent<SpriteRenderer> ( ).sprite = rowSprites [ y % rowSprites.Length ];
                m_Bricks [ bricksX * y + x ] = brick;
            }
        }
	}

    void Update() {
        if ( Input.GetKeyDown ( KeyCode.R ) )
            Reset ( );
    }
	
    void Reset() {
        for ( int i = 0 ; i < m_Bricks.Length ; i++ ) {
            m_Bricks [ i ].SetActive ( true );
        }
    }

    Bounds CalculatePlayfieldBounds() {
        Bounds brickBounds = brickPrefab.GetComponent<SpriteRenderer> ( ).bounds;
        Bounds playfieldBounds = brickBounds;
        playfieldBounds.size = Vector3.Scale ( brickBounds.size, new Vector3 ( bricksX, bricksY, 1 ) );
        playfieldBounds.center = transform.position;
        return playfieldBounds;
    }

    void OnDrawGizmos() {
        Bounds bounds = CalculatePlayfieldBounds ( );
        Gizmos.DrawWireCube ( bounds.center, bounds.size );
    }
}
