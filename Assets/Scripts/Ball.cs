using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public static int score;
    public static int lives;

    public SpriteRenderer sprite { get { return GetComponent<SpriteRenderer> ( ); } }

    public GameObject gameOverText;
    public Paddle paddle;
    public float speed;
    public Vector3 startOffset;

    public AudioClip bounceSfx;
    public AudioClip respawnSfx;
    public AudioClip startSfx;
    public AudioClip dieSfx;

    private bool m_OnPaddle = true;
    private Vector2 m_Velocity;

    public void Reset()
    {
        SetPaddle(true);
        score = 0;
        lives = 5;
        gameOverText.SetActive(false);
        gameObject.SetActive(true);
    }

    void Start() {
        Reset();
    }
    
	// Update is called once per frame
	void Update () {
        if (lives < 0)
            return;

        if(m_OnPaddle && Input.GetMouseButtonDown ( 0 ) ) {
            SetPaddle ( false );
        }

        if ( !m_OnPaddle ) {
            transform.position += (Vector3)m_Velocity * speed * Time.deltaTime;

            Vector3 collisionNormal;
            if ( Camera.main.GetComponent<CameraHelper> ( ).IsOutside ( sprite.bounds, out collisionNormal ) ) {
                if ( Mathf.Approximately ( Vector3.Dot ( collisionNormal.normalized, Vector3.up ), 1 ) ) {
                    if (lives > 0)
                    {
                        SetPaddle(true);
                        Camera.main.GetComponent<CameraHelper>().ShakeScreen(0.5f);
                    } else
                    {
                        gameOverText.SetActive(true);
                        gameObject.SetActive(false);
                    }
                    lives--;
                    PlayAudioClip(dieSfx);
                } else {
                    transform.position += collisionNormal;
                    Bounce ( collisionNormal.normalized );
                    Camera.main.GetComponent<CameraHelper> ( ).ShakeScreen ( 0.1f );
                    PlayAudioClip(bounceSfx, true);
                }
            }
        }
	}

    void OnCollisionEnter2D(Collision2D coll) {
        Bounce ( coll.contacts [ 0 ].normal );
        Camera.main.GetComponent<CameraHelper> ( ).ShakeScreen ( 0.25f );
        PlayAudioClip(bounceSfx, true);
    }

    void Bounce(Vector2 normal) {
        Vector2 dir = m_Velocity.normalized;
        if ( Vector3.Dot ( dir, normal ) > 0 )
            return;

        SetDirection ( Vector2.Reflect ( dir, normal ) );
    }

    void SetDirection(Vector2 vector) {
        m_Velocity = vector * speed;
    }

    void SetPaddle(bool onPaddle) {
        m_OnPaddle = onPaddle;
        transform.SetParent ( onPaddle ? paddle.transform : null );

        if(!onPaddle) {
            Vector2 direction = new Vector2 ( 0.707f, 0.707f );
            if ( Random.Range ( 0, 2 ) == 1 )
                direction.x = -direction.x;

            SetDirection ( direction );
            PlayAudioClip(startSfx);
        } else {
            transform.localPosition = startOffset;
            PlayAudioClip(respawnSfx);
        }
    }

    void PlayAudioClip(AudioClip clip, bool randomPitch = false)
    {
        AudioSource source = GetComponent<AudioSource>();
        source.pitch = randomPitch ? Random.Range(0.5f, 1f) : 1;
        source.PlayOneShot(clip, 1);
    }
}
