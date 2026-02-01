using UnityEngine;

public class MaskUXFeedback : MonoBehaviour
{
    private Material dissolveMat;
    [SerializeField] private MeshRenderer rend;
    
    [Header("Bobbing")]
    [SerializeField, Range(0, 5)] float bobRate = 1;
    [SerializeField, Range(0, 2)] float amplitude = 0.25f;
    [SerializeField, Range(0.1f, 5)] float rotateSpeed = 0.75f;

    Vector3 startingPos;
    float tempNum;

    private void Start()
    {
        startingPos = transform.position;
        dissolveMat = rend.material;
    }
    
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, startingPos.y + Mathf.Sin(Time.time * bobRate) * amplitude, transform.position.z);
        transform.Rotate(0, rotateSpeed, 0);
    }

    public void BindMaterial()
    {
        dissolveMat = rend.material;
    }

    public void UpdateDissolve(float amt)
    {
        if (dissolveMat != null)
            dissolveMat.SetFloat("_CutoffHeight", amt);
    }
}
