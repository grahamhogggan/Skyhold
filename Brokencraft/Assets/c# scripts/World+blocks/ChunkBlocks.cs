using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkBlocks : MonoBehaviour
{

    [SerializeField] private GameObject cube;
    private Chunk chunk;
    private static int ground = 10;
    private static int scale = 30;
    private static float size = 0.05f;
    public readonly static float waterLevel = 23;
    public readonly static float snowPeak = 33;

    private List<Vector2> trees = new List<Vector2>();
    // Start is called before the first frame update
    public void SpawnChunk(int seed)
    {
        
        chunk = transform.root.gameObject.GetComponent<Chunk>();
        for (int i = 0; i < Spawner.width; i++)
        {
            for (int k = 0; k < Spawner.length; k++)
            {
                float perlin = GetPerlinValue(i + chunk.GetLocation().x * 5, k + chunk.GetLocation().z * 5);
                int h = (int)perlin;
                for (int j = 0; j < Spawner.height; j++)
                {

                    Vector3 pos = new Vector3(i, j, k) + transform.position;
                    GameObject b = Instantiate(cube, pos, Quaternion.identity);
                    b.transform.SetParent(transform);
                    
                    Block spawned = b.GetComponent<Block>();
                    spawned.parentChunk = chunk;
                    spawned.localPos = pos-transform.position;
                    spawned.typeName = "dirt";
                    chunk.addBlock(b.GetComponent<Block>(), i, j, k);
                    int blockY = j + (int)((Spawner.instance.chunksY - chunk.GetLocation().y) * 5);
                    if (blockY > h)
                    {
                        if (blockY <= waterLevel)
                        {
                            spawned.SetBlock("water");
                        }
                        else
                        {
                            b.SetActive(false);
                        }

                    }
                    else
                    {
                        if(h<=waterLevel+1&&blockY>h-2)
                        {
                            spawned.SetBlock("sand");
                        }
                        else if (blockY >= snowPeak)
                        {
                            spawned.SetBlock("snow");
                        }
                        else if (blockY == h)
                        {
                            spawned.SetBlock("grass");

                        }
                    }
                    if(blockY>=h)
                    {
                        chunk.Expose();
                    }


                    if(blockY<ground)
                    {
                        if(Random.Range(0,50)==25)
                        {
                            spawned.SetBlock("metal");
                        }
                        else
                        {
                            spawned.SetBlock("stone");

                        }
                    }
                    
                }
            }
        }
    }
    public static float GetPerlinValue(float x, float y)
    {
        
        float perlin1 = Mathf.PerlinNoise(Spawner.seed + x * size, Spawner.seed + y * size) * scale;
        float perlin15 = Mathf.PerlinNoise(Mathf.Pow(Spawner.seed,0.7f) + x * size, Mathf.Pow(Spawner.seed,0.7f) + y * size) * scale;
        float perlin2 = Mathf.PerlinNoise(Mathf.Pow(Spawner.seed,0.5f) + x * size*2,Mathf.Pow(Spawner.seed,0.5f) + y * size*2) * scale*0.5f;
        return (perlin1+perlin2+perlin15)/2.5f+ground;
        //for building structures
        //return ground+20;
    }
}
