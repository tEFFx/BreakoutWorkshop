using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D coll) {
        Ball.score += 100;
        gameObject.SetActive ( false );
    }
}
