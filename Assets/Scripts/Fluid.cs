using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathReader;
public class Fluid : MonoBehaviour
{


    public static Fluid singleton;

    public float horizontalLimit = 10f;
    public float verticalLimit = 8f;

    private Vector4 rect;

    public int particlePerGenerator = 10;
    public float generatorPlacement = 0.5f;
    public float particleLifeTime = 10;


    public FluidGenerator generatorPrefab;
    public Material particleMaterial;

    List<FluidGenerator> generators = new List<FluidGenerator>();

    List<KeyValuePair<Material, float>> particleMaterials = new List<KeyValuePair<Material, float>>();
    public int spawned = 0;


    public MathEquation horizontalMathEquation, verticalMathEquation;

    public string horizontalEquation = "1";
    public string verticalEquation = "1";
    [HideInInspector]
    public string horizontalCalculatedEquation, verticalCalculatedEquation;

    private void Awake()
    {
        singleton = this;
        UpdateEquations();
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = new Vector4(-horizontalLimit, -verticalLimit, horizontalLimit, verticalLimit);

        SpawnGenerators();
        int generatorCount = 0;
        int particleCount = 0;
        foreach (FluidGenerator generator in generators)
        {
            generatorCount++;
            particleCount += generator.particles.Count;
        }
        Debug.Log("Generator Count: " + generatorCount);
        Debug.Log("Particle Count: " + particleCount);
    }

    void SpawnGenerators()
    {
        for (int i = 0; i < particlePerGenerator; i++)
        {
            particleMaterials.Add(new KeyValuePair<Material, float>(new Material(particleMaterial), 0f));
        }
        for (float x = rect.x; x <= rect.z; x += generatorPlacement)
        {
            for (float y = rect.y; y <= rect.w; y += generatorPlacement)
            {
                Vector2 pos = new Vector2(x, y);
                FluidGenerator generator = Instantiate<FluidGenerator>(generatorPrefab, transform.position + (Vector3)pos, Quaternion.identity, transform);
                generator.SpawnParticles(particleMaterials.ConvertAll(pair => pair.Key));
                generators.Add(generator);
            }
        }
        StartCoroutine(ActivateParticles());
    }

    IEnumerator ActivateParticles()
    {
        for (spawned = 0; spawned < particlePerGenerator;)
        {
            spawned++;
            yield return new WaitForSeconds(particleLifeTime / particlePerGenerator);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFluid();
    }


    public void UpdateFluid()
    {
        horizontalMathEquation.time = Time.time;
        verticalMathEquation.time = Time.time;
        for (int i = 0; i < spawned; i++)
        {
            //multithread this
            foreach (FluidGenerator generator in generators)
                generator.UpdateParticle(i);


            KeyValuePair<Material, float> pair = particleMaterials[i];
            float newLivedFor = pair.Value + Time.deltaTime;
            if (newLivedFor >= particleLifeTime)
            {
                newLivedFor = 0;
                foreach (FluidGenerator generator in generators)
                    generator.ResetParticle(i);
            }
            UpdateMaterial(pair.Key, newLivedFor);
            particleMaterials[i] = new KeyValuePair<Material, float>(pair.Key, newLivedFor);
        }
    }

    public void UpdateMaterial(Material mat, float livedFor)
    {
        float divided = livedFor / particleLifeTime;
        float alpha = 1 - Mathf.Pow(Mathf.Abs((divided - 0.5f) * 2), 1 / 3f);
        mat.SetFloat("_AlphaMultiplier", alpha);
    }

    public Vector2 VectorField(FluidParticle particle)
    {
        Vector2 position = particle.worldPosition;
        Vector2 diff = position - particle.spawnPosition;
        float angle = Mathf.Atan2(position.y, position.x);
        float magnitude = position.magnitude;

        horizontalMathEquation.x = position.x;
        horizontalMathEquation.y = position.y;
        horizontalMathEquation.dx = diff.x;
        horizontalMathEquation.dy = diff.y;
        horizontalMathEquation.angle = angle;
        horizontalMathEquation.r = magnitude;

        verticalMathEquation.x = position.x;
        verticalMathEquation.y = position.y;
        verticalMathEquation.dx = diff.x;
        verticalMathEquation.dy = diff.y;
        verticalMathEquation.angle = angle;
        verticalMathEquation.r = magnitude;

        return new Vector2(horizontalMathEquation.GetResult(), verticalMathEquation.GetResult());
    }


    public bool UpdateEquations()
    {

        MathEquation tempEquation = new MathEquation(horizontalEquation);
        bool updateHorizontal = tempEquation.Validate();
        if (!updateHorizontal)
            Debug.Log("Error in horizontal");
        MathEquation tempEquation2 = new MathEquation(verticalEquation);
        bool updateVertical = tempEquation2.Validate();
        if (!updateVertical)
            Debug.Log("Error in vertical");
        if (updateHorizontal && updateVertical)
        {
            verticalMathEquation = tempEquation2;
            verticalCalculatedEquation = verticalMathEquation.AsString();
            horizontalMathEquation = tempEquation;
            horizontalCalculatedEquation = horizontalMathEquation.AsString();
            return true;
        }
        return false;
    }

    /*public float x, y;


	public string result;


	public void Calculate() {
		horizontalMathEquation.x = x;
		horizontalMathEquation.y = y;
		horizontalMathEquation.time = Time.time;
		result = horizontalMathEquation.GetResult() + "";
	}
	*/
}
