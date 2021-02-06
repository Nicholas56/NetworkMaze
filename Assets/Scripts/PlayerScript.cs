using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class PlayerScript : NetworkBehaviour
{
    [Header("Components")]
    public NavMeshAgent agent;

    [Header("Movement")]
    public float rotationSpeed = 100;

    [Header("Firing")]
    public KeyCode shootKey = KeyCode.Space;
    public GameObject projectilePrefab;
    public Transform projectileMount;

    Transform StartPoints;
    int spawn = 0;

    [SyncVar] public int thisPlayerScore;
    [SyncVar] public string thisPlayerName;
    ScoreScript score;

    public override void OnStartServer()
    {
        base.OnStartServer();
    }
    void Start()
    {
        if (isLocalPlayer)
        {
            //loseTxt = GameObject.Find("loseTxt").GetComponent<TMP_Text>();
            CameraFollow360.player = transform;
            CameraTopDown.player = transform;
            StartPoints = GameObject.Find("StartPositions").transform;
            score = FindObjectOfType<ScoreScript>();
            score.AddIdentity(GetComponent<NetworkIdentity>().netId);
        }
    }
    void FixedUpdate()
    {
        // movement for local player
        if (!isLocalPlayer) return;

        if (Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical")!=0)
        {
            float horInput = Input.GetAxis("Horizontal");
            float verInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horInput, 0f, verInput);
            Vector3 moveDestination = transform.position + movement;
            agent.SetDestination(moveDestination);
        }
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
        
        // shoot
        if (Input.GetKeyDown(shootKey) || Input.GetMouseButtonDown(1))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.transform.tag == "Switch")
                {
                    hitCollider.transform.GetComponent<SwitchScript>().MazeAction();
                    return;
                }
            }
            CmdFire();
        }
    }

    [Command]
    void CmdUpdateScore(int newScore)
    {
        thisPlayerScore += newScore;
        RpcOnScore();
    }
    // this is called on the server
    [Command]
    void CmdFire()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileMount.position, transform.rotation);
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
        NetworkServer.Spawn(projectile);
    }


    void RpcOnScore()
    {
        if(isLocalPlayer)
        score.WriteScore(GetComponent<NetworkIdentity>().netId, thisPlayerScore);
    }

    public void Respawn()
    {
        spawn++;
        if (spawn == StartPoints.childCount) { spawn = 0; }
        transform.position = StartPoints.GetChild(spawn).position;
        agent.destination=this.transform.position;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (isLocalPlayer && collision.gameObject.tag == "Token")
        {
            CmdUpdateScore(1);
            FindObjectOfType<SpawnTokenScript>().SpawnToken();
        }
        if (isLocalPlayer && collision.gameObject.tag == "Projectile")
        {
            Respawn();
        }
    }
}
