using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SortController : MonoBehaviour
{
    [SerializeField] private List<Transform> lsSlotSort;
    // [SerializeField] private List<TilebaseController> lsTilebaseControllers;
    [SerializeField] private List<CountId> lsCountID;
    [SerializeField] private List<ParentTile> lsObjectParent;
    [SerializeField] public ParentTile parent;

    public void Init()
    {
        for (int i = 0; i < 8; i++)
        {
            lsCountID.Add(new CountId { iD = i });
        }
    }
    public void HandleParent(TilebaseController tileBaseParam)
    {
        if (lsObjectParent.Count >= 8)
        {
            return;
        }
        if (lsObjectParent.Count == 0)
        {
            var temp = Instantiate(parent);
            temp.iD = tileBaseParam.id;
            lsObjectParent.Add(temp);
            HandleSortParentPosition();
            tileBaseParam.GetComponent<Collider2D>().enabled = false;
            tileBaseParam.HandleMoveLocal(temp.transform, delegate { HandleCount(tileBaseParam.id); });
            return;
        }
        else
        {
            var temp = Instantiate(parent);
            temp.iD = tileBaseParam.id;
            bool isSort = false;
            int index = 0;
            for (int i = 0; i < lsObjectParent.Count; i++)
            {
                if (lsObjectParent[i].iD == tileBaseParam.id)
                {
                    isSort = true;
                    index = i;
                    break;
                }
            }
            if (isSort)
            {
                lsObjectParent.Insert(index, temp);
                HandleSortParentPosition();
                tileBaseParam.HandleMoveLocal(temp.transform, delegate { HandleCount(tileBaseParam.id); });
            }
            else
            {
                lsObjectParent.Add(temp);
                HandleSortParentPosition();
                tileBaseParam.HandleMoveLocal(temp.transform, delegate { HandleCount(tileBaseParam.id); });
            }
        }
        HandleSortParentPosition();
    }

    public void HandleCount(int id)
    {
        //foreach (var item in lsCountID)
        //{
        //    if (item.iD == id)
        //    {
        //        item.count += 1;
        //        if (item.count >= 3)
        //        {
        //            HandleDeleteTile(item.iD);
        //            item.count = 0;
        //        }
        //    }
        //}
        for (int i = 0; i < lsCountID.Count; i++)
        {
            if (lsCountID[i].iD == id)
            {
                lsCountID[i].count += 1;
                if (lsCountID[i].count >= 3)
                {
                    HandleDeleteTile(lsCountID[i].iD);
                    lsCountID[i].count = 0;
                }
            }
        }
    }

    public void HandleSortParentPosition()
    {
        for (int i = 0; i < lsObjectParent.Count; i++)
        {
            lsObjectParent[i].transform.position = lsSlotSort[i].transform.position;
        }
    }

    public void HandleDeleteTile(int iD)
    {
        for (int i = lsObjectParent.Count - 1; i >= 0; i--)
        {
            if (lsObjectParent[i].iD == iD)
            {
                Destroy(lsObjectParent[i].gameObject);
                lsObjectParent.RemoveAt(i);
            }
        }
        HandleSortParentPosition();
    }


    //public void HandleSortTile(TilebaseController tilebase)
    //{
    //    if(lsTilebaseControllers.Count >= 8)
    //    {
    //        return;
    //    }
    //    if (lsTilebaseControllers.Count == 0)
    //    {
    //        lsTilebaseControllers.Add(tilebase);
    //        HandleSortPosition();
    //        return;
    //    }
    //    else
    //    {
    //        bool isSort = false;
    //        int index = 0;
    //        for(int i = 0; i < lsTilebaseControllers.Count; i++)
    //        {
    //            if(lsTilebaseControllers[i].id == tilebase.id)
    //            {
    //                isSort = true;
    //                index = i;
    //                break;
    //            }
    //        }
    //        if(isSort)
    //        {
    //            lsTilebaseControllers.Insert(index, tilebase);
    //        }
    //        else
    //        {
    //            lsTilebaseControllers.Add(tilebase);
    //        }
    //    }
    //    HandleSortPosition();
    //}
    //public void HandleSortPosition()
    //{
    //    for(int i = 0; i < lsTilebaseControllers.Count; i++)
    //    {
    //        lsTilebaseControllers[i].transform.DOMove(lsSlotSort[i].transform.position, 0.5f);
    //    }
    //}
}

[System.Serializable]

public class CountId
{
    public int iD;
    public int count;
}

