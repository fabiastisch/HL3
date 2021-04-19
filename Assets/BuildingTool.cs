using UnityEngine;

public class BuildingTool : Tool {
    private Camera cam;
    public float maxBuildingDistance = 10f;
    public LayerMask buildableLayerMask;
    public LayerMask buildableWithoutPrefabs;
    public GameObject[] frameworks;
    public GameObject[] buildings;
    private int frameworkIndex = 0;
    private GameObject currentFramework;


    private void Start() {
        cam = GameSettings.Instance.cam;
        Debug.Log("Start");
        Physics.IgnoreLayerCollision(0, 8);
    }

    protected override void OnUpdate() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) {
            // Debug.Log("scrollUp");
            // scroll up
            frameworkIndex = frameworkIndex - 1 >= 0 ? frameworkIndex - 1 :
                this.frameworks.Length > 0 ? this.frameworks.Length - 1 : 0;
        }
        else if (scroll < 0f) {
           // Debug.Log("scrollDown");
            // scroll down
            frameworkIndex = frameworkIndex + 1 < this.frameworks.Length ? frameworkIndex + 1 : 0;
        }

        if (Input.GetButtonDown("Fire1")) {
            if (currentFramework) {
                //Debug.Log("Instantiate : " + currentFramework.transform.position);
                //Debug.Log("Rotation : " + currentFramework.transform.eulerAngles);
                //Debug.Log("Cam : " + cam.transform.forward);
                Transform currentFrameworkTransform = currentFramework.transform;
                Destroy(currentFramework);
                currentFramework = null;
                GameObject o = Instantiate(this.buildings[frameworkIndex], currentFrameworkTransform.position,
                    currentFrameworkTransform.localRotation);
                    o.transform.parent = GameSettings.Instance.buildings.transform;
                    o.GetComponent<BuildingWrapper>().Building.SetColliderRotation(currentFrameworkTransform.localRotation);
            }

            // replace Framework with building;
            // framework = null;
        }


        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawLine(ray.origin, ray.GetPoint(maxBuildingDistance));
        RaycastHit raycastHit;
        
        if (Physics.Raycast(ray, out raycastHit, maxBuildingDistance, buildableLayerMask)) {
            if (raycastHit.transform.gameObject.CompareTag(this.frameworks[frameworkIndex].tag)) {// If the Object already existed
                Debug.Log("Exist" + raycastHit.transform.gameObject.tag);
                return;
            }
            // Debug.Log("RayCastHit");
            Vector3 hitPoint = raycastHit.point;
            hitPoint.x = Mathf.Floor(hitPoint.x / 3) * 3;
            hitPoint.z = Mathf.Ceil(hitPoint.z / 3) * 3;
            hitPoint.y = (Mathf.Round(hitPoint.y / 3)) * 3;

            Vector3 direction = cam.transform.forward;
            /*if (direction.x < 0.5f && direction.x > -0.5f) {
                direction.z 
            }*/

            direction.y = 0;
            direction.x = Mathf.Round(direction.x);
            direction.z =  Mathf.Abs(direction.x) == 1 ? 0 : Mathf.Round(direction.z);
            // Debug.Log(direction);
            
            /**
             * Fix Rotation issues
             */
            if (direction.x == -1f) {
                hitPoint.z -= 3;
            }

            if (direction.x == 1f) {
                hitPoint.x += 3;
            }

            if (direction.z == -1) {
                hitPoint.x += 3;
                hitPoint.z -= 3;
            }
            Debug.DrawLine(transform.position, hitPoint);


            Quaternion rotation = direction != Vector3.zero ? Quaternion.LookRotation(direction) : Quaternion.identity;
            
            bool checkSphere = Physics.CheckSphere(hitPoint, 1.6f, buildableWithoutPrefabs);

            if (checkSphere) {

                currentFramework = Instantiate(this.frameworks[frameworkIndex], hitPoint, rotation);
            
            currentFramework.transform.parent = GameSettings.Instance.buildings.transform;
            }
        }
        else {
            if (currentFramework) {
                Destroy(currentFramework);
            }
        }
    }

    private void OnDisable() {
        if (currentFramework) {
            Destroy(currentFramework);
        }
    }
}