using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.Jobs;
using Unity.Collections;

public class Chunk : MonoBehaviour
{
    
    public static GameObject mainCamera;
    private static int renderDist = 60;
    private static int closeRenderDist = 20;
    private static int playerFOV = 90;
            [SerializeField] private GameObject blocks;
    private Block[] chunk;
    //private int index = 0;
    private BlockChange changeLog;
    private string savepath;

    private string identifier;
    private Vector3 loc;

    private Spawner spawn;

    public bool emptyStart = false;

    [SerializeField] private Transform top;

    public bool exposed = false;

    private bool disabled = false;

    JobHandle visibilityCheckHandle;
        NativeArray<bool> visibilityCheckResult;

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {

        if(IsVisibleChunk())
        {
            if (disabled)
            {
                Enable();

            }
        }
        else
        {
            if(!disabled)
            {
                Disable();

            }
        }
        
    }
    public void RecordChange(string change, Block block)
    {
        string saveCode = block.transform.localPosition.x + "/" + block.transform.localPosition.y + "/" + block.transform.localPosition.z + "/" + change;
        int index = changeLog.FindChangeToBlock(block.transform.localPosition);
        if (index!=-1)
        {
            changeLog.changeCodes[index] = (saveCode);

        }
        else
        {
            changeLog.changeCodes.Add(saveCode);

        }
        //SaveDataToFile(savepath,changeLog);
        //index++;
    }
    void SaveDataToFile(string filePath,BlockChange change)
    {
        string jsonData = JsonUtility.ToJson(change);
        File.WriteAllText(filePath, jsonData);
    }
    void SaveDataToFile()
    {
        string jsonData = JsonUtility.ToJson(changeLog);
        File.WriteAllText(savepath, jsonData);
    }
    void ReadData(string path)
    {
        changeLog = JsonUtility.FromJson<BlockChange>(File.ReadAllText(path));
    }
    public void ApplyChanges()
    {
        exposed = changeLog.exposed;
        foreach (string change in changeLog.changeCodes)
        {
            string[] code = change.Split("/");
            int x = int.Parse(code[0]);
            int y = int.Parse(code[1]);
            int z = int.Parse(code[2]);
            ApplyChange(new Vector3(x,y,z),code); 
        }
    }
    public void ApplyAlternateChanges(BlockChange changes, Vector3 offset,int rotationalOffset)
    {
        exposed = changes.exposed;
        foreach (string change in changes.changeCodes)
        {
            string[] code = change.Split("/");
            int x = int.Parse(code[0]);
            int y = int.Parse(code[1]);
            int z = int.Parse(code[2]);
            Vector3 position = new Vector3(x,y,z);
            if(rotationalOffset>0)
            {
                    for(int i = 0; i<rotationalOffset; i++)
                    {
                        position = CW(position);
                    }
            }
                if(rotationalOffset<0)
                {
                    for(int i = 0; i<-1*rotationalOffset; i++)
                    {
                        position = CCW(position);
                    }
            }
            ApplyChange(position+offset,code); 
        }
    }
    public void ApplyChange(Vector3 position,string[] code)
    {

               Vector3 offC = new Vector3((int)position.x/5,-1*((int)position.y/5),(int)position.z/5);
                position.x%=5;
                position.y%=5;
                position.z%=5;
                if(position.x<0)
                {
                    offC.x--;
                    position.x+=5;
                }
                if(position.y<0)
                {
                    offC.y++;
                    position.y+=5;
                }
                if(position.z<0)
                {
                    offC.z--;
                    position.z+=5;
                }
                if(offC.sqrMagnitude>0.25)
                {
                    Chunk c = GetNearOtherChunk(offC);
                    if(c!=null)
                    {
                    c.ApplyChange(position,code);

                    }
                }
                else
                {
                
            string type = code[3];
            int rotation = int.Parse(code[4]);
            //Debug.Log("altering block at position: "+position);
            Block b = GetBlock((int)position.x, (int)position.y, (int)position.z);
            if(type == "mined")
            {
                //Debug.Log("erased block");
                b.gameObject.SetActive(false);
            }
            if(type.Length>7)
            {
                if (type.Substring(0, 7) == "place: ")
                {

                    //Debug.Log("placed block");
                    b.gameObject.SetActive(true);
                    b.SetBlock(type.Substring(7));

                }
            }
            b.transform.rotation = Quaternion.Euler(0,rotation,0);
            }
    }

    private Vector3 CW(Vector3 input)
    {
        return new Vector3(input.z,input.y,4-input.x);
    }
    private Vector3 CCW(Vector3 input)
    {
        return new Vector3(4-input.z,input.y,input.x);
    }
    
    public void Initialize()
    {
        SaveManager.Register(SaveDataToFile);
        savepath = Overlord.saveDirectory+"/worldSaves/";
        Directory.CreateDirectory(savepath);
        savepath += identifier + ".savedata";
        Init(5, 5, 5);
        if (!File.Exists(savepath) || Overlord.OverwriteMode)
        {
            changeLog = new BlockChange();
            SaveDataToFile(savepath, changeLog);

        }
        else
        {
            ReadData(savepath);
        }
        blocks.GetComponent<ChunkBlocks>().SpawnChunk(Spawner.seed);

        
        //ApplyChanges(changeLog);
    }
    public void Init(int x, int y, int z)
    {
        chunk = new Block[x * y * z];
    }
    public Block GetBlock(int x, int y, int z)
    {
        //Debug.Log("found " + chunk[25 * z + 5 * y + x] + " at " + (25 * z + 5 * y + x));
        return chunk[25*z + 5*y + x];
    }
    public void addBlock (Block b,  int x, int y, int z)
    {
        //Debug.Log("added at: " + (25 * z + 5 * y + x));
        chunk[25*z + 5*y + x] = b;
    }
    public void SetIdentifier(string i)
    {
        identifier = i;
    }
    public void SetSpawner(Spawner sp)
    {
        spawn = sp;
    }
    public Chunk GetNearChunk(Vector3 offset)
    {
        Vector3 location = loc + offset;
        if (InWorld(location))
        {
            return spawn.GetChunk((int)location.x, (int)location.y, (int)location.z);
        }
        else return this;
    }
    public Chunk GetNearOtherChunk(Vector3 offset)
    {
        Vector3 location = loc + offset;
        if (InWorld(location))
        {
            return spawn.GetChunk((int)location.x, (int)location.y, (int)location.z);
        }
        else return null;
    }
    public void SetLocation(Vector3 location)
    {
        loc = location;
    }
    private bool InWorld(Vector3 location)
    {
        if (location.x < 0) return false;
        if (location.y < 0) return false;

        if (location.z < 0) return false;

        if (location.x >= spawn.chunksX) return false;

        if (location.y >= spawn.chunksY) return false;

        if (location.z >= spawn.chunksZ) return false;
        return true;

    }
    private bool IsVisibleChunk()
    {
        //make sure we activate near chunks
        if ((transform.position - mainCamera.transform.position).sqrMagnitude < closeRenderDist*closeRenderDist)
        {

            return true;
        }
        //is covered up?
        //how to do it?

        if(!exposed)
        {
            return false;
        }
        //in line of sight
        if (Vector3.Angle(transform.position- mainCamera.transform.position, mainCamera.transform.forward) < playerFOV)
        {
            return true;
        }

        
        
        return false;
    }
    public void Expose()
    {
    exposed = true;
        changeLog.exposed = true;
    }
    public void Hide()
    {
        exposed = false;
        changeLog.exposed = false;
    }
    public bool IsEmpty()
    {
        foreach(Block b in chunk)
        {
            if(b.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
    public Vector3 GetLocation() { return loc; }
    private void Disable()
    {
        disabled = true;
        foreach(Renderer rend in blocks.GetComponentsInChildren<Renderer>())
        {
            rend.enabled = false;
        }
    }
    private void Enable()
    {
        disabled = false;
        foreach (Renderer rend in blocks.GetComponentsInChildren<Renderer>())
        {
            rend.enabled = true;
        }
    }
}
