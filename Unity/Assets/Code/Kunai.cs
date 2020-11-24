using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kunai : MonoBehaviour
{

    [SerializeField] private float speed;

    public Rigidbody2D myRigidBody2d;

    public bool isReleased { get; set; }
    public bool hit { get; set; }

    public void throwKunai ()
    {
        if (!isReleased && !gameManager.Instance.isGameOver)
        {
            isReleased = true;
            myRigidBody2d.AddForce(new Vector2(x: 0f, y: speed), ForceMode2D.Impulse);
            Debug.Log("threw Kunai");
        }
        isReleased = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wheel") && !hit && !gameManager.Instance.isGameOver && !isReleased)
        {
            other.gameObject.GetComponent<Wheel>().kunaiHit(this);
            gameManager.Instance.score++;
            SoundManager.Instance.playKunaiHitButton();
            Debug.Log("Kunai Hit Score++");
        }

        else if (other.gameObject.CompareTag("Kunai") && !hit && !gameManager.Instance.isGameOver && !other.gameObject.GetComponent<Kunai>().isReleased)
        {
            transform.SetParent(other.transform);
            myRigidBody2d.velocity = Vector2.zero;
            myRigidBody2d.isKinematic = true;

            SoundManager.Instance.Vibrate();

            gameManager.Instance.isGameOver = true;

            Debug.Log("Game Is Over");
            UIManager.Instance.gameOver();

            Invoke("GameOver", 2f);
        
        }
    }

    private void GameOver()
    {
        UIManager.Instance.gameOver();
    }
 
}
