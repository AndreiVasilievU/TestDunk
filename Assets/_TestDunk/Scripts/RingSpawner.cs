using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RingSpawner : MonoBehaviour
{
    public static RingSpawner instance;

    [SerializeField] private GameObject ringPrefab;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] private GameObject ringsContainer;
    [SerializeField] private GameObject[] rings;

    private GameObject newRing;

    [Header ("Get from scene")]
    [SerializeField] private Vector3 startScaleRing;
    [Header("Time to change scale")]
    [SerializeField] private float time = 0.3f;
    [Header("Local position catcher in ring")]
    [SerializeField] private Vector3 catcherLocalPosition;

    private void Start()
    {
        instance = this;
    }

    public void SpawnRing()
    {
        if (rings[0] != null)
        {
            StartCoroutine(DestroyRing(rings[0]));
            StartCoroutine(ChangeTransform(rings[1].transform));
        }

        rings[0] = rings[1];
        rings[1] = null;

        StartCoroutine(CreateRing());
    }

    IEnumerator DestroyRing(GameObject ring)
    {
        ring.gameObject.transform.DOScale(0, time);
        yield return new WaitForSeconds(time);
        Destroy(ring);
    }

    IEnumerator CreateRing()
    {
        yield return new WaitForSeconds(time);

        newRing = Instantiate(ringPrefab, spawnPoints[Random.Range(0, 5)].position, Quaternion.identity, ringsContainer.transform);
        var ballCatcher = newRing.GetComponentInChildren<BallCatcher>();

        newRing.transform.localScale = Vector3.zero;
        newRing.transform.DOScale(startScaleRing, time);

        ballCatcher.myRingNumber = CounterManager.instance.ringsCounter;
        rings[1] = newRing;

        yield return new WaitForSeconds(time);

        ballCatcher.transform.localPosition = catcherLocalPosition;
    }
    IEnumerator ChangeTransform(Transform ring)
    {
        yield return new WaitForSeconds(time);
        ring.DOMoveY(-3, time);
    }
}
