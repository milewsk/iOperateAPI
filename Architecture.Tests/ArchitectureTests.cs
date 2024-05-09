using NetArchTest.Rules;
using FluentAssertions;

namespace Architecture.Tests;

public class ArchitectureTests
{
    private const string DomainNamespace = "Domain";
    private const string ApplicationNamespace = "Application";
    private const string InfrastructureNamespace = "Infrastructure";
    private const string PresentationNamespace = "Presentation";
    private const string ApiNamespace = "API";

    [Fact]
    public void DomainShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Domain.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Application.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            InfrastructureNamespace,
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void InfrastructureShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

        var otherProjects = new[]
        {
            PresentationNamespace,
            ApiNamespace
        };

        // Act
        var testResult = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(otherProjects).GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void PresentationShouldNotHaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Presentation.AssemblyReference).Assembly;
        
        // Act
        var otherProjects = new[]
        {
            InfrastructureNamespace,
            ApiNamespace
        };

        //Assert
        var testResult = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void HandlersShouldHaveDependencyOnDomain()
    {
        // Arrange
        var assembly = typeof(Application.AssemblyReference).Assembly;

        // Act
       var testResult = Types.InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveDependencyOn(DomainNamespace)
            .GetResult();
        
        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void ControllersShouldHaveDependencyOnMediator()
    {
        // Arrange
        var assembly = typeof(Presentation.AssemblyReference).Assembly;

        // Act
        var testResult = Types.InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Controller")
            .Should()
            .HaveDependencyOn("MediatR")
            .GetResult();
        
        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
}