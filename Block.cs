using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //Configuration parameters
    [SerializeField] AudioClip breakSound = null;
    [SerializeField] GameObject blockSparklesVFX = null;
    [SerializeField] Sprite[] hitSprites = null;

    //Cached reference
    Level level;

    //State variable
    [SerializeField] int timesHit; //Only serialized for debug purposes

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        if (tag == "Breakable")
        {
            level = FindObjectOfType<Level>();
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit-1;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block Sprite is Missing From Array. Object: " + gameObject.name);
        }
        
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        FindObjectOfType<GameSession>().AddToScore();
        //TriggerSparklesVFX();
        Destroy(gameObject);
        level.BlockDestroyed();
    }

    private void PlayBlockDestroySFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(blockSparklesVFX, 2f);
    }
}
