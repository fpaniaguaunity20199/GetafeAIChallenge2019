using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PadillaVan : SimpleVan
{
    private readonly Vector3 LIMIT_LOWER = new Vector3(-50, 0, -50);
    private readonly Vector3 LIMIT_UPPER = new Vector3(50, 0, 50);
    private const string DESTINATION = "Destination";
    private const string DESTINATION_CLONE = "Destination(Clone)";

    private Transform lastObstacle;
    private Rigidbody rb;
    private float avoidingClock;
    private float findingClock;
    private enum State { avoiding, onTrack }
    private State state = State.onTrack;

    private Vector3 nextDestination;
    private float angleToDestination;
    public float timeAvoidingObstacle = 2;
    public float timeFindingDestination = 10;
    [Range(0f, 5f)]
    [SerializeField] float timeSavePositions = 0.5f;

    public List<Vector2> positions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private new void Start()
    {
        base.Start();
        GetRandomDestination();
        StartCoroutine(SavePosition());
        avoidingClock = timeAvoidingObstacle;
        findingClock = timeFindingDestination;
    }
    public override void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (state == State.onTrack)
        {
            findingClock -= Time.deltaTime;
            FaceDestination();
            if(findingClock <= 0)
            {
                GetRandomDestination();
            }
        }
        if (state == State.avoiding)
        {
            avoidingClock -= Time.deltaTime;
            AvoidObstacle(lastObstacle);
        }
    }
    private void FaceDestination()
    {
        if(Mathf.Sign(speed) == -1)
        {
            speed = -speed;
        }
        Vector3 vector = transform.position - nextDestination;
        angleToDestination = Vector3.SignedAngle(vector, transform.forward, Vector3.up);
        Vector3 direction;
        if(Mathf.Sign(angleToDestination) == 1)
        {
            direction = new Vector3(0, angularSpeed, 0);
        }
        else
        {
            direction = new Vector3(0, -angularSpeed, 0);
        }
        if (Mathf.Abs(angleToDestination) < 178)
        {
            transform.Rotate(direction * Time.deltaTime);
            speed = 5;
        }
        else
        {
            speed = 10;
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        print(other.gameObject.tag);
        print(other.gameObject.name);
        if (other.gameObject.name == DESTINATION_CLONE)
        {
            GetRandomDestination();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag(TAG_PARED))
        {
            state = State.avoiding;
            speed = -speed;
            lastObstacle = other.transform;
        //}else if (other.gameObject.CompareTag(TAG_TARGET))
        //{
        //    GetComponent<AudioSource>().Play();
        //    textMeshPlayerName.text = playerName + " WINS";
        //    textMeshPlayerName.color = WIN_COLOR;
        //    textMeshPlayerName.characterSize *= 2f;
        //    Time.timeScale = 0;
        }
    }

    private void GetRandomDestination()
    {
        findingClock = timeFindingDestination;
        int x = Random.Range((int)LIMIT_LOWER.x, (int)LIMIT_UPPER.x);
        int z = Random.Range((int)LIMIT_LOWER.z, (int)LIMIT_UPPER.z);
        Vector2 newDestination = new Vector2(x, z);
        CompareRandomDestination(newDestination);
    }
    private void CompareRandomDestination(Vector2 comparedDestination)
    {
        if (!positions.Contains(comparedDestination))
        {
            nextDestination = new Vector3(comparedDestination.x, 0, comparedDestination.y);
            GameObject target = Instantiate(new GameObject(DESTINATION), nextDestination, Quaternion.Euler(0, 0, 0));
            target.AddComponent<SphereCollider>();
            target.GetComponent<SphereCollider>().isTrigger = true;
            target.GetComponent<SphereCollider>().radius = 4;
        }
        else
        {
            GetRandomDestination();
        }
    }
    private void AvoidObstacle(Transform obstacle)
    {
        float leftRight;
        leftRight = Mathf.Sign(Vector3.SignedAngle(transform.position - obstacle.position, transform.forward, Vector3.up));
        if (Mathf.Sign(speed) == 1)
        {
            speed = Random.Range(5, 10);
        }
        else
        {
            speed = Random.Range(-10, -5);
        }
        if (avoidingClock > 0)
        {
            if (leftRight == -1)
            {   
                if(avoidingClock > timeAvoidingObstacle*0.5)
                {
                    Vector3 direction = new Vector3(0, angularSpeed, 0);
                    transform.Rotate(direction * Time.deltaTime);
                }
                else
                {
                    speed = 10;
                }
            }
            else if (leftRight == 1)
            {
                if(avoidingClock > timeAvoidingObstacle * 0.5)
                {
                    Vector3 direction = new Vector3(0, -angularSpeed, 0);
                    transform.Rotate(direction * Time.deltaTime);
                }
                else
                {
                    speed = 10;
                }
            }
        }
        else
        {
            state = State.onTrack;
            avoidingClock = timeAvoidingObstacle;
        }
    }
    IEnumerator SavePosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeSavePositions);
            positions.Add(new Vector2((int)transform.position.x, (int)transform.position.z));
        }
    }
}
    
