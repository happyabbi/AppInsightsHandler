using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace AppInsightsHandler
{
    public class AppInsightsHandler
    {
        private TelemetryClient _client;

        private readonly List<KeyValuePair<string, string>> _contextGlobalProperties =
            new List<KeyValuePair<string, string>>();

        private EventTelemetry _eventTelemetry;
        private ExceptionTelemetry _exceptionTelemetry;
        private readonly List<KeyValuePair<string, double>> _metrics = new List<KeyValuePair<string, double>>();
        private MetricTelemetry _metricTelemetry;

        private string _objectType;

        private readonly List<KeyValuePair<string, string>> _properties = new List<KeyValuePair<string, string>>();
        private RequestTelemetry _requestTelemetry;
        private TraceTelemetry _traceTelemetry;

        public void InstantiateClient(string instrumentationKey, string objectType)
        {
            if (string.IsNullOrEmpty(instrumentationKey))
                throw new ArgumentNullException(nameof(instrumentationKey), "Instrumentation key is required.");
            if (string.IsNullOrEmpty(objectType))
                throw new ArgumentNullException(nameof(objectType), "Object type is required.");
            this._objectType = objectType;
            var config = new TelemetryConfiguration(instrumentationKey);
            _client = new TelemetryClient(config);
            switch (this._objectType)
            {
                case "EventTelemetry":
                    _eventTelemetry = new EventTelemetry();
                    break;
                case "MetricTelemetry":
                    _metricTelemetry = new MetricTelemetry();
                    break;
                case "TraceTelemetry":
                    _traceTelemetry = new TraceTelemetry();
                    break;
                case "ExceptionTelemetry":
                    _exceptionTelemetry = new ExceptionTelemetry();
                    break;
                case "RequestTelemetry":
                    _requestTelemetry = new RequestTelemetry();
                    break;
            }
        }

        public void AddProperty(string key, string value)
        {
            var property = new KeyValuePair<string, string>(key, value);
            if (!_properties.Exists(item => item.Key == key)) _properties.Add(property);
        }

        public void AddContextGlobalProperty(string key, string value)
        {
            var property = new KeyValuePair<string, string>(key, value);
            if (!_contextGlobalProperties.Exists(item => item.Key == key)) _contextGlobalProperties.Add(property);
        }

        public void AddMetrics(string key, double value)
        {
            var metric = new KeyValuePair<string, double>(key, value);
            if (!_metrics.Exists(item => item.Key == key)) _metrics.Add(metric);
        }

        public void SetContextUserInfo(string accountId, string authenticatedUserId, string userId = "",
            string userAgent = "")
        {
            switch (_objectType)
            {
                case "EventTelemetry":
                    _eventTelemetry.Context.User.AccountId = accountId;
                    _eventTelemetry.Context.User.AuthenticatedUserId = authenticatedUserId;
                    _eventTelemetry.Context.User.Id = userId;
                    _eventTelemetry.Context.User.UserAgent = userAgent;
                    break;
                case "MetricTelemetry":
                    _metricTelemetry.Context.User.AccountId = accountId;
                    _metricTelemetry.Context.User.AuthenticatedUserId = authenticatedUserId;
                    _metricTelemetry.Context.User.Id = userId;
                    _metricTelemetry.Context.User.UserAgent = userAgent;
                    break;
                case "TraceTelemetry":
                    _traceTelemetry.Context.User.AccountId = accountId;
                    _traceTelemetry.Context.User.AuthenticatedUserId = authenticatedUserId;
                    _traceTelemetry.Context.User.Id = userId;
                    _traceTelemetry.Context.User.UserAgent = userAgent;
                    break;
                case "ExceptionTelemetry":
                    _exceptionTelemetry.Context.User.AccountId = accountId;
                    _exceptionTelemetry.Context.User.AuthenticatedUserId = authenticatedUserId;
                    _exceptionTelemetry.Context.User.Id = userId;
                    _exceptionTelemetry.Context.User.UserAgent = userAgent;
                    break;
                case "RequestTelemetry":
                    _requestTelemetry.Context.User.AccountId = accountId;
                    _requestTelemetry.Context.User.AuthenticatedUserId = authenticatedUserId;
                    _requestTelemetry.Context.User.Id = userId;
                    _requestTelemetry.Context.User.UserAgent = userAgent;
                    break;
            }
        }

        public void SetContextOperation(string operationId, string operationName, string parentId = "")
        {
            switch (_objectType)
            {
                case "EventTelemetry":
                    _eventTelemetry.Context.Operation.Id = operationId;
                    _eventTelemetry.Context.Operation.Name = operationName;
                    _eventTelemetry.Context.Operation.ParentId = parentId;
                    break;
                case "MetricTelemetry":
                    _metricTelemetry.Context.Operation.Id = operationId;
                    _metricTelemetry.Context.Operation.Name = operationName;
                    _metricTelemetry.Context.Operation.ParentId = parentId;
                    break;
                case "TraceTelemetry":
                    _traceTelemetry.Context.Operation.Id = operationId;
                    _traceTelemetry.Context.Operation.Name = operationName;
                    _traceTelemetry.Context.Operation.ParentId = parentId;
                    break;
                case "ExceptionTelemetry":
                    _exceptionTelemetry.Context.Operation.Id = operationId;
                    _exceptionTelemetry.Context.Operation.Name = operationName;
                    _exceptionTelemetry.Context.Operation.ParentId = parentId;
                    break;
                case "RequestTelemetry":
                    _requestTelemetry.Context.Operation.Id = operationId;
                    _requestTelemetry.Context.Operation.Name = operationName;
                    _requestTelemetry.Context.Operation.ParentId = parentId;
                    break;
            }
        }

        public void SetContextComponentVersion(string version)
        {
            switch (_objectType)
            {
                case "EventTelemetry":
                    _eventTelemetry.Context.Component.Version = version;
                    break;
                case "MetricTelemetry":
                    _metricTelemetry.Context.Component.Version = version;
                    break;
                case "TraceTelemetry":
                    _traceTelemetry.Context.Component.Version = version;
                    break;
                case "ExceptionTelemetry":
                    _exceptionTelemetry.Context.Component.Version = version;
                    break;
                case "RequestTelemetry":
                    _requestTelemetry.Context.Component.Version = version;
                    break;
            }
        }

        public void SetContextGlobalProperties()
        {
            foreach (var element in _contextGlobalProperties)
                switch (_objectType)
                {
                    case "EventTelemetry":
                        _eventTelemetry.Context.GlobalProperties.Add(element);
                        break;
                    case "MetricTelemetry":
                        _metricTelemetry.Context.GlobalProperties.Add(element);
                        break;
                    case "TraceTelemetry":
                        _traceTelemetry.Context.GlobalProperties.Add(element);
                        break;
                    case "ExceptionTelemetry":
                        _exceptionTelemetry.Context.GlobalProperties.Add(element);
                        break;
                    case "RequestTelemetry":
                        _requestTelemetry.Context.GlobalProperties.Add(element);
                        break;
                }
        }

        public void SetsysExceptionTelemetryValues(Exception exception, string message, string problemId,
            int severityLevel)
        {
            if (_exceptionTelemetry != null)
            {
                _exceptionTelemetry.Exception = exception;
                _exceptionTelemetry.Message = message;
                _exceptionTelemetry.ProblemId = problemId;
                switch (severityLevel)
                {
                    case 0:
                        _exceptionTelemetry.SeverityLevel = SeverityLevel.Verbose;
                        break;
                    case 1:
                        _exceptionTelemetry.SeverityLevel = SeverityLevel.Information;
                        break;
                    case 2:
                        _exceptionTelemetry.SeverityLevel = SeverityLevel.Warning;
                        break;
                    case 3:
                        _exceptionTelemetry.SeverityLevel = SeverityLevel.Error;
                        break;
                    case 4:
                        _exceptionTelemetry.SeverityLevel = SeverityLevel.Critical;
                        break;
                    default:
                        _exceptionTelemetry.SeverityLevel = SeverityLevel.Information;
                        break;
                }

                _exceptionTelemetry.Timestamp = DateTimeOffset.UtcNow;
                foreach (var element in _properties) _exceptionTelemetry.Properties.Add(element);
                foreach (var element in _metrics) _exceptionTelemetry.Metrics.Add(element);
            }
        }

        public void SetExceptionTelemetryValues(string exceptionMessage, string exceptionSource, string message,
            string problemId, int severityLevel)
        {
            if (_exceptionTelemetry != null)
            {
                var e = new Exception(exceptionMessage)
                {
                    Source = exceptionSource
                };
                _exceptionTelemetry.Exception = e;
                _exceptionTelemetry.Message = message;
                _exceptionTelemetry.ProblemId = problemId;
                switch (severityLevel)
                {
                    case 0:
                        _exceptionTelemetry.SeverityLevel = SeverityLevel.Verbose;
                        break;
                    case 1:
                        _exceptionTelemetry.SeverityLevel = SeverityLevel.Information;
                        break;
                    case 2:
                        _exceptionTelemetry.SeverityLevel = SeverityLevel.Warning;
                        break;
                    case 3:
                        _exceptionTelemetry.SeverityLevel = SeverityLevel.Error;
                        break;
                    case 4:
                        _exceptionTelemetry.SeverityLevel = SeverityLevel.Critical;
                        break;
                    default:
                        _exceptionTelemetry.SeverityLevel = SeverityLevel.Information;
                        break;
                }

                _exceptionTelemetry.Timestamp = DateTimeOffset.UtcNow;
                foreach (var element in _properties) _exceptionTelemetry.Properties.Add(element);
                foreach (var element in _metrics) _exceptionTelemetry.Metrics.Add(element);
            }
        }

        public void SetEventTelemetryValues(string eventName)
        {
            if (_eventTelemetry != null)
            {
                _eventTelemetry.Name = eventName;
                _eventTelemetry.Timestamp = DateTimeOffset.UtcNow;
                foreach (var element in _properties) _eventTelemetry.Properties.Add(element);
                foreach (var element in _metrics) _eventTelemetry.Metrics.Add(element);
            }
        }

        public void SetRequestTelemetryValues(string requestName, string requestId, bool success,
            string responseCode, string source, string uriString, double duration)
        {
            if (_requestTelemetry != null)
            {
                _requestTelemetry.Name = requestName;
                _requestTelemetry.Timestamp = DateTimeOffset.UtcNow;
                _requestTelemetry.Success = success;
                _requestTelemetry.Id = requestId;
                _requestTelemetry.ResponseCode = responseCode;
                _requestTelemetry.Source = source;
                _requestTelemetry.Duration = TimeSpan.FromSeconds(duration);
                if (uriString != "")
                {
                    var uri = new Uri(uriString);
                    _requestTelemetry.Url = uri;
                }

                foreach (var element in _properties) _requestTelemetry.Properties.Add(element);
                foreach (var element in _metrics) _requestTelemetry.Metrics.Add(element);
            }
        }


        public void SetMetricTelemetryValues(string name, string metricNamespace, double sum, int count = 0,
            double min = 0, double max = 0)
        {
            if (_metricTelemetry != null)
            {
                _metricTelemetry.Name = name;
                _metricTelemetry.Timestamp = DateTimeOffset.UtcNow;
                _metricTelemetry.MetricNamespace = metricNamespace;
                _metricTelemetry.Sum = sum;
                if (count != 0)
                    _metricTelemetry.Count = count;

                if (min != 0)
                    _metricTelemetry.Min = min;

                if (max != 0)
                    _metricTelemetry.Max = max;

                foreach (var element in _properties) _metricTelemetry.Properties.Add(element);
            }
        }

        public void SetTraceTelemetryValues(string message, int severityLevel)
        {
            if (_traceTelemetry != null)
            {
                _traceTelemetry.Message = message;
                _traceTelemetry.Timestamp = DateTimeOffset.UtcNow;
                switch (severityLevel)
                {
                    case 0:
                        _traceTelemetry.SeverityLevel = SeverityLevel.Verbose;
                        break;
                    case 1:
                        _traceTelemetry.SeverityLevel = SeverityLevel.Information;
                        break;
                    case 2:
                        _traceTelemetry.SeverityLevel = SeverityLevel.Warning;
                        break;
                    case 3:
                        _traceTelemetry.SeverityLevel = SeverityLevel.Error;
                        break;
                    case 4:
                        _traceTelemetry.SeverityLevel = SeverityLevel.Critical;
                        break;
                    default:
                        _traceTelemetry.SeverityLevel = SeverityLevel.Information;
                        break;
                }

                foreach (var element in _properties) _traceTelemetry.Properties.Add(element);
            }
        }

        public void TrackTelemetry()
        {
            if (_client != null && _objectType != "")
            {
                switch (_objectType)
                {
                    case "EventTelemetry":
                        _client.TrackEvent(_eventTelemetry);
                        break;
                    case "MetricTelemetry":
                        _client.TrackMetric(_metricTelemetry);
                        break;
                    case "TraceTelemetry":
                        _client.TrackTrace(_traceTelemetry);
                        break;
                    case "ExceptionTelemetry":
                        _client.TrackException(_exceptionTelemetry);
                        break;
                    case "RequestTelemetry":
                        _client.TrackRequest(_requestTelemetry);
                        break;
                }

                _client.Flush();
            }
        }
    }
}