using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using static TravelBookingPlatform.Architecture.Tests.Constants;

namespace TravelBookingPlatform.Architecture.Tests;

public class ProjectsDependenciesTests
{
  [Fact]
  public void DomainShouldNotHaveDependencyOnAnyLayer()
  {
    var assembly = Assembly.Load(DomainAssembly);

    var otherProjects = new[]{
      ApplicationNamespace,
      InfrastructureNamespace,
      ApiNamespace
    };

    var result = Types.InAssembly(assembly)
      .ShouldNot()
      .HaveDependencyOnAll(otherProjects)
      .GetResult();

    result.IsSuccessful.Should().BeTrue();
  }
  
  [Fact]
  public void ApplicationShouldNotHaveDependencyOnExternalLayers()
  {
    var assembly = Assembly.Load(ApplicationAssembly);

    var otherProjects = new[]{
      InfrastructureNamespace,
      ApiNamespace
    };

    var result = Types.InAssembly(assembly)
      .ShouldNot()
      .HaveDependencyOnAll(otherProjects)
      .GetResult();

    result.IsSuccessful.Should().BeTrue();
  }
  
  [Fact]
  public void InfrastructureShouldNotHaveDependencyOnOtherExternalLayers()
  {
    var assembly = Assembly.Load(InfrastructureAssembly);

    var otherProjects = new[]{
      ApiNamespace
    };

    var result = Types.InAssembly(assembly)
      .ShouldNot()
      .HaveDependencyOnAll(otherProjects)
      .GetResult();

    result.IsSuccessful.Should().BeTrue();
  }
}