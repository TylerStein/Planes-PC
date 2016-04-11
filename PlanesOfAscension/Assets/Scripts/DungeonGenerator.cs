using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/*Procedural Segment-Based Dungeon Generator
 * Created by Tyler Stein
 * April 3, 2016
 */

public class DungeonGenerator : MonoBehaviour {

    //List of prefabs
    public Module[] Modules;
    public Module StartModule;
    public Module CapModule;

    public Module CapWall;

    public TreeStructure levelTree;

    public string levelSeed = "Tyler";
    public bool useRandomSeed = false;

    [Range(0, 50)]
    public int iterations = 3;
    
    void Start()
    {
        //==SET UP THE RANDOM SEED==//
        if(useRandomSeed){
            Random.seed = (int)System.Environment.TickCount;
        }else{
            Random.seed = (int)levelSeed.GetHashCode();
        }

        //==CREATE THE START MODULE AND LISTS==//
        Module startModule = (Module)Instantiate(StartModule, transform.position, transform.rotation);
        //Create the level tree with start module as root
        //levelTree = new TreeStructure(startModule);
        //Create the list of modules
        List<Module> moduleList = new List<Module>();
        //Add the start module to the list
        moduleList.Add(startModule);
        //Track depth node
        //Node currentParent = levelTree.Root;
        //Get list of pending exits
        List<ModuleConnector> pendingExits = new List<ModuleConnector>(startModule.GetExits());

        //Iterate
        for (int iteration = 0; iteration < iterations; iteration++)
        {
            //Create kust if new exuts
            //Create a list of new exits
            List<ModuleConnector> newExits = new List<ModuleConnector>();

            //For each pending exit
            foreach (ModuleConnector pendingExit in pendingExits)
            {
                ModuleConnector[] newModuleExits;
                ModuleConnector exitToMatch;
                Module newModule;

                string newTag = GetRandom(pendingExit.Tags);
                Module newModulePrefab = GetRandomWithTag(Modules, newTag);
                newModule = (Module)Instantiate(newModulePrefab);
                newModuleExits = newModule.GetExits();
                exitToMatch = newModuleExits.FirstOrDefault(x => x.isDefault) ?? GetRandom(newModuleExits);
                MatchExits(pendingExit, exitToMatch);

                //Check for collision between newModule and other modules
                if (checkCollision(newModule, moduleList))
                {
                    Destroy(newModule);
                }
                else
                {
                    moduleList.Add(newModule);

                    foreach (ModuleConnector newExit in newModuleExits)
                    {
                        if (newExit != exitToMatch)
                        {
                            newExits.Add(newExit);
                        }
                    }
                }

                
            }

            pendingExits = newExits;
            
        }

        moduleList.Clear();

        //Cap each remaining exit
        foreach (ModuleConnector pendingExit in pendingExits)
        {
            Module wallModule = (Module)Instantiate(CapWall); //Create a cap module
            ModuleConnector[] newModuleExits = wallModule.GetExits(); //Get the cap module's exit
            MatchExits(pendingExit, newModuleExits[0]); //Connect the pending exit with the cap's 0th exit
            moduleList.Add(wallModule);
        }
        

    }

    private bool checkCollision(Module newModule, List<Module> moduleList)
    {
        //Get center and size of room
        Vector3 newModuleCenter = newModule.gameObject.transform.position;

        BoxCollider newCollider = newModule.GetComponent<BoxCollider>();

        //Vector2 halfSize = new Vector3(newModuleSize.x / 10, newModuleSize.y / 10, newModuleSize.z / 10);
        Vector3 halfSize = new Vector3(newCollider.bounds.extents.x, newCollider.bounds.extents.y, newCollider.bounds.extents.z);

        foreach (Module m in moduleList)
        {
            BoxCollider testCollider = m.GetComponent<BoxCollider>();
            float diff = (newModuleCenter - m.transform.position).magnitude;

            if (diff < 9 && (newModule != m))
            {
                //Debug.DrawLine(newModuleCenter + new Vector3(0, halfSize.y, 0), newModuleCenter + new Vector3(halfSize.x, halfSize.y, 0), Color.red, 1000.0f);
                //Debug.DrawLine(newModuleCenter + new Vector3(0, halfSize.y, 0), newModuleCenter + new Vector3(0, halfSize.y + halfSize.y, 0), Color.green, 1000.0f);
                //Debug.DrawLine(newModuleCenter + new Vector3(0, halfSize.y, 0), newModuleCenter + new Vector3(0, halfSize.y, halfSize.z), Color.blue, 1000.0f);
                Debug.Log("Distance between intersecting modules: " + (newModuleCenter - m.transform.position).magnitude);

                Debug.DrawLine(newModuleCenter + new Vector3(0, halfSize.y, 0), m.transform.position, Color.red, 1000.0f);

                return true;
            }

            /*
            if (testCollider.bounds.Intersects(newCollider.bounds))
            {

                Debug.DrawLine(newModuleCenter + new Vector3(0, halfSize.y, 0), newModuleCenter + new Vector3(halfSize.x, halfSize.y, 0), Color.red, 1000.0f);
                Debug.DrawLine(newModuleCenter + new Vector3(0, halfSize.y, 0), newModuleCenter + new Vector3(0, halfSize.y + halfSize.y, 0), Color.green, 1000.0f);
                Debug.DrawLine(newModuleCenter + new Vector3(0, halfSize.y, 0), newModuleCenter + new Vector3(0, halfSize.y, halfSize.z), Color.blue, 1000.0f);

                Debug.Log("Distance between intersecting modules: " + (newModuleCenter - m.transform.position).magnitude);

                return true;
            }*/
        }

        return false;
    }

    private void MatchExits(ModuleConnector oldExit, ModuleConnector newExit)
    {
        Transform newModule = newExit.transform.parent;
        Vector3 forwardVectorToMatch = -oldExit.transform.forward;
        float correctiveRotation = Azimuth(forwardVectorToMatch) - Azimuth(newExit.transform.forward);
        newModule.RotateAround(newExit.transform.position, Vector3.up, correctiveRotation);
        Vector3 correctiveTranslation = oldExit.transform.position - newExit.transform.position;
        newModule.transform.position += correctiveTranslation;
    }

    //Generic "GetRandom" returns a random of any type (<TItem>) from an array
    public static TItem GetRandom<TItem>(TItem[] array)
    {
        
        return array[Random.Range(0, array.Length)];
    }

    public static Module GetRandomWithTag(IEnumerable<Module> modules, string tagToMatch)
    {
        Module[] matchingModules = modules.Where(m => m.Tags.Contains(tagToMatch)).ToArray();
        return GetRandom(matchingModules);
    }

    private static float Azimuth(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }
    
}
