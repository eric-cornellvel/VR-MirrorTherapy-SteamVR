using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class InputManager_SteamVR : MonoBehaviour
{

    public GameObject controllerModelLeft;
    public GameObject controllerModelRight;
    public GameObject leftHandIKTarget;
    public GameObject rightHandIKTarget;
    public GameObject mirrorLeftIKTarget;
    public GameObject mirrorRightIKTarget;
    public GameObject settingUICanvas;
    public GameObject debugMirror;
    public Button calibrateBtn;
    public Button mirrorNone;
    public Button mirrorLeft;
    public Button mirrorRight;
    public Button startBtn;
    public Button stopBtn;
    public Button maleBtn;
    public Button femaleBtn;
    public SphereSpawner sphereSpawner;
    public SaveData saveDataScript;

    public AudioSource touchSound;
    public TextMeshProUGUI debugText;

    private Quaternion leftHandRot = Quaternion.Euler(-90, 0, -90);
    private Quaternion rightHandRot = Quaternion.Euler(90, 0, 90);
    private Quaternion leftControllerRot = Quaternion.Euler(0, 90, 90);
    private Quaternion rightControllerRot = Quaternion.Euler(0, -90, -90);

    public static InputManager_SteamVR Instance; //singleton variable

    private void Awake()
    {
        if (Instance == null) Instance = this; //store singleton
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null; //delete singelton
    }

    // Start is called before the first frame update
    void Start()
    {
        calibrateBtn.onClick.AddListener(VRRig.Instance.Calibration);
        mirrorNone.onClick.AddListener(() => VRRig.Instance.changeMirror(VRRig.MirrorMode.None));
        mirrorLeft.onClick.AddListener(() => VRRig.Instance.changeMirror(VRRig.MirrorMode.MirrorLeft));
        mirrorRight.onClick.AddListener(() => VRRig.Instance.changeMirror(VRRig.MirrorMode.MirrorRight));
        startBtn.onClick.AddListener(StartBtnClickHandler);
        maleBtn.onClick.AddListener(() => VRRig.Instance.LoadAvatarBones(VRRig.AvatarGender.Male));
        femaleBtn.onClick.AddListener(() => VRRig.Instance.LoadAvatarBones(VRRig.AvatarGender.Female));
        stopBtn.onClick.AddListener(StopRecording);
    }

    public void HideControlelr()
    {
        //TODO-find controller models
        controllerModelLeft.SetActive(false);
        controllerModelRight.SetActive(false);

    }

    public void StartBtnClickHandler()
    {
        saveDataScript.StartRecording();
        sphereSpawner.SpawnSphere();

        //TODO-figure out how to use action-based input
        settingUICanvas.SetActive(false);
        debugMirror.SetActive(false);

        //TODO-hide pointer
        Utils.getLeftController(VRRig.Instance.vRType).GetComponent<XRInteractorLineVisual>().enabled = false;
        Utils.getRightController(VRRig.Instance.vRType).GetComponent<XRInteractorLineVisual>().enabled = false;
    }

    public void StopRecording()
    {
        saveDataScript.StopRecording();
    }

    private void DoCalibration()
    {
        VRRig.Instance.Calibration();
        //handModelLeft.gameObject.layer = LayerMask.NameToLayer("HideFromCam");
        //handModelRight.gameObject.layer = LayerMask.NameToLayer("HideFromCam");
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnApplicationQuit()
    {
        StopRecording();
    }
}
