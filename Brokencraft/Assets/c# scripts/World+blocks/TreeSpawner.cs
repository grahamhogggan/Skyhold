using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    private static int spacing = 14;
    private static int seed = 1729;
    public static Spawner world;
    public TreeProfile[] TheTreeProfiles;
    private static TreeProfile[] treeProfiles;
    void Awake()
    {
        treeProfiles = TheTreeProfiles;
    }
    // Start is called before the first frame update
    public static void Do()
    {
        TreeProfile profile = treeProfiles[(int)Overlord.type];
        spacing = profile.spacing;
        for (int x = 2; x < world.chunksX * 5 - 2; x += Random.Range(2, 4))
        {
            for (int y = 2; y < world.chunksZ * 5 - 2; y += Random.Range(2, 4))
            {
                int heightAtValue = (int)ChunkBlocks.GetPerlinValue(x, y);
                Block currentBlock = world.GetBlock(x, heightAtValue, y);
                Block originBlock = world.GetBlock(x, heightAtValue + 1, y);
                if (currentBlock == null) continue;

                if (currentBlock.typeName == "grass" && Random.Range(0, spacing / 2) == 1)
                {
                    if (Random.Range(0, 3) != 1)
                    {
                        if (profile.bushes) SpawnBush(originBlock);
                    }
                    else if (profile.pines) SpawnPineTree(originBlock);
                    else if (profile.trees) SpawnTree(originBlock);
                }
                else if ((currentBlock.typeName == "sand" && originBlock.typeName != "water4" && Random.Range(0, spacing) == 1))
                {
                    if (profile.palms) SpawnPalmTree(originBlock);
                }
                else if (currentBlock.typeName == "snow"  && (originBlock.typeName != "water4" && originBlock.typeName != "ice")&& Random.Range(0, spacing) == 1)
                {
                    if (profile.pines) SpawnPineTree(originBlock);
                }
            }
        }
    }
    private static void SpawnTree(Block origin)
    {
        BlockChange change = StructureManager.GetStructure("tree");
        Chunk c = origin.getChunk();
        Vector3 offset = origin.GetLocalPos() + new Vector3(-2, 0, -2);
        c.ApplyAlternateChanges(change, offset, 0);
        BlockChange part2 = StructureManager.GetStructure("tree pt 2");
        c.GetNearChunk(new Vector3(0, -1, 0)).ApplyAlternateChanges(part2, offset, 0);

    }
    private static void SpawnPineTree(Block origin)
    {
        BlockChange change = StructureManager.GetStructure("pine tree");
        Chunk c = origin.getChunk();
        Vector3 offset = origin.GetLocalPos() + new Vector3(-2, 0, -2);
        c.ApplyAlternateChanges(change, offset, 0);
        BlockChange part2 = StructureManager.GetStructure("pine tree 2");
        c.GetNearChunk(new Vector3(0, -1, 0)).ApplyAlternateChanges(part2, offset, 0);
    }
    private static void SpawnBush(Block origin)
    {
        origin.SetBlock("bush");
        origin.gameObject.SetActive(true);
    }
    private static void SpawnPalmTree(Block origin)
    {

        int rot = Random.Range(0, 4);
        BlockChange change = StructureManager.GetStructure("palm tree 1");
        Chunk c = origin.getChunk();
        Vector3 offset = origin.GetLocalPos() + new Vector3(-2, 0, -2);

        switch (rot)
        {
            case 0:
                offset += new Vector3(0, 0, -1);
                break;
            case 1:
                offset += new Vector3(-1, 0, 0);
                break;
            case 2:
                offset += new Vector3(0, 0, 1);
                break;
            case 3:
                offset += new Vector3(1, 0, 0);
                break;

        }

        c.ApplyAlternateChanges(change, offset, rot);
        BlockChange part2 = StructureManager.GetStructure("palm tree 2");
        c.GetNearChunk(new Vector3(0, -1, 0)).ApplyAlternateChanges(part2, offset, rot);

    }

}
[System.Serializable]
public struct TreeProfile
{
    public int spacing;
    public bool trees;
    public bool bushes;
    public bool palms;
    public bool pines;

}