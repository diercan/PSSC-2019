using PSSC.Models.Postare;
using PSSC.Repository;
using NUnit.Framework;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace PSSCTests
{

	public class PostareRepositoryShould
	{
		[Test,  Category("pass")]
		public void GetAllPosts()
		{

			// Arrange
			var repo = new PostareRepository();

			// Act
			var result = repo.GetAllPosts();

            // Assert
            NUnit.Framework.Assert.Equals(2, result.Count);
		}

		[Test,Category("pass")]
		public void CreeazaPostareTestPass()
		{
			// Arrange
			var repo = new PostareRepository();
            var PostareFactory = new PostareFactory();
            var postare = PostareFactory.CreeazaPostare("1", "1", "1", "1", (GradImportantaPostare)1,
                (TipPostare)1, DateTime.Now);

			// Act
			repo.AdaugarePostare(postare);
			var result = repo.GetAllPosts();

            // Assert
            NUnit.Framework.Assert.Equals(3, result.Count);
		}

        [Test, ExpectedException(typeof(GradSauTipIntroduseGresitException))]
        [Category("fail")]
        [TestCase(3, 1)]
        [TestCase(5, 1)]
        [TestCase(2, 3)]
        [TestCase(2, 7)]
        public void CreeazaPostareTestFail(int grad, int tip)
        {
            // Arrange
            var repo = new PostareRepository();
            var PostareFactory = new PostareFactory();
            
            //Act
            var postare = PostareFactory.CreeazaPostare("1", "1", "1", "1", (GradImportantaPostare)grad,
                (TipPostare)tip, DateTime.Now);

        }

        [Test, Category("pass")]
        public void StergerePostareTest()
        {
            //Arrange
            var repo = new PostareRepository();
            var DummyPost = new Postare();
            //Act
            repo.AdaugarePostare(DummyPost);

            var result = repo.GetAllPosts();
            //Assert
            NUnit.Framework.Assert.Equals(3, result.Count);

            //Act
            repo.StergerePostare(DummyPost);

            result = repo.GetAllPosts();
            //Assert
            NUnit.Framework.Assert.Equals(2, result.Count);

            

        }


        [Test, Category("fail")]
        public void CreeazaPostareNullTest()
        {
            Postare DummyEmptyPostare = new Postare();
            DummyEmptyPostare = null;
            var repo = new PostareRepository();
            repo.AdaugarePostare(DummyEmptyPostare);

            var result = repo.GetAllPosts();

            NUnit.Framework.Assert.Equals(3, result.Count);
        }

        [Test, Category("pass")]
        public void GetPostareByIdFail()
        {
            Guid idfail = new Guid();
            var repo = new PostareRepository();

            Postare post = repo.GetPostareById(idfail);

            NUnit.Framework.Assert.AreEqual(null, post);
        }

        [Test, Category("pass")]
        public void GetPostareByIdFound()
        {
            Guid id = new Guid();

            var newPostMock = new Mock<Postare>();

            newPostMock.Setup(x => x.id).Returns(id);
            
            var repo = new PostareRepository();

            repo.AdaugarePostare(newPostMock.Object);
            

            Postare postToFind = repo.GetPostareById(id);

            NUnit.Framework.Assert.AreEqual(newPostMock.Object, postToFind);
        }


    }
}
