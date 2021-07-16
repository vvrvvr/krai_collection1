using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMusicTrigger : MonoBehaviour
{
    [SerializeField] private MusicBox _musicBox;
    [FMODUnity.EventRef] [SerializeField] protected string _melody;

    private void Update()
    {
        var currentRpm = _musicBox.GetEngineEvenTRpm();
       // Debug.Log(currentRpm);
        if(currentRpm > 0.09f)
        {
            Debug.Log("here");
            _musicBox.PlayScream(_melody, 1);
            Destroy(gameObject);
        }
    }
        
            
        
    
}
