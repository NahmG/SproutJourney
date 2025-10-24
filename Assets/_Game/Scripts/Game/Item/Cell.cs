using UnityEngine;

public class Cell : MonoBehaviour
{
    public Transform Tf;
    public Transform obstacleTf;
    GameObject obstacle;

    Land land;
    public Land Land => land;

    bool isEmpty = true;
    public bool IsEmpty
    {
        get
        {
            isEmpty = obstacle == null;
            return isEmpty;
        }
    }

    public void OnInit(Land land)
    {
        this.land = land;
    }

    public void SetObstacle(GameObject obsPrefab)
    {
        Clear();

        obstacle = Instantiate(obsPrefab, obstacleTf);
        obstacle.transform.localPosition = Vector3.zero;
    }

    public void Clear()
    {
        if (obstacle != null)
        {
            Destroy(obstacle);
            obstacle = null;
        }
    }
}