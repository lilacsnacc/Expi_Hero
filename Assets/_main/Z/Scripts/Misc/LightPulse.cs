using UnityEngine;

public class LightPulse : MonoBehaviour
{
  Color baseColor;

  void Start() {
    baseColor = GetComponent<Light>().color;
  }
  
  void Update() {
      GetComponent<Light>().color = baseColor * Mathf.Min(1.5f, (1.25f + Mathf.Sin(Time.time * Mathf.PI * 2) * 0.5f * Random.value));
  }
}
