using UnityEngine;
using SpaceStrategy.Services;

namespace SpaceStrategy.Services
{
    public class BackendChecker : MonoBehaviour
    {
        [Header("Настройки")]
        [SerializeField] private string backendUrl = "http://localhost:8080";
        [SerializeField] private float checkInterval = 10f;
        [SerializeField] private bool checkOnStart = true;

        private BackendService _backendService;

        private void Start()
        {
            _backendService = GetComponent<BackendService>();
            if (_backendService == null)
            {
                _backendService = gameObject.AddComponent<BackendService>();
            }

            _backendService.SetBackendUrl(backendUrl);
            _backendService.SetCheckInterval(checkInterval);

            if (checkOnStart)
            {
                Debug.Log("[BackendChecker] Запуск проверки бэкенда...");
                _backendService.CheckBackendStatus();
            }
        }

        public void CheckNow()
        {
            _backendService.CheckBackendStatus();
        }
    }
}
