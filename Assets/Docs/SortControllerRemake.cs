using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CandyCoded;
using CandyCoded.HapticFeedback;
using System.Linq;
using System;
public class SortControllerRemake : MonoBehaviour
{
    public List<Transform> lsSlotSort;
    public List<TilebaseController> lsTilebaseClicked;
    public List<TilebaseController> lsTilebaseSelected;
    public ParticleSystem matchedParticle;
    public int numTileIsMatched;
    private void Awake()
    {
        this.AddLsSlotSort();
        numTileIsMatched = 0;
    }
    private void Update()
    {
        if (lsTilebaseClicked.Count > 0)
        {
            AutoSortTileSlots();
        }
    }
    public void AddLsSlotSort()
    {
        foreach (Transform children in transform)
        {
            if (children.name == "Particle System") continue;
            lsSlotSort.Add(children);
        }
    }
    private void AutoSortTileSlots()
    {
        for (int i = 0; i < lsTilebaseClicked.Count; i++)
        {
            if (lsTilebaseClicked[i] != null)
            {
                Vector3 correctPos = lsSlotSort[i].position;
                if (lsTilebaseClicked[i].transform.position != correctPos)
                {
                    lsTilebaseClicked[i].transform.DOMove(correctPos, 0.2f);
                }
            }
        }
    }
    public void HandleOnMouseDown(TilebaseController tile)
    {
        if (lsTilebaseClicked.Count >= lsSlotSort.Count)
        {
            Debug.Log("Full slot → Game Over");
            return;
        }

        int validPos = GetValidPosToSort(tile);

        if (validPos < lsTilebaseClicked.Count)
        {
            RearrangeSlotSort(validPos);
        }

        tile.boxCollider.enabled = false;

        lsTilebaseClicked.Insert(validPos, tile);
        lsTilebaseSelected.Add(tile);

        MoveTileToTarget(tile, lsSlotSort[validPos].position, () =>
        {
            StartCoroutine(HandleTileMatch(tile));
        });

        GameController.Instance.numOfCurrentTile--;
    }

    private int GetValidPosToSort(TilebaseController tileBaseParam)
    {
        int count = 0;
        int tilePos = 0;
        for (int i = 0; i < lsTilebaseClicked.Count; i++)
        {
            if (tileBaseParam.id == lsTilebaseClicked[i].id)
            {
                count++;
                tilePos = i;
            }
        }
        if (count != 0) return tilePos + 1;
        return lsTilebaseClicked.Count;
    }

    public virtual void MoveTileToTarget(TilebaseController tile, Vector3 targetPos, Action onComplete = null)
    {
        tile.gameObject.transform.DOMove(targetPos, 0.5f).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }

    protected void RearrangeSlotSort(int index)
    {
        if (index == 0) return;
        for (int i = lsTilebaseClicked.Count; i > index; i--)
        {
            MoveTileToTarget(lsTilebaseClicked[i - 1], lsSlotSort[i].position);
        }
    }
    private IEnumerator HandleTileMatch(TilebaseController tile)
    {
        if (lsTilebaseClicked.Count < 3) yield break;
        int count = 0;
        List<TilebaseController> matchedTiles = new List<TilebaseController>();

        for (int i = 0; i < lsTilebaseClicked.Count; i++)
        {
            if (tile.id == lsTilebaseClicked[i].id)
            {
                count++;
                matchedTiles.Add(lsTilebaseClicked[i]);
                if (count == 3)
                {
                    int indexOfMiddleTile = lsTilebaseClicked.IndexOf(matchedTiles[1]);
                    Vector3 particlePos = GameController.Instance.SortControllerRemake.lsSlotSort[indexOfMiddleTile].position;

                    yield return new WaitForSeconds(0.2f);

                    foreach (var t in matchedTiles)
                    {
                        if (t != null)
                        {
                            t.transform.DOScale(Vector3.zero, 0.1f).SetDelay(0.1f);
                        }
                    }
                    yield return new WaitForSeconds(0.4f);

                    if (matchedParticle != null)
                    {
                        if (PlayerPrefs.GetInt("IsVibration") == 1)
                        {
                            HapticFeedback.LightFeedback();
                        }
                        matchedParticle.transform.position = particlePos;
                        matchedParticle.Play();
                        GameController.Instance.audioManager.PlaySFX(GameController.Instance.audioManager.moveTile);
                    }

                    yield return new WaitForSeconds(0.1f);

                    foreach (var t in matchedTiles)
                    {
                        if (t != null)
                        {
                            lsTilebaseSelected.Remove(t);
                            SkillManager.instance.tilePosTracking.Remove(t.name);
                            t.transform.DOKill();
                            Destroy(t.gameObject);
                        }
                    }

                    lsTilebaseClicked.RemoveAll(t => matchedTiles.Contains(t));
                    FillTile();
                    yield return new WaitForSeconds(0.1f);
                    yield break;
                }
            }
        }
    }
    public bool CanMatchTriple()
    {
        var grouped = lsTilebaseClicked.GroupBy(t => t.id);
        foreach (var group in grouped)
        {
            if (group.Count() >= 3)
            {
                return true;
            }
        }
        return false;
    }
    protected void FillTile()
    {
        for (int i = 0; i < lsTilebaseClicked.Count; i++)
        {
            if (lsTilebaseClicked[i] != null)
            {
                lsTilebaseClicked[i].transform.DOMove(lsSlotSort[i].position, 0.15f);
            }
        }
    }
}
