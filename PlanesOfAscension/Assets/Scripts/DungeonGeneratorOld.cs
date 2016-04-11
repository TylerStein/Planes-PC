using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DungeonGeneratorOld : MonoBehaviour
{

    //List of prefabs
    public Module[] Modules;
    public Module StartModule;
    public Module CapModule;

    public TreeStructure levelTree;

    [Range(0, 50)]
    public int iterations = 5;

    [Range(0, 9)]
    public int CollideDistance;

    void Start()
    {
        //Instantiate the start module at origin
        Module startModule = (Module)Instantiate(StartModule, transform.position, transform.rotation);
        //Grab the exits of the start module
        List<ModuleConnector> pendingExits = new List<ModuleConnector>(startModule.GetExits());

        //Create the level tree with start module as root
        levelTree = new TreeStructure(startModule);

        //Create the list of modules
        List<Module> moduleList = new List<Module>();
        //Add the start module to the list
        moduleList.Add(startModule);

        //For each iteration
        for (int iteration = 0; iteration < iterations; iteration++)
        {
            //Create a list of new exits
            List<ModuleConnector> newExits = new List<ModuleConnector>();

            //For each pending exit
            foreach (ModuleConnector pendingExit in pendingExits)
            {
                bool colliding = false;
                ModuleConnector[] newModuleExits;
                ModuleConnector exitToMatch;
                Module newModule;

                do
                {

                    string newTag = GetRandom(pendingExit.Tags);
                    Module newModulePrefab = GetRandomWithTag(Modules, newTag);
                    newModule = (Module)Instantiate(newModulePrefab);
                    newModuleExits = newModule.GetExits();
                    exitToMatch = newModuleExits.FirstOrDefault(x => x.isDefault) ?? GetRandom(newModuleExits);
                    MatchExits(pendingExit, exitToMatch);

                    //Does newModule collide with any of the modules in the list?
                    colliding = checkColliding(newModule, moduleList);
                    if (colliding)
                    {
                        //Get rid of the colliding module to try again
                        //Destroy(newModule);
                    }
                } while (!colliding);

                moduleList.Add(newModule);

                foreach (ModuleConnector newExit in newModuleExits)
                {
                    if (newExit != exitToMatch)
                    {
                        newExits.Add(newExit);
                    }
                }
            }

            pendingExits = newExits;
        }


        //Clear the module list for reuse
        moduleList.Clear();

        foreach (ModuleConnector pendingExit in pendingExits)
        {
            Module newModule = (Module)Instantiate(CapModule); //Create a cap module
            ModuleConnector[] newModuleExits = newModule.GetExits(); //Get the cap module's exit
            MatchExits(pendingExit, newModuleExits[0]); //Connect the pending exit with the cap's 0th exit



            moduleList.Add(newModule);
        }

        /*
        Module lastCap = null;
        List<Module> capsToRemove = new List<Module>();

        
        //Go over the caps and check for overlaps
        foreach (Module cap in moduleList)
        {
            if (lastCap == null)
            {
                lastCap = cap;
            }
            else
            {
                if (lastCap.transform.position == cap.transform.position)
                {
                    capsToRemove.Add(lastCap);
                }
                lastCap = cap;
            }
        }

        for (int i = 0; i < capsToRemove.Count; i++)
        {
            //Grab the cap to remove
            GameObject tbd = capsToRemove[i].gameObject;
            //Remove the cap from the list
            capsToRemove.RemoveAt(i);
            //Destroy the cap
            Destroy(tbd);

        }*/
    }

    private bool checkColliding(Module checkModule, List<Module> moduleList)
    {
        foreach (Module currentModule in moduleList)
        {
            if (currentModule != checkModule)
            {
                float distance = (currentModule.transform.position - checkModule.transform.position).magnitude;
                if (distance < CollideDistance)
                {
                    //Not colliding
                    return false;
                }
                else
                {
                    //Within collide distance
                    Debug.Log("There was a collision!");
                    return true;
                }
            }
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
