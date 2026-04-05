using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TrashCarMovement : MonoBehaviour
{
    [SerializeField] private LevelControl levelControl;
    private float movementSpeed = 25f;
    private float rotationSpeed = 5f;
    private Vector3 target;
    private bool isMove = false;

    private List<Vector3> points = new List<Vector3>();
    private int curIndex = 0;
    private Rigidbody rb;
    private float stoppingDistance = 0.2f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        points.Add(new Vector3(-20f, 0.1f, 52f));
        points.Add(new Vector3(52f, 0.1f, 52f));
        points.Add(new Vector3(52f, 0.1f, -52f));
        points.Add(new Vector3(-20f, 0.1f, -52f));
    }
    // Start is called before the first frame update
    void Start()
    {
        isMove = true;
        target = points[curIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            // ѕровер€ем, достигли ли мы текущей точки
            Vector2 pos = new Vector2(transform.position.x, transform.position.z);
            Vector2 tg = new Vector2(target.x, target.z);
            //if (Vector3.Distance(transform.position, target) < stoppingDistance)
            if (Vector3.Distance(pos, tg) < stoppingDistance)
            {
                NextPoint();
            }
            else
            {
                // ѕоворачиваем в сторону следующей точки
                LookAtWaypoint();

                // ѕеремещаем врага к текущей точке
                MoveTowardsWaypoint();
            }
        }
    }
    private void LookAtWaypoint()
    {
        // ѕоворачиваем врага в сторону следующей точки
        Vector3 dir = target - transform.position; dir.y = 0f;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotationSpeed * Time.deltaTime);
    }

    private void MoveTowardsWaypoint()
    {
        // ѕеремещаем врага к текущей точке
        Vector3 dir = target - transform.position; dir.y = 0f;
        rb.MovePosition(transform.position + dir.normalized * movementSpeed * Time.deltaTime);
    }

    private void NextPoint()
    {
        if (curIndex < points.Count)
        {
            target = points[curIndex];
            curIndex++;
        }
        else
        {
            curIndex = 0;
            //isMove = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TrashCan"))
        {
            isMove = false;
            Invoke("StartMovement", 5f);
            TrashCanPoint tcp = other.gameObject.GetComponent<TrashCanPoint>();
            if (tcp != null)
            {
                Vector3 target = transform.position;
                target.y += 1.5f;
                target.z -= 1f;
                GameObject bag;
                do
                {
                    bag = tcp.CanteinerControl.GetTrashBag();
                    if (bag != null)
                    {
                        GarbageBag bagCntr = bag.GetComponent<GarbageBag>();
                        bagCntr.SetTarget(target);
                        if (levelControl != null) levelControl.ChangeMany(-bagCntr.Price);
                    }
                } while (bag != null);
            }
        }
    }

    private void StartMovement()
    {
        isMove = true;
    }
}
