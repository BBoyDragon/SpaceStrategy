using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SpaceStrategy.Models;

namespace SpaceStrategy.Services
{
    public class BackendService : MonoBehaviour
    {
        [Header("Backend Configuration")]
        [SerializeField] private string backendUrl = "http://localhost:8080";
        [SerializeField] private float requestTimeout = 5f;
        [SerializeField] private float checkInterval = 10f;

        private Coroutine _checkCoroutine;

        private void Start()
        {
            Debug.Log($"[BackendService] Инициализация сервиса. URL бэкенда: {backendUrl}");
            StartChecking();
        }

        private void OnDestroy()
        {
            StopChecking();
        }

        public void StartChecking()
        {
            if (_checkCoroutine != null)
            {
                StopCoroutine(_checkCoroutine);
            }
            _checkCoroutine = StartCoroutine(CheckBackendStatusCoroutine());
        }

        public void StopChecking()
        {
            if (_checkCoroutine != null)
            {
                StopCoroutine(_checkCoroutine);
                _checkCoroutine = null;
            }
        }

        private IEnumerator CheckBackendStatusCoroutine()
        {
            while (true)
            {
                yield return CheckBackendStatus();
                yield return new WaitForSeconds(checkInterval);
            }
        }

        public Coroutine CheckBackendStatus()
        {
            return StartCoroutine(GetServiceInfoCoroutine());
        }

        private IEnumerator GetServiceInfoCoroutine()
        {
            string url = $"{backendUrl}/api/ServiceInfo";
            Debug.Log($"[BackendService] Запрос к бэкенду: {url}");

            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.timeout = (int)requestTimeout;
                
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    try
                    {
                        string jsonResponse = request.downloadHandler.text;
                        ServiceInfo serviceInfo = JsonUtility.FromJson<ServiceInfo>(jsonResponse);
                        
                        PrintServiceInfo(serviceInfo);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[BackendService] Ошибка при парсинге ответа: {ex.Message}");
                        Debug.LogError($"[BackendService] Ответ сервера: {request.downloadHandler.text}");
                    }
                }
                else
                {
                    HandleConnectionError(request);
                }
            }
        }

        private void PrintServiceInfo(ServiceInfo serviceInfo)
        {
            Debug.Log("=== ИНФОРМАЦИЯ О БЭКЕНДЕ ===");
            Debug.Log($"Название сервиса: {serviceInfo.serviceName}");
            Debug.Log($"Версия: {serviceInfo.version}");
            Debug.Log($"Время запуска: {serviceInfo.startTime}");
            
            Debug.Log("--- Информация о базе данных ---");
            Debug.Log($"Подключено: {(serviceInfo.databaseInfo.isConnected ? "ДА" : "НЕТ")}");
            
            if (serviceInfo.databaseInfo.isConnected)
            {
                Debug.Log($"Имя БД: {serviceInfo.databaseInfo.databaseName ?? "не указано"}");
                Debug.Log($"Версия сервера: {serviceInfo.databaseInfo.serverVersion ?? "не указано"}");
            }
            else
            {
                Debug.LogWarning($"Ошибка подключения к БД: {serviceInfo.databaseInfo.errorMessage ?? "неизвестная ошибка"}");
            }
            
            Debug.Log("===========================");
        }

        private void HandleConnectionError(UnityWebRequest request)
        {
            string errorMessage = "";
            
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    errorMessage = $"Ошибка подключения: не удалось установить соединение с {backendUrl}";
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    errorMessage = "Ошибка обработки данных";
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    errorMessage = $"Ошибка протокола HTTP: {request.responseCode} - {request.error}";
                    break;
                default:
                    errorMessage = $"Неизвестная ошибка: {request.error}";
                    break;
            }

            Debug.LogError($"[BackendService] БЭКЕНД НЕДОСТУПЕН!");
            Debug.LogError($"[BackendService] {errorMessage}");
            
            if (!string.IsNullOrEmpty(request.downloadHandler.text))
            {
                Debug.LogError($"[BackendService] Ответ сервера: {request.downloadHandler.text}");
            }
            
            Debug.LogWarning($"[BackendService] Проверьте, что бэкенд запущен и доступен по адресу: {backendUrl}");
        }

        public void SetBackendUrl(string url)
        {
            backendUrl = url;
            Debug.Log($"[BackendService] URL бэкенда изменен на: {backendUrl}");
        }

        public void SetCheckInterval(float interval)
        {
            checkInterval = interval;
            Debug.Log($"[BackendService] Интервал проверки изменен на: {checkInterval} секунд");
        }
    }
}
