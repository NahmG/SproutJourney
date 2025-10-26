using UnityEngine;

public class Cell : MonoBehaviour
{
    public Transform Tf;
    [SerializeField]
    Transform obstacleTf;
    GameObject obstacle;
    Land land;
    public Land Land => land;

    public bool IsEmpty => obstacle == null;

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

    public static bool IsValid(Vector3 start, Vector3 dir, LayerMask layer, out Cell cell)
    {
        cell = null;
        Vector3 origin = start + dir;

        if (Physics.Raycast(origin + Vector3.up * 1f, Vector3.down, out RaycastHit hit, 10f, layer))
        {
            cell = hit.collider.GetComponent<Cell>();
            return cell != null && cell.IsEmpty;
        }

        return false;
    }
}