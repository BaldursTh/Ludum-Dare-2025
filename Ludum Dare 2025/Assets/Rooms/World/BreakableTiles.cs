using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTiles : MonoBehaviour
{
    Tilemap tilemap;
    PlayerMovement player;
    EffectHandler effectHandler;
    public EffectData effectData;
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        effectHandler = gameObject.AddComponent<EffectHandler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detecting the Grid Position of Player
        if (collision.transform.parent.tag == "Player")
        {
            if (!(player.Attacking || player.Dashing)) return;
            Vector3 dir = player.rb.velocity.normalized;
            float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;
            Quaternion mult = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            Vector3Int pPos = tilemap.WorldToCell(collision.transform.position - (mult * new Vector3(1, 2f, 0)));
            Vector3Int pPos2 = tilemap.WorldToCell(collision.transform.position - (mult * new Vector3(-1, 2f, 0)));
            Vector3 tileCenter = tilemap.CellToWorld(pPos) + new Vector3(1, 1f, 0);
            Vector3 tileCenter2 = tilemap.CellToWorld(pPos2) + new Vector3(1, 1f, 0);
            if (pPos == pPos2) {
                effectHandler.CreateEffect(effectData, tileCenter, Quaternion.identity);

                tilemap.SetTile(pPos, null);
                return;
            }
            if (tilemap.GetTile(pPos) != null) effectHandler.CreateEffect(effectData, tileCenter, Quaternion.identity);

            tilemap.SetTile(pPos, null);
            if (tilemap.GetTile(pPos2) != null) effectHandler.CreateEffect(effectData, tileCenter2, Quaternion.identity);

            tilemap.SetTile(pPos2, null);
        }

    }
}
