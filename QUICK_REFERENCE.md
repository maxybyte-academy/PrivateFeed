# Quick Reference Guide - NuGet Package Creation

This is a quick reference for creating and publishing .NET libraries to Azure Artifacts.

## Quick Commands

### Create New Library
```bash
# Create solution and project
dotnet new sln -n MyLibrary
dotnet new classlib -n MyLibrary -f net8.0
dotnet sln add MyLibrary/MyLibrary.csproj
```

### Build and Test Locally
```bash
# Restore dependencies
dotnet restore

# Build
dotnet build --configuration Release

# Pack
dotnet pack --configuration Release --output ./nupkg

# Push to feed
dotnet nuget push ./nupkg/MyLibrary.1.0.0.nupkg --source MyFeed --api-key az
```

## Essential .csproj Properties

```xml
<PropertyGroup>
  <PackageId>Your.Package.Name</PackageId>
  <Version>1.0.0</Version>
  <Authors>Your Name</Authors>
  <Company>Your Company</Company>
  <Description>Package description</Description>
  <PackageTags>tag1;tag2;tag3</PackageTags>
  <PackageLicenseExpression>MIT</PackageLicenseExpression>
  <PackageReadmeFile>readme.md</PackageReadmeFile>
</PropertyGroup>

<ItemGroup>
  <None Include="docs\readme.md" Pack="true" PackagePath="\"/>
</ItemGroup>
```

## Essential nuget.config

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="YourFeed" value="https://pkgs.dev.azure.com/org/_packaging/feed/nuget/v3/index.json" />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
</configuration>
```

## Essential Pipeline (library-pipelines.yml)

```yaml
trigger:
  branches:
    include:
    - main
  tags:
    include:
    - v*

pool:
  vmImage: 'ubuntu-latest'

variables:
  buildConfiguration: 'Release'
  dotnetVersion: '8.x'

steps:
- task: UseDotNet@2
  displayName: 'Install .NET SDK'
  inputs:
    packageType: 'sdk'
    version: $(dotnetVersion)

- task: DotNetCoreCLI@2
  displayName: 'Restore'
  inputs:
    command: 'restore'
    projects: '**/*.csproj'

- task: DotNetCoreCLI@2
  displayName: 'Build'
  inputs:
    command: 'build'
    projects: '**/*.csproj'
    arguments: '--configuration $(buildConfiguration) --no-restore'

- task: DotNetCoreCLI@2
  displayName: 'Pack'
  inputs:
    command: 'pack'
    packagesToPack: '**/*.csproj'
    configuration: $(buildConfiguration)
    nobuild: true
    versioningScheme: 'off'
    packDirectory: '$(Build.ArtifactStagingDirectory)'

- task: DotNetCoreCLI@2
  displayName: 'Push'
  inputs:
    command: 'push'
    packagesToPush: '$(Build.ArtifactStagingDirectory)/**/*.nupkg'
    nuGetFeedType: 'internal'
    publishVstsFeed: '$(System.TeamProject)/YourFeedName'
```

## Granting Feed Permissions

1. Azure DevOps → Artifacts → Select Feed
2. Settings (⚙️) → Permissions
3. Add: `[Project] Build Service (org)`
4. Role: **Contributor**

## Common Issues & Fixes

| Issue | Solution |
|-------|----------|
| Ubuntu 24.04 mono error | Use `DotNetCoreCLI@2` not `NuGetCommand@2` |
| Feed not found (404) | Use `$(System.TeamProject)/FeedName` |
| Forbidden (403) | Grant Build Service Contributor permission |
| Project not found | Use `$(System.TeamProject)` variable |

## Version Numbering

Semantic Versioning (SemVer): `MAJOR.MINOR.PATCH`

- **MAJOR**: Breaking changes (2.0.0)
- **MINOR**: New features, backward compatible (1.1.0)
- **PATCH**: Bug fixes (1.0.1)

## File Checklist

- [ ] `.csproj` with package metadata
- [ ] `nuget.config` with feed URL
- [ ] `library-pipelines.yml` for CI/CD
- [ ] `docs/readme.md` for package documentation
- [ ] Source code files
- [ ] `.sln` solution file

## Testing Your Package

### In Another Project

1. Add `nuget.config` with your feed
2. Install package:
   ```bash
   dotnet add package Your.Package.Name --version 1.0.0
   ```
3. Use in code:
   ```csharp
   using Your.Package.Namespace;
   ```

## Useful Links

- **Full Documentation**: [POC_DOCUMENTATION.md](./POC_DOCUMENTATION.md)
- **Azure Setup**: [AZURE_DEVOPS_SETUP.md](./AZURE_DEVOPS_SETUP.md)
- **Example Pipeline**: [library-pipelines.yml](./AQI.Utility.TimeSpan/AQI.Utility.TimeSpan/library-pipelines.yml)

---

For complete details, see [POC_DOCUMENTATION.md](./POC_DOCUMENTATION.md)
