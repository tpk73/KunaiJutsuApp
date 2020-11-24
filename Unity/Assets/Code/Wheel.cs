using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private int availableKunai;

    [SerializeField] private Sprite firstWheel;
    [SerializeField] private Sprite secondWheel;
    [SerializeField] private Sprite thirdWheel;

    [SerializeField] private bool isBoss;

    [Header("Prefabs")] 
    [SerializeField] private GameObject ramenPrefab;
    [SerializeField] private GameObject kunaiPrefab;

    [Header("Settings")] 
    [SerializeField] private float rotationZ;

    public List<Level> levels;

    [HideInInspector]
    public List<Kunai> kunais;

    private int levelIndex;

    public int AvailableKunai => availableKunai; 

    private void Start()
    {
        if (isBoss)
        {
            if(gameManager.Instance.stage < 5)
            {
                GetComponent<SpriteRenderer>().sprite = firstWheel;
            }
            else if (gameManager.Instance.stage > 5 && gameManager.Instance.stage < 10)
            {
                GetComponent<SpriteRenderer>().sprite = secondWheel;
            }
            else if (gameManager.Instance.stage > 10)
            {
                GetComponent<SpriteRenderer>().sprite = thirdWheel;
            }
        }

        rotateWheel();
        levelIndex = UnityEngine.Random.Range(0, levels.Count);

        if (levels[levelIndex].ramenChance > UnityEngine.Random.value)
        {
            spawnRamen();
            Debug.Log("Spawn Ramen");
        }

        spawnKunai();
        Debug.Log("spawn Kunai");

        
    }

    private void Update()
    {
        rotateWheel();
    }



    private void spawnKunai()
    {
        foreach (float kunaiAngle in levels[levelIndex].kunaiAngleFromWheel)
        {
            GameObject tempKunai = Instantiate(kunaiPrefab);
            tempKunai.transform.SetParent(transform);

            setRotationFromWheel(transform, tempKunai.transform, kunaiAngle, 0.20f, 180f);
            tempKunai.transform.localScale = new Vector3(.8f, .8f, .8f);
            Debug.Log("spawn Kunai");
        }
    }
    private void spawnRamen()
    {
        foreach (float ramenAngle in levels[levelIndex].ramenAngleFromWheel)
        {
            GameObject tempRamen = Instantiate(ramenPrefab);
            tempRamen.transform.SetParent(transform);

            setRotationFromWheel(transform, tempRamen.transform, ramenAngle, 0.25f, 0f);
            tempRamen.transform.localScale = Vector3.one;
            Debug.Log("Spawn Ramen");
        }
    }


    public void setRotationFromWheel(Transform wheel, Transform objectToPlace, float angle, float spaceFromObject, float objectRotation)
    {
        Vector2 offset = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)) * (wheel.GetComponent <CircleCollider2D>().radius + spaceFromObject);
        objectToPlace.localPosition = (Vector2) wheel.localPosition + offset;
        objectToPlace.localRotation = Quaternion.Euler(0, 0, -angle + objectRotation);
    }

    public void kunaiHit(Kunai kunai)
    { 
        kunai.myRigidBody2d.isKinematic = true;
        kunai.myRigidBody2d.velocity = Vector2.zero;
        kunai.transform.SetParent(transform);
        kunai.hit = true;

        kunais.Add(kunai);

        if (kunais.Count >= AvailableKunai)
        {
            // goto next level
            levelManager.Instance.nextLevel();
        }

        // score ++ 
        gameManager.Instance.score++;
    }

    private void rotateWheel()
    {
        transform.Rotate(0, 0, rotationZ * Time.deltaTime);
    }

    public void destroyKnife ()
    {
        foreach (var kunai in kunais)
        {
            Destroy(kunai.gameObject);
        }

        Destroy(gameObject);
    }
}

[Serializable]
public class Level
{
    [Range(0, 1)] 
    [SerializeField] public float ramenChance;

    public List<float> ramenAngleFromWheel = new List<float>();
    public List<float> kunaiAngleFromWheel = new List<float>();
}
