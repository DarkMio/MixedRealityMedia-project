using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class RenderBufferSwapper : MonoBehaviour {
    /*

    public List<RenderTexture> buffers;

	public int index;
	public long seenFrames;
	public Material cameraViewMaterial;
    public RawImage rawOutputDisplay;
    */ 
    [RangeAttribute(15, 60)]
    public float cameraFPS;
    [RangeAttribute(0, 1)]
    public float cameraOffset;
    public Material targetMaterial;
    public Camera observerCamera;
    public Camera frontCamera;


    private float frameWindow;
    private float delay;
    private int IntDelay { get { return (int)delay;} }
    private float frameDelay;
    private int IntFrameDelay { get { return (int)frameDelay;} }
    private float fractionDelay;
    private float innerTimer;
    private float absoluteTimer;
    private float initialDelay;
    public List<RenderTexture> colorBuffers;
    public List<RenderTexture> alphaBuffers;
    public int index;


    void Start () {
        frameWindow = 1.0f / cameraFPS;
        delay = cameraOffset / frameWindow;
        frameDelay = (int)delay * frameWindow;
        fractionDelay = delay % 1 * frameWindow;

        innerTimer = 0.0f;
        absoluteTimer = 0.0f;
        initialDelay = frameDelay + fractionDelay;

		RebuildRenderBuffers();
	}
	
	void Update () {
        innerTimer += Time.deltaTime;
        absoluteTimer += Time.deltaTime;
        var localTime = innerTimer - fractionDelay;
        if(localTime < frameWindow || absoluteTimer < initialDelay) {
            return;
        }

        SwapRenderBuffer();
        innerTimer %= frameWindow;
        absoluteTimer = absoluteTimer % (2f) + initialDelay;
	}

    void SwapRenderBuffer() {
        index = index % IntDelay;
        //  observerCamera.SetTargetBuffers(colorBuffers[index].colorBuffer, depthBuffers[index].depthBuffer);
        observerCamera.targetTexture = colorBuffers[index];
        frontCamera.targetTexture = alphaBuffers[index];
        var frameTex = colorBuffers[(index + 1) % IntDelay];
        var alphaTex = alphaBuffers[(index + 1) % IntDelay];
        targetMaterial.SetTexture("_MainTex", frameTex);
        targetMaterial.SetTexture("_AlphaTex", alphaTex);
        index++;
    }

	void RebuildRenderBuffers() {
        colorBuffers = new List<RenderTexture>();
        alphaBuffers = new List<RenderTexture>();
		for(int i = 0; i < IntDelay; i++) {
            var cBuf = new RenderTexture(Screen.width, Screen.height, 32, RenderTextureFormat.ARGB32);
            // cBuf.antiAliasing = QualitySettings.antiAliasing;
            colorBuffers.Add(cBuf);

            var aBuf = new RenderTexture(Screen.width, Screen.height, 32, RenderTextureFormat.ARGB32);
            // aBuf.antiAliasing = QualitySettings.antiAliasing;
            alphaBuffers.Add(aBuf);
		}
		Debug.Log("Rebuilt buffers");
	}
}
