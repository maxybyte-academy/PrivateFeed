# POC Documentation: Creating and Publishing a .NET Class Library to Azure Artifacts

## Overview

This document provides a complete guide for creating a .NET class library, packaging it as a NuGet package, and publishing it to Azure Artifacts using Azure DevOps Pipelines.

**Project Example**: `AQI.Utility.TimeSpan.Formatter` - A utility library for converting DateTime to human-readable relative time strings (e.g., "5 minutes ago").

---

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Step 1: Create the Class Library Project](#step-1-create-the-class-library-project)
3. [Step 2: Configure the Project for NuGet Packaging](#step-2-configure-the-project-for-nuget-packaging)
4. [Step 3: Set Up Azure Artifacts Feed](#step-3-set-up-azure-artifacts-feed)
5. [Step 4: Configure NuGet for Local Development](#step-4-configure-nuget-for-local-development)
6. [Step 5: Create Azure DevOps Pipeline](#step-5-create-azure-devops-pipeline)
7. [Step 6: Grant Permissions](#step-6-grant-permissions)
8. [Step 7: Trigger the Pipeline](#step-7-trigger-the-pipeline)
9. [Consuming the Package](#consuming-the-package)
10. [Troubleshooting](#troubleshooting)
11. [Best Practices](#best-practices)

---

## Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 or VS Code
- Azure DevOps account and organization
- Git installed locally
- Basic knowledge of C# and .NET

---

## Step 1: Create the Class Library Project

### 1.1 Create Solution and Project

```bash
# Create solution directory
mkdir AQI.Utility.TimeSpan
cd AQI.Utility.TimeSpan

# Create solution file
dotnet new sln -n AQI.Utility.TimeSpan

# Create class library project
dotnet new classlib -n AQI.Utility.TimeSpan -f net8.0

# Add project to solution
dotnet sln add AQI.Utility.TimeSpan/AQI.Utility.TimeSpan.csproj
```

### 1.2 Project Structure

```
AQI.Utility.TimeSpan/
├── AQI.Utility.TimeSpan.sln
└── AQI.Utility.TimeSpan/
    ├── AQI.Utility.TimeSpan.csproj
    ├── Formatter.cs
    ├── docs/
    │   └── readme.md
    ├── library-pipelines.yml
    └── nuget.config
```

### 1.3 Implement the Library Code

**Example: Formatter.cs**

```csharp
namespace AQI.Utility.TimeSpan
{
    public static class Formatter
    {
        public static string ToRelativeTimeAgoString(this DateTime occurred)
        {
            var timeSpan = DateTime.UtcNow - occurred.ToUniversalTime();

            if (timeSpan.TotalSeconds < 60)
                return $"{(int)timeSpan.TotalSeconds} second{((int)timeSpan.TotalSeconds != 1 ? "s" : "")} ago";

            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} minute{((int)timeSpan.TotalMinutes != 1 ? "s" : "")} ago";

            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} hour{((int)timeSpan.TotalHours != 1 ? "s" : "")} ago";

            if (timeSpan.TotalDays < 30)
                return $"{(int)timeSpan.TotalDays} day{((int)timeSpan.TotalDays != 1 ? "s" : "")} ago";

            if (timeSpan.TotalDays < 365)
            {
                int months = (int)(timeSpan.TotalDays / 30);
                return $"{months} month{(months != 1 ? "s" : "")} ago";
            }

            int years = (int)(timeSpan.TotalDays / 365);
            return $"{years} year{(years != 1 ? "s" : "")} ago";
        }
    }
}
```

---

## Step 2: Configure the Project for NuGet Packaging

### 2.1 Update .csproj File

Add NuGet package metadata to your `.csproj` file:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>

    <!-- NuGet Package Metadata -->
    <PackageId>AQI.Utility.TimeSpan.Formatter</PackageId>
    <Version>1.0.0</Version>
    <Authors>Your Name</Authors>
    <Company>Your Company</Company>
    <Product>TimeSpan Utility</Product>
    <PackageTags>TimeSpan;Utility;C#;Library;DateTime;RelativeTime</PackageTags>
    <Description>
      Human-friendly "time ago" formatter that converts a DateTime into a relative time string (like "5 minutes ago" or "2 days ago").
    </Description>
    <PackageLicenseExpression>MIT</PackageLicenseExpression>
    <PackageReadmeFile>readme.md</PackageReadmeFile>
  </PropertyGroup>

  <ItemGroup>
    <None Include="docs\readme.md" Pack="true" PackagePath="\"/>
  </ItemGroup>

</Project>
```

### 2.2 Key Properties Explained

| Property | Description | Example |
|----------|-------------|---------|
| `PackageId` | Unique identifier for your package | `AQI.Utility.TimeSpan.Formatter` |
| `Version` | Semantic version number | `1.0.0` |
| `Authors` | Package authors | `Your Name` |
| `Company` | Company/organization name | `Your Company` |
| `Description` | What the package does | `Human-friendly time formatter...` |
| `PackageTags` | Searchable tags | `TimeSpan;Utility;DateTime` |
| `PackageLicenseExpression` | SPDX license identifier | `MIT` |
| `PackageReadmeFile` | README to include | `readme.md` |

### 2.3 Create Package README

Create `docs/readme.md`:

```markdown
# AQI.Utility.TimeSpan

Human-friendly "time ago" formatter that converts a `DateTime` into a relative time string.

## Installation

```bash
dotnet add package AQI.Utility.TimeSpan.Formatter
```

## Usage

```csharp
using AQI.Utility.TimeSpan;

var pastDate = DateTime.Now.AddMinutes(-5);
string result = pastDate.ToRelativeTimeAgoString();
// Output: "5 minutes ago"
```
```

---

## Step 3: Set Up Azure Artifacts Feed

### 3.1 Create Feed in Azure DevOps

1. Navigate to your Azure DevOps organization: `https://dev.azure.com/your-org`
2. Select your project
3. Click **Artifacts** in the left sidebar
4. Click **+ Create Feed**
5. Configure feed settings:
   - **Name**: `AQIPrivateFeed` (or your preferred name)
   - **Visibility**: Choose based on your needs
     - **Organization**: Available to entire organization
     - **Project**: Available only to this project
   - **Upstream sources**: Check NuGet.org if needed
6. Click **Create**

### 3.2 Note Your Feed Details

You'll need these later:
- **Organization**: e.g., `afs-dev`
- **Project**: e.g., `AQI`
- **Feed Name**: e.g., `AQIPrivateFeed`

---

## Step 4: Configure NuGet for Local Development

### 4.1 Create nuget.config

Create `nuget.config` in the same folder as your `.csproj`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <!-- Azure Artifacts feed -->
    <add key="AQIPrivateFeed" value="https://pkgs.dev.azure.com/your-org/_packaging/AQIPrivateFeed/nuget/v3/index.json" />
    <!-- NuGet.org as fallback -->
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3" />
  </packageSources>
  <packageSourceMapping>
    <packageSource key="nuget.org">
      <package pattern="*" />
    </packageSource>
    <packageSource key="AQIPrivateFeed">
      <package pattern="AQI.*" />
    </packageSource>
  </packageSourceMapping>
</configuration>
```

**Replace**:
- `your-org` with your Azure DevOps organization name
- `AQIPrivateFeed` with your feed name
- Adjust `packageSourceMapping` patterns as needed

### 4.2 Test Locally

```bash
# Restore packages
dotnet restore --interactive

# Build the project
dotnet build --configuration Release

# Pack the project
dotnet pack --configuration Release --output ./nupkg

# Push to feed (local testing)
dotnet nuget push ./nupkg/AQI.Utility.TimeSpan.Formatter.1.0.0.nupkg --source AQIPrivateFeed --api-key az
```

---

## Step 5: Create Azure DevOps Pipeline

### 5.1 Create Pipeline File

Create `library-pipelines.yml` in your project folder:

```yaml
# Azure Pipelines configuration for building and publishing NuGet package to Azure Artifacts

trigger:
  branches:
    include:
    - main
    - develop
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
    installationPath: $(Agent.ToolsDirectory)/dotnet

- task: DotNetCoreCLI@2
  displayName: 'Restore NuGet packages'
  inputs:
    command: 'restore'
    projects: '**/*.csproj'

- task: DotNetCoreCLI@2
  displayName: 'Build projects'
  inputs:
    command: 'build'
    projects: '**/*.csproj'
    arguments: '--configuration $(buildConfiguration) --no-restore'

- task: DotNetCoreCLI@2
  displayName: 'Pack NuGet package'
  inputs:
    command: 'pack'
    packagesToPack: '**/AQI.Utility.TimeSpan.csproj'
    configuration: $(buildConfiguration)
    nobuild: true
    versioningScheme: 'off'
    packDirectory: '$(Build.ArtifactStagingDirectory)'

- task: DotNetCoreCLI@2
  displayName: 'Push to Azure Artifacts'
  inputs:
    command: 'push'
    packagesToPush: '$(Build.ArtifactStagingDirectory)/**/*.nupkg'
    nuGetFeedType: 'internal'
    publishVstsFeed: '$(System.TeamProject)/AQIPrivateFeed'
  # Note: The Build Service must have 'Contributor' permission on the feed
  # See AZURE_DEVOPS_SETUP.md for configuration instructions
```

### 5.2 Pipeline Breakdown

| Task | Purpose |
|------|---------|
| **UseDotNet@2** | Installs .NET SDK (8.x) |
| **DotNetCoreCLI@2 (restore)** | Restores NuGet dependencies |
| **DotNetCoreCLI@2 (build)** | Builds the project in Release mode |
| **DotNetCoreCLI@2 (pack)** | Creates NuGet package (.nupkg) |
| **DotNetCoreCLI@2 (push)** | Publishes to Azure Artifacts |

### 5.3 Key Points

- **`publishVstsFeed`**: Uses `$(System.TeamProject)/FeedName` format for dynamic project resolution
- **`vmImage: 'ubuntu-latest'`**: Uses latest Ubuntu agent (works without mono for .NET Core)
- **`DotNetCoreCLI@2`**: Modern task for .NET Core/.NET 5+ projects

---

## Step 6: Grant Permissions

### 6.1 The Critical Step

**This is the most common issue!** The Build Service needs permission to push to the feed.

### 6.2 Grant Feed Permissions

1. Go to Azure DevOps → **Artifacts** → Select your feed (`AQIPrivateFeed`)
2. Click the **gear icon** (⚙️) for **Feed Settings**
3. Go to **Permissions** tab
4. Click **Add users/groups**
5. Search for and add one of these identities:

   **Option 1 (Recommended)**: Project Build Service
   ```
   [Your Project Name] Build Service (your-org)
   ```

   **Option 2**: Collection Build Service
   ```
   Project Collection Build Service (your-org)
   ```

6. Set the role to **Contributor** (or higher)
7. Click **Save**

### 6.3 Verify Permissions

The Build Service identity should now have:
- ✅ **Contributor** role on the feed
- ✅ **AddPackage** permission (included in Contributor)

---

## Step 7: Trigger the Pipeline

### 7.1 Set Up Pipeline in Azure DevOps

1. Go to Azure DevOps → **Pipelines** → **New Pipeline**
2. Select your repository source (GitHub, Azure Repos, etc.)
3. Choose **Existing Azure Pipelines YAML file**
4. Select your `library-pipelines.yml` file
5. Click **Run**

### 7.2 Monitor the Pipeline

Watch the pipeline run through these stages:
1. ✅ Install .NET SDK
2. ✅ Restore NuGet packages
3. ✅ Build projects
4. ✅ Pack NuGet package
5. ✅ Push to Azure Artifacts

### 7.3 Verify Success

After successful run:
1. Go to **Artifacts** in Azure DevOps
2. Select your feed
3. You should see your package: `AQI.Utility.TimeSpan.Formatter` v1.0.0

---

## Consuming the Package

### 8.1 In Another Project

**Add nuget.config to consuming project:**

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="AQIPrivateFeed" value="https://pkgs.dev.azure.com/your-org/_packaging/AQIPrivateFeed/nuget/v3/index.json" />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
</configuration>
```

**Install the package:**

```bash
dotnet add package AQI.Utility.TimeSpan.Formatter --version 1.0.0
```

**Use in code:**

```csharp
using AQI.Utility.TimeSpan;

var pastDate = DateTime.Now.AddHours(-2);
Console.WriteLine(pastDate.ToRelativeTimeAgoString());
// Output: "2 hours ago"
```

---

## Troubleshooting

### Issue 1: Ubuntu 24.04 Mono Error

**Error:**
```
The task has failed because you are using Ubuntu 24.04 or later without mono installed.
```

**Solution:**
- Use `DotNetCoreCLI@2` task instead of `NuGetCommand@2`
- The `DotNetCoreCLI@2` task doesn't require mono

### Issue 2: 404 Feed Not Found

**Error:**
```
TF1600011: The feed with ID 'AQIPrivateFeed' doesn't exist.
```

**Solutions:**
- Verify feed exists in Azure Artifacts
- Use correct format: `$(System.TeamProject)/FeedName` for project-scoped feeds
- Use just `FeedName` for organization-scoped feeds

### Issue 3: 403 Forbidden

**Error:**
```
403 (Forbidden - User 'xxx' lacks permission to complete this action. You need to have 'AddPackage'.
```

**Solution:**
- Grant Build Service **Contributor** permission on the feed (see Step 6)
- This is the most common issue!

### Issue 4: Project Not Found

**Error:**
```
TF200016: The following project does not exist: project-name
```

**Solution:**
- Don't hardcode project name
- Use `$(System.TeamProject)` variable for dynamic resolution

---

## Best Practices

### 1. Versioning

Use semantic versioning (SemVer):
- **Major**: Breaking changes (2.0.0)
- **Minor**: New features, backward compatible (1.1.0)
- **Patch**: Bug fixes (1.0.1)

### 2. Documentation

- Always include a README in your package
- Document all public APIs
- Provide usage examples

### 3. Package Metadata

- Choose descriptive `PackageId`
- Add relevant tags for discoverability
- Include license information

### 4. Pipeline Best Practices

- Use `DotNetCoreCLI@2` for .NET Core/.NET 5+ projects
- Use `$(System.TeamProject)` for portable pipelines
- Trigger on tags for version releases
- Use semantic versioning in pipeline

### 5. Security

- Don't commit secrets to source control
- Use Azure DevOps service connections for authentication
- Regularly update dependencies

### 6. Feed Organization

- Use project-scoped feeds for team projects
- Use organization-scoped feeds for shared libraries
- Set up upstream sources (NuGet.org) as needed

---

## Summary Checklist

When starting a new library project:

- [ ] Create class library project (`dotnet new classlib`)
- [ ] Add NuGet metadata to `.csproj`
- [ ] Implement library code
- [ ] Create package README (`docs/readme.md`)
- [ ] Create `nuget.config` with feed configuration
- [ ] Create Azure Artifacts feed in Azure DevOps
- [ ] Create `library-pipelines.yml` for CI/CD
- [ ] Grant Build Service **Contributor** permission on feed
- [ ] Set up pipeline in Azure DevOps
- [ ] Test pipeline run
- [ ] Verify package appears in feed
- [ ] Test consuming package in another project

---

## Additional Resources

- [NuGet Package Documentation](https://docs.microsoft.com/nuget/)
- [Azure Artifacts Documentation](https://docs.microsoft.com/azure/devops/artifacts/)
- [DotNetCoreCLI Task Reference](https://docs.microsoft.com/azure/devops/pipelines/tasks/build/dotnet-core-cli)
- [Semantic Versioning](https://semver.org/)

---

## Conclusion

This POC demonstrates a complete workflow for:
1. Creating a reusable .NET class library
2. Packaging it as a NuGet package
3. Publishing to Azure Artifacts via Azure Pipelines
4. Consuming the package in other projects

The key success factors are:
- Using modern `DotNetCoreCLI@2` tasks
- Proper feed permissions configuration
- Dynamic project resolution with `$(System.TeamProject)`
- Complete package metadata in `.csproj`

This workflow is production-ready and can be replicated for all future library projects.
