using System.Collections.Generic;
using NewtonVR;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace _RoboCharm.scripts {
    public class Elevator : MonoBehaviour {
        //Inspector variables
        [SerializeField]
        private Vector3 velocity = new Vector3(0, 1, 0);

        [SerializeField]
        private string[] sceneNames;

        [SerializeField]
        private uint sceneLoadRange = 2;

        [SerializeField]
        private uint sceneSpacing = 4;

        [SerializeField]
        private GameObject barrierPrefab;

        //private member variables
        private readonly LinkedList<Vector3> scenePositions = new LinkedList<Vector3>();
        private readonly LinkedList<Scene> scenes = new LinkedList<Scene>();
        private readonly LinkedList<ElevatorBarrier> barriers = new LinkedList<ElevatorBarrier>();
        private uint nextSceneIndex = 0;
        private GameObject playerHead;
        private Vector3? endPosition;
        private uint currentSceneIndex;
        private bool reloadingScene;

        // Properties
        public uint SceneLoadedCount {
            get { return sceneLoadRange * 2 + 1; }
        }

        public uint MaxLoadedScene {
            get { return nextSceneIndex - 1; }
        }

        public uint MinLoadedScene {
            get { return nextSceneIndex - SceneLoadedCount; }
        }

        // Use this for initialization
        private void Start () {
            //register callback functions for asynchronos scene loading
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;

            playerHead = GetComponentInChildren<NVRHead>().gameObject;

            for (int i = 0; i <= sceneLoadRange; ++i) {
                LoadNextScene(false);
            }
            currentSceneIndex = 0;
        }

        // Update is called once per frame
        private void Update () {
            Vector3 toEndPosition = Vector3.positiveInfinity;
            if (endPosition != null) {
                toEndPosition = endPosition.Value - transform.position;
            }

            Vector3 translation = velocity * Time.deltaTime;

            if (toEndPosition.sqrMagnitude > translation.sqrMagnitude) {
                transform.position += translation;
            }
            else {
                System.Diagnostics.Debug.Assert(endPosition != null, "endPosition != null");
                transform.position = endPosition.Value;
            }
        }

        public bool LoadNextScene (bool async = true) {
            //early out if there isn't a scene to load
            if (nextSceneIndex >= sceneNames.Length) {
                return false;
            }

            var sceneName = sceneNames[nextSceneIndex];
            ++nextSceneIndex;
            if (async) {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
            else {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }

            return true;
        }

        private void OnSceneUnloaded (Scene scene)
        {
            if (reloadingScene)
            {
                string sceneName = scene.name;
                Debug.Log("Unloaded " + sceneName);
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }

        private void OnSceneLoaded (Scene scene,
            LoadSceneMode mode) {

            Debug.Log("Loaded " + scene.name);
            Vector3 scenePosition = new Vector3();
            if (reloadingScene)
            {
                Debug.Log("Reloaded " + scene.name);
                scenePosition = GetCurrentScenePosition();
            } else if (scenePositions.Count > 0) {
                scenePosition = scenePositions.Last.Value + new Vector3(0, sceneSpacing, 0);
            }

            GameObject[] rootObjects = scene.GetRootGameObjects();
            foreach (GameObject rootObject in rootObjects) {
                rootObject.transform.position += scenePosition;

                Light[] lights = rootObject.GetComponentsInChildren<Light>();
                foreach (Light light in lights) {
                    if (light.type == LightType.Directional) {
                        light.enabled = false;  
                    }
                }

                NVRPlayer player = rootObject.GetComponentInChildren<NVRPlayer>();
                if (player != null) {
                    GameObject.Destroy(player.gameObject);
                }
            }

            //when restarting a scene, we don't want to recreate the barrier or add anything back into the lists
            if (reloadingScene)
            {
                LinkedListNode<Scene> sceneNode = scenes.First;
                for (int i = 0; i < currentSceneIndex; ++i)
                {
                    sceneNode = sceneNode.Next;
                }

                Debug.Log("Replacing " + sceneNode.Value + " in list");
                sceneNode.Value = scene;
                return;
            }

            if (nextSceneIndex < sceneNames.Length) {
                GameObject barrierObject = GameObject.Instantiate(barrierPrefab);
                ElevatorBarrier barrier = barrierObject.GetComponent<ElevatorBarrier>();
                barrier.transform.position += scenePosition;
                barrier.ElevatorObj = this;
                barrier.PlayerHead = playerHead;
                barriers.AddLast(barrier);
            }
            else {
                endPosition = barriers.Last.Value.transform.position;
            }

            scenePositions.AddLast(scenePosition);
            scenes.AddLast(scene);
            if (scenes.Count > SceneLoadedCount) {
                UnloadScene();
            }
        }

        private void UnloadScene () {
            Scene scene = scenes.First.Value;
            scenes.RemoveFirst();
            scenePositions.RemoveFirst();
            SceneManager.UnloadSceneAsync(scene);
            --currentSceneIndex;
        }

        public void DisableBarrier () {
            barriers.First.Value.KillPlayer = false;
        }

        public void KillPlayer () {
            Debug.Log("Kill Player");
            transform.position += new Vector3(0, -sceneSpacing, 0);

            ResetCurrentScene();
        }

        public void PassBarrier () {
            ElevatorBarrier barrier = barriers.First.Value;
            GameObject.Destroy(barrier.gameObject);
            barriers.RemoveFirst();
            LoadNextScene();
            ++currentSceneIndex;
        }

        public Scene GetCurrentScene () {
            LinkedListNode<Scene> sceneNode = scenes.First;
            Debug.Log("Getting Scene " + currentSceneIndex);
            for (int i = 0; i < currentSceneIndex; ++i)
            {
                sceneNode = sceneNode.Next;
            }
            return sceneNode.Value;
        }

        public Vector3 GetCurrentScenePosition ()
        {
            LinkedListNode<Vector3> positionNode = scenePositions.First;
            for (int i = 0; i < currentSceneIndex; ++i)
            {
                positionNode = positionNode.Next;
            }
            return positionNode.Value;
        }

        private void ResetCurrentScene() {
            Scene currentScene = GetCurrentScene();
            Debug.Log("Current Scene is " + currentScene.name);
            reloadingScene = true;

            SceneManager.UnloadSceneAsync(currentScene);
            

        }
    }
}