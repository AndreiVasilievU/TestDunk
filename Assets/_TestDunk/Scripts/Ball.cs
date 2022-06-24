using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int ringNumber = 0;
    public float timerToDie;

    [SerializeField] private Rigidbody2D ballRigidbody;
    [SerializeField] private GameObject[] effects;
    [SerializeField] private GameObject bouncePrefab;
    private void Update()
    {
        if (ballRigidbody.bodyType == RigidbodyType2D.Dynamic)
        {
            timerToDie += Time.deltaTime;
            if (timerToDie > 5f)
            {
                MainCanvas.instance.OnGameOver();
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DeathZone")
        {
            AudioManager.instance.PlayOneShot(Clips.Die);
            MainCanvas.instance.OnGameOver();
            timerToDie = 0;
            Destroy(gameObject, 1f);
        }
        else if (other.gameObject.tag == "Ring")
        {
            AudioManager.instance.PlayOneShot(Clips.Dash);
            timerToDie = 0;
        }
        else if (other.gameObject.tag == "Star")
        {
            other.gameObject.SetActive(false);
            AudioManager.instance.PlayOneShot(Clips.CatchStar);
            MainCanvas.instance.UpdateStarScore();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
            effects[0].SetActive(true);
            Instantiate(bouncePrefab, transform.position, Quaternion.identity);

            CounterManager.instance.SetBounceCounter();

            AudioManager.instance.PlayOneShot(Clips.Dash);
            timerToDie = 0;
        }
    }

    public void OffEffects()
    {
        effects[0].SetActive(false);
    }
}
