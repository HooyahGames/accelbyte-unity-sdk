// Copyright (c) 2020 - 2024 AccelByte Inc. All Rights Reserved.
// This is licensed software from AccelByte Inc, for limitations
// and restrictions contact your company contract manager.

using System.Collections;
using System.Collections.Generic;
using AccelByte.Core;
using AccelByte.Models;
using System;
using UnityEngine.Assertions;

namespace AccelByte.Api
{
    [Obsolete("Please use ClientGameTelemetryApi api, will be removed on September release")]
    public class GameTelemetryApi : ApiBase
    {
        /// <summary>
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="config">baseUrl==GameTelemetryServerUrl</param>
        /// <param name="session"></param>
        [UnityEngine.Scripting.Preserve]
        public GameTelemetryApi( IHttpClient httpClient
            , Config config
            , ISession session ) 
            : this( httpClient, config, session , null)
        {
        }

        [UnityEngine.Scripting.Preserve]
        public GameTelemetryApi(IHttpClient httpClient
            , Config config
            , ISession session
            , HttpOperator httpOperator)
            : base(httpClient, config, config.GameTelemetryServerUrl, session, httpOperator)
        {
        }

        public IEnumerator SendProtectedEvents(List<TelemetryBody> events
            , ResultCallback callback)
        {
            if (events == null)
            {
                Result errorResult = Result.CreateError(ErrorCode.InvalidRequest, "Telemetry events are empty");
                callback?.Invoke(errorResult);
                yield break;
            }

            var request = HttpRequestBuilder
                .CreatePost(BaseUrl + "/v1/protected/events")
                .WithContentType(MediaType.ApplicationJson)
                .WithBody(events.ToUtf8Json())
                .WithBearerAuth(AuthToken)
                .Accepts(MediaType.ApplicationJson)
                .GetResult();

            IHttpResponse response = null;

            yield return HttpClient.SendRequest(request,
                rsp => response = rsp);

            var result = response.TryParse();
            callback.Try(result);
        }

        public void SendProtectedEventsV1(List<TelemetryBody> events, ResultCallback callback)
        {
            if(events == null)
            {
                Result result = Result.CreateError(ErrorCode.InvalidRequest, "Telemetry events are empty");
                callback?.Invoke(result);
                return;
            }

            var request = HttpRequestBuilder
                    .CreatePost(BaseUrl + "/v1/protected/events")
                    .WithContentType(MediaType.ApplicationJson)
                    .WithBody(events.ToUtf8Json())
                    .WithBearerAuth(AuthToken)
                    .Accepts(MediaType.ApplicationJson)
                    .GetResult();

            httpOperator.SendRequest(request, response =>
            {
                var result = response.TryParse();
                callback.Try(result);
            });
        }
    }
}
