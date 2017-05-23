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

using System.Linq;
using EventFlow.Extensions;
using EventFlow.PostgreSQL.EventStores;
using EventFlow.TestHelpers;
using EventFlow.TestHelpers.PostgreSQL;
using NUnit.Framework;

namespace EventFlow.PostgreSQL.Tests.IntegrationTests.EventStores
{
    [Category(Categories.Integration)]
    public class PostgreSQLScriptsTests
    {
        private IPostgreSQLDatabase _msSqlDatabase;

        [Test]
        public void SqlScriptsAreIdempotent()
        {
            // Arrange
            var sqlScripts = EventFlowEventStoresPostgreSQL.GetSqlScripts().ToList();

            // Act
            foreach (var _ in Enumerable.Range(0, 2))
            {
                foreach (var sqlScript in sqlScripts)
                {
                    _msSqlDatabase.Execute(sqlScript.Content);
                }
            }
        }

        [SetUp]
        public void SetUp()
        {
            _msSqlDatabase = PostgreSQLHelpz.CreateDatabase("eventflow");
        }

        public void TearDown()
        {
            _msSqlDatabase.DisposeSafe("PostgreSQL database");
        }
    }
}