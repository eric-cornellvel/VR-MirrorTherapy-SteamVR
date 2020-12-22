using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDetection : MonoBehaviour
{
    private bool alreadyTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision:" + other.gameObject);
        if (alreadyTrigger == false) {
            //spawner_script.log_data(this.transform.position);
            Vector3 pos = this.gameObject.transform.position;
            if (VRRig.Instance.vRType == Utils.VRType.Oculus)
            {
                InputManager.Instance.saveDataScript.LogSphereTouchedEvent(pos);
                InputManager.Instance.touchSound.Play();
            }
            else if(VRRig.Instance.vRType == Utils.VRType.SteamVR)
            {
                InputManager_SteamVR.Instance.saveDataScript.LogSphereTouchedEvent(pos);
                InputManager_SteamVR.Instance.touchSound.Play();
            }
            alreadyTrigger = true;
            
            Destroy(this.gameObject);
            VRRig.Instance.gameObject.GetComponent<SphereSpawner>().SpawnSphereAfter(3.0f);
           
        }

    }
}
