using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TilebaseController : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public PolygonCollider2D polygonCollider;
    public int id;
    public List<TilebaseController> lsTileHigher;
    public static bool isPause;
    private void Update()
    {
        ResetColor();
    }
    private void Awake()
    {
        isPause = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }
    private void OnMouseDown()
    {
        if (lsTileHigher.Count > 0 || GameController.Instance.SortControllerRemake.lsTilebaseClicked.Count >= 8)
        {
            return;
        }
        if (isPause)
        {
            return;
        }
        SkillManager.instance.tilePosTracking.Add(this.transform.name, this.transform.position);

        this.transform.SetParent(null);
        this.transform.SetParent(GameController.Instance.ContainerTiles);

        GameController.Instance.audioManager.PlaySFX(GameController.Instance.audioManager.tileCLick);
        GameController.Instance.lsTilesInCurrentLevel.Remove(this);
        GameController.Instance.SortControllerRemake.HandleOnMouseDown(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int currentLayerIndex = gameObject.layer;
        int triggerLayerIndex = collision.gameObject.layer;
        if (currentLayerIndex < triggerLayerIndex)
        {
            TilebaseController tile = collision.gameObject.GetComponent<TilebaseController>();
            if (tile != null) lsTileHigher.Add(tile);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < lsTileHigher.Count; i++)
        {
            if (lsTileHigher[i].gameObject.name == collision.gameObject.name)
            {
                lsTileHigher.RemoveAt(i);
                break;
            }
        }
    }
    private void ResetColor()
    {
        if (lsTileHigher.Count > 0)
        {
            this.spriteRenderer.color = Color.gray;
        }
        if (lsTileHigher.Count == 0)
        {
            this.spriteRenderer.color = Color.Lerp(Color.gray, Color.white, 2f);

        }
    }
    public void HandleMoveLocal(Transform target, TweenCallback onComplete)
    {
        // Tắt collider để tránh va chạm khi đang di chuyển
        if (polygonCollider != null)
            polygonCollider.enabled = false;

        // Di chuyển tới vị trí local của target trong 0.3 giây
        transform.DOLocalMove(target.localPosition, 0.3f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                // Gọi callback sau khi di chuyển xong
                onComplete?.Invoke();
            });
    }
}
