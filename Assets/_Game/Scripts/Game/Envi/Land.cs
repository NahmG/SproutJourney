using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Land : GameUnit
{
    [SerializeField]
    LandData data;

    [SerializeField]
    LAND_TYPE landType;
    public LAND_TYPE LandType => landType;
    [SerializeField]
    int index;
    public int Index => index;

    [SerializeField]
    Cell cellPrefab;
    Cell[,] cells = new Cell[3, 3];

    [SerializeField]
    GameObject landView;

    public static Land CreateLand(Transform parent, LAND_TYPE landType, int index)
    {
        Land land = SimplePool.Spawn<Land>(PoolType.LAND, parent.position, Quaternion.identity, parent);
        land.landType = landType;
        land.index = index;

        land.Init(true);
        return land;
    }

    public static Land CreateLand(Land pref, Transform parent, LAND_TYPE landType, int index)
    {
        Land land = Instantiate(pref, parent);
        land.landType = landType;
        land.index = index;

        land.SetSkin();
        return land;
    }

    public void Init(bool playAnim = false)
    {
        GenerateCells();
        AddObstacle();
        SetSkin();

        if (playAnim)
            AnimInit();
    }

    public void OnDespawn(Action action = null)
    {
        AnimDespawn(() =>
        {
            DespawnImmediate();
            action?.Invoke();
        });
    }

    public void DespawnImmediate()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                Destroy(cells[x, y].gameObject);
            }
        }
        SimplePool.Despawn(this);
    }

    void GenerateCells()
    {
        Vector3 origin = new(-3 / 2, 0, -3 / 2f + .5f);
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                Cell cell = Instantiate(cellPrefab, TF);
                cell.Tf.localPosition = origin + new Vector3(x, 0, y);
                cell.OnInit(this);
                cells[x, y] = cell;
            }
        }
    }

    void AddObstacle()
    {
        int[,] pattern = data.GetPattern(index - 1);
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (pattern[x, y] == 1 && cells[x, y].IsEmpty)
                {
                    GameObject obsPref = data.GetObstaclePrefab((int)landType);
                    cells[x, y].SetObstacle(obsPref);
                }
            }
        }
    }

    void SetSkin()
    {
        if (landView != null)
        {
            Destroy(landView);
        }
        landView = Instantiate(data.GetLandPrefab((int)landType), SkinTF);
    }

    void AnimInit()
    {
        TF.DOScale(Vector3.one, 0.3f).From(Vector3.zero).SetEase(Ease.OutBack);
    }

    void AnimDespawn(Action action = null)
    {
        TF.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() => action?.Invoke());
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        string label = $"{Index}";
        if (Camera.current != null)
        {
            Vector3 screenPos = Camera.current.WorldToScreenPoint(transform.position);
            if (screenPos.z > 0)
            {
                Handles.BeginGUI();
                GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
                style.normal.textColor = Color.yellow;
                GUI.Label(new Rect(screenPos.x, Screen.height - screenPos.y, 100, 20), label, style);
                Handles.EndGUI();
            }
        }
    }

#endif
}
