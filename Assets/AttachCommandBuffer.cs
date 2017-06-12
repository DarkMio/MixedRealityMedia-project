using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AttachCommandBuffer : MonoBehaviour {

    private CommandBuffer buf;
    private Camera cam;
    // public Material alphaMap;
    public RenderTexture alphaMap;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        if (!cam) {
            Debug.LogError("No camera to attach Commandbuffer to");
            return;
        }
        buf = new CommandBuffer();
        buf.name = "Hopefully the right buffer";
        buf.Blit(BuiltinRenderTextureType.CurrentActive, alphaMap);
        cam.AddCommandBuffer(CameraEvent.AfterForwardAlpha, buf);
    }

    void OnDestroy() {
        cam.RemoveCommandBuffer(CameraEvent.AfterForwardAlpha, buf);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
