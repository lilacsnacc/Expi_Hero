using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    //stats
    private int str = 1;
    private float spd = 1f;
    private int vit = 1;
    public int expi;
    //stat modifiers
    public int healthBase;
    public int healthGainPerPoint;
    public int spdMod;
    
    //Gets and Sets
    public int Str { get => str; set => str = value; }
    public float Spd { get => spd; set => spd = value; }
    public int Vit { get => vit; set => vit = value; }
    public int Expi { get => expi; set => expi = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
