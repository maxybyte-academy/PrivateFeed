# PrivateFeed - NuGet Package Library POC

This repository demonstrates how to create, package, and publish a .NET class library to Azure Artifacts using Azure DevOps Pipelines.

## 📦 What's Inside

This POC contains a complete example of:
- A .NET 8.0 class library (`AQI.Utility.TimeSpan.Formatter`)
- NuGet package configuration
- Azure DevOps Pipeline for automated publishing
- Complete documentation for replication

## 🚀 Quick Start

### View the Complete Documentation

📊 **[CONFLUENCE_PRESENTATION.md](./CONFLUENCE_PRESENTATION.md)** - **Confluence presentation for team meetings** ⭐ NEW!

📖 **[POC_DOCUMENTATION.md](./POC_DOCUMENTATION.md)** - Complete step-by-step guide

📋 **[AZURE_DEVOPS_SETUP.md](./AZURE_DEVOPS_SETUP.md)** - Azure DevOps permissions and troubleshooting

🚀 **[QUICK_REFERENCE.md](./QUICK_REFERENCE.md)** - Quick commands and templates

## 📚 What You'll Learn

1. **Creating a Class Library**
   - Setting up a .NET class library project
   - Structuring your code for reusability
   - Adding package metadata

2. **NuGet Packaging**
   - Configuring `.csproj` for NuGet
   - Including README and documentation
   - Setting up `nuget.config`
   - Versioning best practices

3. **Azure Artifacts**
   - Creating and configuring feeds
   - Managing permissions
   - Publishing packages

4. **CI/CD Pipeline**
   - Azure Pipelines YAML configuration
   - Automated build, pack, and publish
   - Modern .NET Core tasks
   - Troubleshooting common issues

## 🎯 Example Project

**Package Name**: `AQI.Utility.TimeSpan.Formatter`
**Purpose**: Human-friendly "time ago" formatter
**Example Usage**:

```csharp
using AQI.Utility.TimeSpan;

var pastDate = DateTime.Now.AddHours(-2);
Console.WriteLine(pastDate.ToRelativeTimeAgoString());
// Output: "2 hours ago"
```

## 📂 Repository Structure

```
PrivateFeed/
├── POC_DOCUMENTATION.md           # Complete step-by-step guide
├── AZURE_DEVOPS_SETUP.md          # Azure DevOps setup and troubleshooting
├── README.md                       # This file
└── AQI.Utility.TimeSpan/
    ├── AQI.Utility.TimeSpan.sln   # Solution file
    └── AQI.Utility.TimeSpan/
        ├── AQI.Utility.TimeSpan.csproj    # Project file with NuGet metadata
        ├── Formatter.cs                    # Library code
        ├── nuget.config                    # NuGet feed configuration
        ├── library-pipelines.yml           # Azure Pipelines configuration
        └── docs/
            └── readme.md                   # Package README
```

## ✅ Success Criteria

This POC successfully demonstrates:

- ✅ Creating a reusable .NET class library
- ✅ Configuring NuGet package metadata
- ✅ Setting up Azure Artifacts feed
- ✅ Creating Azure DevOps CI/CD pipeline
- ✅ Resolving common pipeline issues:
  - Ubuntu 24.04 mono requirement → Use `DotNetCoreCLI@2`
  - Feed not found → Use `$(System.TeamProject)` variable
  - Permission errors → Grant Build Service Contributor role
- ✅ Publishing package to private feed
- ✅ Consuming package in other projects

## 🔧 Technologies Used

- **.NET 8.0** - Target framework
- **C#** - Programming language
- **NuGet** - Package management
- **Azure Artifacts** - Private package hosting
- **Azure Pipelines** - CI/CD automation
- **YAML** - Pipeline configuration
- **Git/GitHub** - Source control

## 📖 Key Documentation Files

1. **[CONFLUENCE_PRESENTATION.md](./CONFLUENCE_PRESENTATION.md)** ⭐ **NEW!**
   - Professional presentation for Confluence
   - Ready to copy/paste into Confluence pages
   - Includes executive summary, architecture, challenges, and results
   - Perfect for team presentations and stakeholder updates
   - 24KB comprehensive presentation (763 lines)

2. **[POC_DOCUMENTATION.md](./POC_DOCUMENTATION.md)**
   - Complete workflow from start to finish
   - Step-by-step instructions
   - Code examples
   - Best practices
   - Troubleshooting guide

3. **[AZURE_DEVOPS_SETUP.md](./AZURE_DEVOPS_SETUP.md)**
   - Feed permissions setup
   - Common errors and solutions
   - Local development setup
   - Pipeline configuration details

4. **[QUICK_REFERENCE.md](./QUICK_REFERENCE.md)**
   - Essential commands at a glance
   - Configuration templates
   - Common issues reference table

5. **[CONFLUENCE_IMPORT_GUIDE.md](./CONFLUENCE_IMPORT_GUIDE.md)**
   - How to import the presentation into Confluence
   - Customization tips
   - Best practices for presenting

6. **[library-pipelines.yml](./AQI.Utility.TimeSpan/AQI.Utility.TimeSpan/library-pipelines.yml)**
   - Production-ready pipeline configuration
   - Automated build, pack, push workflow

## 🎓 Learning Outcomes

After following this POC, you will know how to:

1. Structure a .NET class library for NuGet packaging
2. Configure package metadata in `.csproj`
3. Set up Azure Artifacts feeds
4. Create Azure DevOps pipelines for automatic publishing
5. Handle common pipeline issues
6. Consume private NuGet packages
7. Apply best practices for versioning and documentation

## 🚦 Pipeline Workflow

```
Trigger (push to main/develop or tag)
  ↓
Install .NET SDK 8.x
  ↓
Restore NuGet Packages
  ↓
Build (Release Configuration)
  ↓
Pack NuGet Package
  ↓
Push to Azure Artifacts Feed
  ↓
✅ Package Available for Consumption
```

## 🔐 Security Notes

- Build Service requires **Contributor** permission on the feed
- Use `nuget.config` for feed configuration
- Never commit credentials to source control
- Azure DevOps handles authentication automatically in pipelines

## 🎯 Next Steps for Real Projects

When creating your real library projects:

1. **Start Here**: Follow [POC_DOCUMENTATION.md](./POC_DOCUMENTATION.md)
2. **Use as Template**: Copy the structure and pipeline configuration
3. **Customize**: Update package names, namespaces, and metadata
4. **Add Tests**: Include unit tests for your library
5. **Version Control**: Use semantic versioning
6. **Documentation**: Keep README updated with API usage

## 🤝 Contributing

This is a POC/template repository. Feel free to:
- Use it as a template for your own libraries
- Adapt the pipeline for your needs
- Reference the documentation for your projects

## 📝 License

MIT License - See package configuration for details

## 🙏 Acknowledgments

This POC was created to demonstrate best practices for:
- .NET library development
- NuGet package creation
- Azure DevOps automation
- Private package hosting

---

**Ready to create your own library?** Start with [POC_DOCUMENTATION.md](./POC_DOCUMENTATION.md) for the complete guide! 🚀
