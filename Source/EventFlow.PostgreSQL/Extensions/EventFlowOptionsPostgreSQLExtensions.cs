// The MIT License (MIT)
// 
// Copyright (c) 2015-2017 Rasmus Mikkelsen
// Copyright (c) 2015-2017 eBay Software Foundation
// https://github.com/eventflow/EventFlow
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using EventFlow.Configuration;
using EventFlow.PostgreSQL.Connections;
using EventFlow.PostgreSQL.RetryStrategies;

namespace EventFlow.PostgreSQL.Extensions
{
#if NET451
    public static class EventFlowOptionsPostgreSQLExtensions
    {
        public static IEventFlowOptions ConfigurePostgreSQL(
            this IEventFlowOptions eventFlowOptions,
            IPostgreSQLConfiguration postgreSQLConfiguration)
        {
            return eventFlowOptions
                .RegisterServices(f =>
                    {
                        f.Register<IPostgreSQLDatabaseMigrator, PostgreSQLDatabaseMigrator>();
                        f.Register<IPostgreSQLConnection, PostgreSQLConnection>();
                        f.Register<IPostgreSQLConnectionFactory, PostgreSQLConnectionFactory>();
                        f.Register<IPostgreSQLErrorRetryStrategy, PostgreSQLErrorRetryStrategy>();
                        f.Register(_ => postgreSQLConfiguration, Lifetime.Singleton);
                    });
        }
    }
#else
    using System;
    public static class EventFlowOptionsPostgreSQLExtensions
    {
        public static IEventFlowOptions ConfigurePostgreSQL(
            this IEventFlowOptions eventFlowOptions,
            IPostgreSQLConfiguration postgreSQLConfiguration, Type databaseMigratorType)
        {
            if(databaseMigratorType == null)
                throw new ArgumentNullException(nameof(databaseMigratorType));

            if(!(databaseMigratorType is IPostgreSQLDatabaseMigrator))
                throw new ArgumentException(nameof(databaseMigratorType));

            return eventFlowOptions
                .RegisterServices(f =>
                {
                    f.Register(typeof(IPostgreSQLDatabaseMigrator), databaseMigratorType);
                    f.Register<IPostgreSQLConnection, PostgreSQLConnection>();
                    f.Register<IPostgreSQLConnectionFactory, PostgreSQLConnectionFactory>();
                    f.Register<IPostgreSQLErrorRetryStrategy, PostgreSQLErrorRetryStrategy>();
                    f.Register(_ => postgreSQLConfiguration, Lifetime.Singleton);
                });
        }
    }
#endif
}