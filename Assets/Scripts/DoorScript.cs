using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public GameObject front, back;
    public Animator doorOpen;
    public bool isBlue, isYellow, isRed;

    private GameObject player;
    private bool isOpen;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player Ship");
        anim = doorOpen.GetComponent<Animator>();
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Open(){
      if (isBlue && !player.GetComponent<PlayerInput>().hasBlue()) {
        doorOpen.GetComponent<AudioSource>().Play();
        return;
      } else if (isYellow && !player.GetComponent<PlayerInput>().hasYellow()) {
        doorOpen.GetComponent<AudioSource>().Play();
        return;
      } else if (isRed && !player.GetComponent<PlayerInput>().hasRed()) {
        doorOpen.GetComponent<AudioSource>().Play();
        return;
      }
      Debug.Log("OPENING DOOR");
      anim.Play("Open");
      isOpen = true;
      front.GetComponent<MeshCollider>().enabled = false;
      back.GetComponent<MeshCollider>().enabled = false;
      front.GetComponent<AudioSource>().Play();
      StartCoroutine(WaitForFiveSeconds());
    }

    public void Close(){
      Debug.Log("CLOSING DOOR");
      anim.Play("Close");
      isOpen = false;
      front.GetComponent<MeshCollider>().enabled = true;
      back.GetComponent<MeshCollider>().enabled = true;
      back.GetComponent<AudioSource>().Play();
    }

    IEnumerator WaitForFiveSeconds()
    {
        yield return new WaitForSeconds(5);
        Close();
    }
}
