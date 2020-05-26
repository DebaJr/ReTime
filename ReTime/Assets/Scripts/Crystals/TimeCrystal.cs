using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCrystal : MonoBehaviour
{
    [Tooltip("Prefab of the time shards instantiated when colliding with crystal")][SerializeField] GameObject timeShardPrefab;
    [Tooltip("Number of shards to be instanciated")][SerializeField] int numShardsToInstantiate;
    [Tooltip("Minimum force to be applied on shards after explosion")][SerializeField] float minExplosionForce = 3f;
    [Tooltip("Maximum force to be applied on shards after explosion")][SerializeField] float maxExplosionForce = 5f;
    [Tooltip("ParticleSystem to be played when crystal hit by player")][SerializeField] ParticleSystem crystalExplosionParticles;
    AudioSource audioSource;
    float waitUntilDestroy;

    Material thisCrystalMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        thisCrystalMaterial = GetComponent<Renderer>().material;
        waitUntilDestroy = audioSource.clip.length > crystalExplosionParticles.main.duration ? audioSource.clip.length + 0.1f : crystalExplosionParticles.main.duration + 0.1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BreakCrystal();
            StartCoroutine(SelfDestruct());
        }    
    }

    void BreakCrystal()
    {
        audioSource.Play();
        ParticleSystem particles = Instantiate(crystalExplosionParticles, transform.position, Quaternion.identity);
        particles.GetComponent<ParticleSystemRenderer>().material = thisCrystalMaterial;
        particles.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        for(int i=0; i < numShardsToInstantiate; i++)
        {
            InstantiateShard();
        }
    }

    void InstantiateShard()
    {
        // Calculate shard instantiation position as one unit above crystal origin (in this case, its base) considering the direction the crystal is pointing
        Vector3 _shardPosition = transform.position + transform.up.normalized;
        //Instantiate the shard without no rotation
        GameObject _timeShard = Instantiate(timeShardPrefab, _shardPosition, Quaternion.Euler(0f,0f,0f)) as GameObject;
        //set the shards material the same as the crystal;
        _timeShard.GetComponentInChildren<Renderer>().material = thisCrystalMaterial;
        //disable shard collider so it wont be picked up in middle air right after being instantiated or collide with other shards/player
        _timeShard.GetComponent<Collider>().enabled = false;
        //calculate the direction in which the shard will fly off (adds diversity)
        Vector3 _shardDirection = CalculateShardDirection();
        //calculate a random speed for the flight of each shard (adds diversity)
        float randomSpeed = Random.Range(minExplosionForce, maxExplosionForce);
        //shoots the shard away from crystal (uses rigidbody and engine phisics >> ShardGravityController Script disables gravity after shards reach the ground) 
        _timeShard.GetComponent<Rigidbody>().AddForce(_shardDirection * randomSpeed, ForceMode.Impulse);
    }

    Vector3 CalculateShardDirection()
    {
        
        //calculate a direction in relation to crystals "Up" and randominzing XY deviation
        //this can shoot shards upwards in all direction randomly
        //the direction is a vector of minimun size 1 and maximun size sqrt(3) aprox 1.71. 
        Vector3 direction = transform.up.normalized + new Vector3 (Random.Range(-1f,1f), 0f, Random.Range(-1f,1f));
        //normalize makes the size to be 1, so only speed will influence the distance travelled by the shard
        return direction.normalized;
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(waitUntilDestroy); 
        Destroy(gameObject);
    }
}
