namespace Sitecore.Feature.Language.Tests
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using FluentAssertions;
    using Moq;
    using Sitecore.Abstractions;
    using Sitecore.Collections;
    using Sitecore.Feature.Language.Controllers;
    using Sitecore.Feature.Language.Infrastructure.Pipelines;
    using Sitecore.Feature.Language.Models;
    using Sitecore.Feature.Language.Repositories;
    using Sitecore.Foundation.Testing.Attributes;
    using Sitecore.Pipelines;
    using Xunit;

    public class LanguageControllerTests
    {
        [Theory]
        [AutoDbData]
        public void ChangeLanguage_ShouldInvokeChangeLanguagePipeline(string newLanguage, string currentLanguage, string changeResult, ILanguageRepository languageRepository)
        {
            // Arrange
            var pipelineManager = new Mock<BaseCorePipelineManager>();
            pipelineManager.Setup(x => x.Run(LanguageController.ChangeLanguagePipeline, It.IsAny<ChangeLanguagePipelineArgs>(), It.IsAny<bool>()))
                .Callback((string pipeline, PipelineArgs args, bool flag) => args.CustomData.Add(changeResult, new object()));
            var controller = new LanguageController(languageRepository, pipelineManager.Object);

            // Act
            var result = controller.ChangeLanguage(newLanguage, currentLanguage);

            // Assert
            pipelineManager.VerifyAll();
            result.Should().BeOfType<JsonResult>();
            var jsonResult = (JsonResult)result;
            jsonResult.Data.Should().BeOfType<SafeDictionary<string, object>>();
            var data = (SafeDictionary<string, object>)jsonResult.Data;
            data[changeResult].Should().NotBeNull();
        }

        [Theory]
        [AutoDbData]
        public void LanguageSelector_ShouldReturnActiveAndSupportedLanguages(BaseCorePipelineManager pipelineManager)
        {
            // Arrange
            var active = new Language();
            var supported = new List<Language>();
            var languageRepository = new Mock<ILanguageRepository>();
            languageRepository.Setup(x => x.GetActive()).Returns(active);
            languageRepository.Setup(x => x.GetSupportedLanguages()).Returns(supported);
            var controller = new LanguageController(languageRepository.Object, pipelineManager);

            // Act
            var result = controller.LanguageSelector();

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewResult = (ViewResult)result;
            viewResult.Model.Should().BeOfType<LanguageSelector>();
            var model = (LanguageSelector)viewResult.Model;
            model.ActiveLanguage.Should().Be(active);
            model.SupportedLanguages.Should().BeEquivalentTo(supported);
        }
    }
}
