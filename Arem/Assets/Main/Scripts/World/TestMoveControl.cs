using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class TestMoveControl : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;


    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.TryGetComponent<LocationMapPoint>(out var locationMapPoint))
                {
                    StopAllCoroutines();
                    StartCoroutine(EnterLocation(locationMapPoint));
                }
                else
                {
                    StopAllCoroutines();
                    _navMeshAgent.SetDestination(hit.point);
                }
            }
        }
    }


    private IEnumerator EnterLocation(LocationMapPoint locationMapPoint)
    {
        _navMeshAgent.SetDestination(locationMapPoint.transform.position);

        while (Vector3.Distance(transform.position, locationMapPoint.transform.position) > 1f)
            yield return null;

        var popUpFactory = ControllersContainer.GetController<PopUpFactory>();
        var popUp = popUpFactory.Create<PopUpEnterLocation>();

        var contextData = new PopUpEnterLocationContextData(
            locationMapPoint.EnterLocationContextData.Description,
            locationMapPoint.EnterLocationContextData.Sprite,
            () =>
            {
                popUp.Hide();

                var runner = new ActionsRunner();
                runner.Setup(new LoadSceneStep(locationMapPoint.SceneName));
                runner.Run();
            },
            () => popUp.Hide());

        popUp.Show(contextData);
    }
}