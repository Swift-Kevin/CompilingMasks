using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaskCompiler : MonoBehaviour
{
    private MaskEntry currentMask;
    private float maxCompileTime;
    private float compileElapsed;
    [SerializeField] private MeshRenderer maskRenderer;
    [SerializeField] private MeshFilter maskFilter;
    
    [SerializeField] private Slider compilationSlider;
    [SerializeField] private TextMeshProUGUI labelCompPercent;
    [SerializeField] private MaskUXFeedback maskObjFeedback;
    
    public void Startup()
    {
        MaskDatabase.Instance.Init();
        GenerateNewCompilation();
    }

    void Update()
    {
        compileElapsed += (Time.deltaTime * GameManager.Instance.GetSimulationMultiplier());
        Debug.Log(GameManager.Instance.GetSimulationMultiplier());
        
        compilationSlider.value = compileElapsed;

        float percent = Mathf.Clamp01(compileElapsed / maxCompileTime) * 100f;
        labelCompPercent.text = percent.ToString("F0") + "%";

        if (compileElapsed >= maxCompileTime)
        {
            CompilationFinished();
        }

        float dissolvePct = Mathf.Clamp01(compileElapsed / maxCompileTime);
        float remapped = (dissolvePct * 1.5f) - 0.75f;
        maskObjFeedback.UpdateDissolve(remapped);
    }

    private void CompilationFinished()
    {
        float rewardPrice = currentMask.rewardPrice;
        rewardPrice = rewardPrice * GameManager.Instance.CurrencyModifier;
        
        GameManager.Instance.CurrencyResource.Increase(rewardPrice);
        
        // VERY LAST THING >>>
        GenerateNewCompilation();
    }
    
    private void GenerateNewCompilation()
    {
        currentMask = MaskDatabase.Instance.GetRandomMask();

        maxCompileTime = currentMask.compilationTime;
        compileElapsed = 0f;

        compilationSlider.maxValue = maxCompileTime;
        compilationSlider.value = 0f;

        maskFilter.mesh = currentMask.model;
        maskRenderer.sharedMaterial = currentMask.mat;
    }
}
