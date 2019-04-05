using System;
using System.Collections.Generic;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;
using Mmu.Mlh.RestExtensions.Areas.Models;
using Mmu.Mlh.RestExtensions.Areas.Models.Security;
using Mmu.Mlh.RestExtensions.IntegrationTests.TestingInfrastructure.Models;

namespace Mmu.Mlh.RestExtensions.IntegrationTests.TestingAreas.Areas.RestProxies.Services
{
    internal static class DataGenerator
    {
        internal static RestCall CreateCommentsByPostIdRestCall()
        {
            var queryParameters = new List<QueryParameter> { new QueryParameter("postId", 1) };

            return new RestCall(
                new Uri("https://jsonplaceholder.typicode.com/comments"),
                Maybe.CreateNone<string>(),
                RestCallMethodType.Get,
                RestSecurity.CreateAnonymous(),
                new RestHeaders(new List<RestHeader>()),
                Maybe.CreateNone<RestCallBody>(),
                new QueryParameters(queryParameters));
        }

        internal static RestCall CreateGetOneTodoRestCall()
        {
            return new RestCall(
                new Uri("https://jsonplaceholder.typicode.com/todos/1"),
                Maybe.CreateNone<string>(),
                RestCallMethodType.Get,
                RestSecurity.CreateAnonymous(),
                new RestHeaders(new List<RestHeader>()),
                Maybe.CreateNone<RestCallBody>(),
                new QueryParameters(new List<QueryParameter>()));
        }

        internal static RestCall CreateNotExistingUrlRestCall()
        {
            return new RestCall(
                new Uri("https://http://googl12345.com/"),
                Maybe.CreateNone<string>(),
                RestCallMethodType.Get,
                RestSecurity.CreateAnonymous(),
                new RestHeaders(new List<RestHeader>()),
                Maybe.CreateNone<RestCallBody>(),
                new QueryParameters(new List<QueryParameter>()));
        }

        internal static RestCall CreatePostOneTodoRestCall(Todo todo)
        {
            return new RestCall(
                new Uri("https://jsonplaceholder.typicode.com/comments"),
                Maybe.CreateNone<string>(),
                RestCallMethodType.Post,
                RestSecurity.CreateAnonymous(),
                new RestHeaders(new List<RestHeader>()),
                RestCallBody.CreateApplicationJson(todo),
                new QueryParameters(new List<QueryParameter>()));
        }

        internal static RestCall CreatePostOneTodoRestCall(string todoJson)
        {
            return new RestCall(
                new Uri("https://jsonplaceholder.typicode.com/comments"),
                Maybe.CreateNone<string>(),
                RestCallMethodType.Post,
                RestSecurity.CreateAnonymous(),
                new RestHeaders(new List<RestHeader>()),
                RestCallBody.CreateApplicationJson(todoJson),
                new QueryParameters(new List<QueryParameter>()));
        }
    }
}