using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    [SerializeField] float xp;
    public Cell core;
    public Camera playerCamera;
    public Transform backGround;
    public GameObject gameOver;
    [SerializeField] Animator animator;
    public bool isDestroing = false;
    public AudioManager audioManager;
    public GameObject shieldFront;
    public GameObject shieldBack;
    public List<Sprite> armorSprites;


    void Update()
    {
        if (core.isDestroyed)
        {
            gameOver.SetActive(true);
            audioManager.SoundPlay0();
            gameObject.SetActive(false);
            GameObject.Find("ButtonPause").SetActive(false);
            Time.timeScale = 0;
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsMoving", false);
        }
    }
    void FixedUpdate()
    {
        Moving();
    }
    void Moving()
    {
        var oldPosition = transform.position;

        if (Input.GetMouseButton(0))
        {
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            objPosition.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, objPosition, speed * Time.deltaTime);

            Vector3 direction = (objPosition - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), 5 * Time.deltaTime);

            Vector3 deltaMovement = oldPosition - transform.position;
            backGround.position = new Vector2(backGround.position.x + deltaMovement.x * 0.12f, backGround.position.y + deltaMovement.y * 0.12f);
            if (!animator.GetBool("IsMoving"))
            {
                animator.SetBool("IsMoving", true);
            }
            if(!audioManager.a.isPlaying)audioManager.SoundPlay1();
        }
        else if (Input.GetMouseButton(1))
        {
            audioManager.a.Stop();
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            objPosition.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, objPosition, 2 * Time.deltaTime);// speed/2

            Vector2 direction = (objPosition - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle + 90), 5 * Time.deltaTime);

            Vector3 deltaMovement = oldPosition - transform.position;
            backGround.position = new Vector2(backGround.position.x + deltaMovement.x * 0.06f, backGround.position.y + deltaMovement.y * 0.06f);
        }
        else audioManager.a.Stop();
    }
}
