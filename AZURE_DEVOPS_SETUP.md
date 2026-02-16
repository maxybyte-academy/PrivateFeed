# Azure DevOps Pipeline Setup Guide

## Prerequisites

This guide explains how to configure Azure DevOps to allow the pipeline to push NuGet packages to Azure Artifacts.

## Feed Permissions Setup

The pipeline requires the Build Service to have permission to push packages to the Azure Artifacts feed.

### Steps to Grant Permissions:

1. **Navigate to Azure Artifacts Feed**
   - Go to Azure DevOps: `https://dev.azure.com/afs-dev`
   - Click on **Artifacts** in the left sidebar
   - Select your feed: **AQIPrivateFeed**

2. **Open Feed Settings**
   - Click the gear icon (⚙️) or **Feed Settings** button
   - Navigate to **Permissions** tab

3. **Add Build Service Identity**

   You need to add one of these identities with **Contributor** role:

   - **Option 1 (Recommended)**: Project-scoped Build Service
     ```
     [Your Project Name] Build Service (afs-dev)
     ```
     Replace `[Your Project Name]` with your actual Azure DevOps project name

   - **Option 2**: Collection Build Service
     ```
     Project Collection Build Service (afs-dev)
     ```

4. **Set Permission Level**
   - Role: **Contributor** (or higher)
   - This grants the `AddPackage` permission required to push packages

### Common Issues

#### 403 Forbidden Error
```
error: Response status code does not indicate success: 403 (Forbidden - User 'xxx' lacks permission to complete this action. You need to have 'AddPackage'.
```

**Solution**: This error means the Build Service doesn't have permissions. Follow the steps above to grant Contributor access.

#### Feed Not Found (404)
```
error: TF1600011: The feed with ID 'AQIPrivateFeed' doesn't exist.
```

**Solution**: Verify the feed exists and the name is correct. The pipeline uses `$(System.TeamProject)/AQIPrivateFeed` format.

## Pipeline Configuration

The pipeline is configured to:
- Use .NET 8.x SDK
- Build and pack the project
- Push to Azure Artifacts feed using the project-scoped feed format

### Feed Configuration

The feed is referenced as:
```yaml
publishVstsFeed: '$(System.TeamProject)/AQIPrivateFeed'
```

This dynamically resolves to the correct project at runtime.

## Local Development Setup

For local development and testing:

1. **Install Azure Artifacts Credential Provider**
   ```bash
   # The credential provider is included with the latest .NET SDK
   # Or download from: https://github.com/microsoft/artifacts-credprovider
   ```

2. **Restore Packages**
   ```bash
   dotnet restore --interactive
   ```

   The `--interactive` flag allows dotnet to prompt for credentials if needed.

3. **Push Packages Locally** (if needed)
   ```bash
   dotnet nuget push <package>.nupkg --source AQIPrivateFeed --api-key az
   ```

## Troubleshooting

### Check Build Service Identity

To find the exact Build Service identity name:
1. Go to Project Settings → Pipelines → Service connections
2. Or check the pipeline logs for the user GUID in error messages
3. Search for that GUID in Azure DevOps organization settings

### Verify Feed URL

The feed URL format should be:
- **Organization-scoped**: `https://pkgs.dev.azure.com/{org}/_packaging/{feed}/nuget/v3/index.json`
- **Project-scoped**: `https://pkgs.dev.azure.com/{org}/{project}/_packaging/{feed}/nuget/v3/index.json`

### Pipeline Logs

Check the pipeline logs to see the actual URL being used for pushing packages.

## References

- [Azure Artifacts Permissions](https://docs.microsoft.com/azure/devops/artifacts/feeds/feed-permissions)
- [Publish to Azure Artifacts](https://docs.microsoft.com/azure/devops/pipelines/artifacts/nuget)
- [.NET Core CLI Task](https://docs.microsoft.com/azure/devops/pipelines/tasks/build/dotnet-core-cli)
