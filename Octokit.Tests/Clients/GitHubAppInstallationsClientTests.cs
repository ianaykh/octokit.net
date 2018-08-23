﻿using System;
using System.Linq;
using NSubstitute;
using Octokit.Clients;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class GitHubAppInstallationsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new GitHubAppInstallationsClient(null));
            }
        }

        public class TheGetAllRepositoriesForCurrentMethod
        {
            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppInstallationsClient(connection);

                client.GetAllRepositoriesForCurrent();

                connection.Received().GetAll<RepositoriesResponse>(Arg.Is<Uri>(u => u.ToString() == "installation/repositories"), null, "application/vnd.github.machine-man-preview+json");
            }

            [Fact]
            public void GetsFromCorrectUrllWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppInstallationsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAllRepositoriesForCurrent(options);

                connection.Received().GetAll<RepositoriesResponse>(Arg.Is<Uri>(u => u.ToString() == "installation/repositories"), null, "application/vnd.github.machine-man-preview+json", options);
            }
        }

        public class TheGetAllRepositoriesForCurrentUserMethod
        {
            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppInstallationsClient(connection);

                client.GetAllRepositoriesForCurrentUser(1234);

                var calls = connection.ReceivedCalls().ToList();

                connection.Received().GetAll<RepositoriesResponse>(Arg.Is<Uri>(u => u.ToString() == "user/installations/1234/repositories"), null, "application/vnd.github.machine-man-preview+json");
            }

            [Fact]
            public void GetsFromCorrectUrllWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppInstallationsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAllRepositoriesForCurrentUser(1234, options);

                connection.Received().GetAll<RepositoriesResponse>(Arg.Is<Uri>(u => u.ToString() == "user/installations/1234/repositories"), null, "application/vnd.github.machine-man-preview+json", options);
            }
        }
    }
}