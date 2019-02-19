using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropScript : MonoBehaviour
{
	Rigidbody rigidbody;
	PlayerScript player;

	VacuumScript vacuum;

	ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.Find("Player").GetComponent<PlayerScript>();
		rigidbody = gameObject.GetComponent<Rigidbody>();
		particles = gameObject.GetComponent<ParticleSystem>();
		particles.Pause();

		vacuum = GameObject.Find("Vacuum").GetComponent<VacuumScript>();
	}

	// Update is called once per frame
	void Update()
	{
		//When the player is holding space, and the drop is close enough to the fork, move towards
		//it and turn on its particle effect.
		if (player.moveTowardFork) {
			float distanceToPlayer = Vector3.Distance(transform.position, player.gameObject.transform.position);
			if (distanceToPlayer < 5) {
				if (!particles.isPlaying) {
					particles.Play();
				}

				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;

				Vector3 directionToPlayer = player.gameObject.transform.position - transform.position;
				directionToPlayer = directionToPlayer.normalized;
				transform.Translate(directionToPlayer * 2f * Time.deltaTime, Space.World);
			}
		}


		//If the drop if close enough to the vacuum, move towards it
		float distanceToVacuum = Vector3.Distance(transform.position, vacuum.gameObject.transform.position);
		if (distanceToVacuum < 3) {
			Vector3 directionToVacuum = vacuum.gameObject.transform.position - transform.position;
			directionToVacuum = directionToVacuum.normalized;
			transform.Translate(directionToVacuum * 2f * Time.deltaTime, Space.World);
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		//Destroy ourself if we collide with something with the "DropDestroyer" tag
		//The cat face trigger has this tag.
		if (other.gameObject.CompareTag("DropDestroyer")) {
			Destroy(gameObject);
		}
	}
}
