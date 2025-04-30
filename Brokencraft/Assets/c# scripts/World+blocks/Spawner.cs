using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject chunk;


    public int chunksX;
    public int chunksY;
    public int chunksZ;

    public static int width = 5;
    public static int height = 5;
    public static int length = 5;

    [SerializeField] private int atmosphereLayers;
    private Chunk[] world;

    public static int seed = 314;
    public static Spawner instance;

    public Miner PlayerMinerComponent;
    public static TMP_Text loadingText;
    [SerializeField] private LoadingPanel loader;

    public static bool finishedLoading;

    private string savePath;
    public Camera camera;
    public Camera loadingCam;

    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        finishedLoading = false;
        instance = this;
        world = new Chunk[chunksX * chunksY * chunksZ];
        PlayerMinerComponent.Start();
        savePath = Overlord.saveDirectory+"/worldInfo.chicken";
        CreateChunks();
        
    }

    void CreateChunks()
    {
        StartCoroutine(GenerateWorld());
    }
    public Chunk GetChunk(int x, int y, int z)
    {
        if(x+chunksX*y+chunksY*chunksX*z<world.Length&&x+chunksX*y+chunksY*chunksX*z>=0)
        {
            //Debug.Log("index value: "+(x+chunksX*y+chunksY*chunksX*z));
             return world[x+chunksX*y+chunksY*chunksX*z];
        }
        else return null;
       
    }
    public void setChunk(Chunk c, int x, int y, int z)
    {
        world[x + chunksX * y + chunksY * chunksX * z] = c;
    }
    public Block GetBlock(int x, int y, int z)
    {
        //Debug.Log("requested Block Y: "+y);
        //Debug.Log("Requested Chunk Y of "+(chunksY-(int)(y / 5)));
        Chunk chunk = GetChunk((int)(x / 5), chunksY-(int)(y / 5), (int)(z / 5));
        if(chunk == null) return null;
        int rX = x % 5;
        int rY = y % 5;
        int rZ = z % 5;
        Block b = chunk.GetBlock(rX, rY, rZ);
        //Debug.Log(b.gameObject.name);
        return b;

    }

    IEnumerator GenerateWorld()
    {
        camera.enabled = false;
        loadingCam.enabled = true;
        if(Overlord.OverwriteMode)
        {
            if(SeedInputBox.seedChosen == 0)
            {
            Random.InitState((int)(Time.deltaTime*Time.time*Random.Range(0,10000)));
            seed = Random.Range(100,100000);
            }
            else
            {
                seed = SeedInputBox.seedChosen;
            }
            File.WriteAllText(savePath,seed.ToString());
        }
        else
        {
            seed = int.Parse(File.ReadAllText(savePath));
        }
        //Debug.Log("Generating . . .");
        Random.InitState(seed);

        for (int i = 0; i < chunksX; i++)
        {
            for (int k = 0; k < chunksZ; k++)
            {
                for (int j = 0; j < chunksY; j++)
                {
                    Vector3 pos = new Vector3(i * width, -j * height, k * length);
                    GameObject chunkS = Instantiate(chunk, pos, Quaternion.identity);
                    Chunk c = chunkS.GetComponent<Chunk>();
                    if (j < atmosphereLayers)
                    {
                        c.emptyStart = true;
                    }
                    c.SetIdentifier("Chunk." + i + "." + k + "." + j);
                    c.SetLocation(new Vector3(i, j, k));
                    c.Initialize();

                    setChunk(c, i, j, k);
                    c.SetSpawner(this);
                    if (j == atmosphereLayers)
                    {
                        c.Expose();
                    }
                    if (loadingText != null)
                    {
                        loadingText.text = "Loading . . . " + (int)(100f * (i * chunksY * chunksZ + k * chunksY + j) / (chunksX * chunksY * chunksZ)) + "%\n"+ (i * chunksY * chunksZ + k * chunksY + j)+ " chunks loaded out of 1000";
                    }
                }
                yield return null;


            }
        }
        TreeSpawner.world = this;
        TreeSpawner.Do();
        foreach(Chunk chunky in world)
        {
            chunky.ApplyChanges();
        }
        player.InitializePlayerPosition();
        loader.Begin();
        finishedLoading = true;
        //re-randomize randomness
        Random.InitState((int)(Time.deltaTime*12838));
        camera.enabled = true;
        loadingCam.enabled = false;
    }
}
