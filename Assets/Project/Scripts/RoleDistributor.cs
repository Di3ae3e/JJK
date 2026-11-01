using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;
public class RoleDistributor : MonoBehaviour
{
    [System.Serializable]
    public class Role
    {
        [SerializeField] private string name;
        [SerializeField] private GameObject rolePrefab;
        [SerializeField][Range(0f, 1f)] private float weight;
        public string Name
        {
            get { return name; }
            private set {  name = value; }
        }
        public GameObject RolePrefab
        { 
            get { return rolePrefab; }
            private set { rolePrefab = value; }
        }
        public float Weight
        {
            get { return weight; }
            private set { weight = value; }
        }
    }

    public GameObject background;
    public TMP_Text roleText;
    [SerializeField] private Role[] roles;
    [SerializeField] private Transform spawnPoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
    }
    public void Select()
    {
        GameObject selectedRole = SelectRole();
        if(selectedRole != null) 
            Instantiate(selectedRole, spawnPoint);

        var target = GameObject.FindWithTag("CinemachineTarget").transform;
        if (target == null)
        {
        Debug.LogError("no cinemachine target");
            return;
        }
        if (CameraSetup.Instance == null)
        {
            Debug.LogError("no camera setup instance");
            return;
        }
        CameraSetup.Instance.SetUp(target);
        background.SetActive(false);
        roleText.SetText($"your role is: {selectedRole.name}");
    }
    public GameObject SelectRole()
    {
        float weightSum = 0;
        foreach (Role role in roles)
        {
            weightSum += role.Weight;
        }

        float rng = Random.Range(0,weightSum);

        foreach (Role role in roles)
        {
            if (rng < role.Weight)
            {
                Debug.Log(role.Name);
                return role.RolePrefab;
            }
            rng -= role.Weight;
        }
        return null;
    }
}
