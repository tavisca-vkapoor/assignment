using GitHubTestApi.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Linq;

namespace GitHubTestApi
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void CreateAndVerifyRepository()
        {
            //Arrange
            string expectedRepoName = "Dummy3";
            string userName = GitConfigurations.Default.username;
            var bodyDataForCreatingNewRepo = DataHelper.PopulateJsonDataForNewPublicRepository(expectedRepoName, "This is for dummy operation");
            var repositoryOperations = new RepositoryOperations();

            //Act
            var responseFromServer = repositoryOperations.CreateNewRepository(userName, GitConfigurations.Default.password, bodyDataForCreatingNewRepo);

            //Assert
            int idFromCreatedRemo = responseFromServer.Result.id;
            string nameFromCreatedRepo= responseFromServer.Result.name;

             Assert.AreEqual(responseFromServer.StatusCode, HttpStatusCode.Created);
             Assert.AreEqual(nameFromCreatedRepo, expectedRepoName);

            //GetAllReposFromServer
            var allReposFromServer = repositoryOperations.GetRepositoryForUser(userName, GitConfigurations.Default.PersonalToken);

            //Verifing the id of the created repostiory in GetAllRepos Collection
            Assert.Equals(allReposFromServer.Result.Single(e => e.id == idFromCreatedRemo ).id , idFromCreatedRemo);
        }

        [TestMethod]
        public void VerifyStatusCodeForAlreadyCreatedRepos()
        {
            //Arrange
            string alreadyCreateRepoName = "Dummy1";
            string userName = GitConfigurations.Default.username;
            var bodyForRepositoryCreation = DataHelper.PopulateJsonDataForNewPublicRepository(alreadyCreateRepoName, "This is for testing purpose");
            var repositoryOptions = new RepositoryOperations();

            //Act
            var responseFromServer = repositoryOptions.CreateNewRepository(userName, GitConfigurations.Default.password, bodyForRepositoryCreation);

            //Assert
            Assert.AreEqual(HttpStatusCode.UnprocessableEntity, responseFromServer.StatusCode);
        }

        [TestMethod]
        public void StarringAndVerifyingRepository()
        {
            //Arrange
            string repoToBeStarred = "Dummy1";
            string userName = GitConfigurations.Default.username;
            var repositoryOptions = new RepositoryOperations();

            //Act
            var responseFromServer = repositoryOptions.StarringRepository(userName, GitConfigurations.Default.password, repoToBeStarred);

            //Assert
            Assert.AreEqual(HttpStatusCode.NoContent, responseFromServer.StatusCode);
            //Getting all the starred repos
            var allStarredRepos = repositoryOptions.GetStarredRepository(userName, GitConfigurations.Default.password);
            //Verify the starred repos is in the list of all starred repos

            Assert.IsTrue(allStarredRepos.Result.Exists(e => e.name == repoToBeStarred));

            //Asserting the repo which is not starred is not in the list of all starred repos
            Assert.IsFalse(allStarredRepos.Result.Exists(e => e.name == "Hello1"));
        }

        [TestMethod]
        public void StarringRepoWhichIsNotPresent()
        {
            //Arrange
            string repoToBeStarredButNotInRepo = "Dummy10";
            string userName = GitConfigurations.Default.username;
            var repositoryOptions = new RepositoryOperations();

            //Act
            var responseFromServer = repositoryOptions.StarringRepository(userName, GitConfigurations.Default.password, repoToBeStarredButNotInRepo);

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, responseFromServer.StatusCode);

            //Getting all the starred repos
            var allStarredRepos = repositoryOptions.GetStarredRepository(userName, GitConfigurations.Default.password);

            //Asserting the repo which is not starred is not in the list of all starred repos
            Assert.IsFalse(allStarredRepos.Result.Exists(e => e.name == repoToBeStarredButNotInRepo));
        }
    }
}
