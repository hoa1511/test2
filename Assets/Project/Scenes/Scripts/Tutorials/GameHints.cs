using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameHints : MonoBehaviour, ICanPlaySound
{

    [SerializeField] private AudioClip audioClip;
    private bool hasLoadScene = false;
    
    Player player;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            if(hasLoadScene == false)
            player = other.GetComponent<Player>();
            if(player.pickedKey == false)
            {
                PlaySound(audioClip);
                Manager.Add(HintsSceneController.HINTSSCENE_SCENE_NAME);
                hasLoadScene = true;
            }
        }
    }
}
