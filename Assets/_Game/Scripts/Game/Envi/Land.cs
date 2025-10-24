using UnityEngine;

public class Land : GameUnit
{
    [SerializeField]
    LandData data;

    LAND_TYPE landType;
    public LAND_TYPE LandType => landType;
    int index;
    public int Index => index;

    [SerializeField]
    Cell cellPrefab;
    Cell[,] cells = new Cell[3, 3];

    GameObject landView;
    bool isInitCell = false;


    void Awake()
    {
        if (!isInitCell)
        {
            isInitCell = true;
            GenerateCells();
        }
    }

    public static Land CreateLand(Transform parent, LAND_TYPE landType, int index)
    {
        Land land = SimplePool.Spawn<Land>(PoolType.LAND, parent.position, Quaternion.identity, parent);
        land.landType = landType;
        land.index = index;

        land.Init();
        return land;
    }

    public static Land CreateLand(Land pref, Transform parent, LAND_TYPE landType, int index)
    {
        Land land = Instantiate(pref, parent);
        land.landType = landType;
        land.index = index;

        land.Init();
        return land;
    }

    public void Init()
    {
        if (!isInitCell)
        {
            isInitCell = true;
            GenerateCells();
        }
        AddObstacle();
        SetSkin();
    }

    public void Despawn()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (!cells[x, y].IsEmpty)
                {
                    cells[x, y].Clear();
                }
            }
        }
        SimplePool.Despawn(this);
    }

    public void SpawnNextLand()
    {
        Debug.Log("Spawn Next Land");
        switch (landType)
        {
            case LAND_TYPE.GRASS:

                break;
            case LAND_TYPE.SNOW:

                break;
            case LAND_TYPE.SAND:

                break;
            case LAND_TYPE.VOLCANIC:

                break;
        }
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
}
